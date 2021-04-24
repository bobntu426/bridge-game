using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager manager;

    //�C���e���W����l�B��ܶ�����ƪ���r�B��Ӫ��e���B�ⶤ�ؼФ��ƻP��o���ƪ���ܤ�rUI�B�C���i�椤�����s�C�����s
    public GameObject table, team_red, team_blue, win_panel, red_goal, red_team_score, blue_team_score, blue_goal, restart_button, bride_game

        //�C���i�椤���ⶤ�ؼм[�Ƥ�rUI�B��ܤ�����rUI�B�޲zAI�s�P������
        , red_goal_UI, blue_goal_UI, king_UI,AI_control,AI_pointer;

    //�x�s�Ҧ����J�P�������}�C�B�ۼƦr�����s�B�۪�⪺���s�B���b��W���P�B�U���a�ۤF�ƻ���ܦr��UI�B���player��r��UI
    public GameObject[] Poker, number_button, color_button, pointer, table_card,player_call_card,playerUI;

    public GameObject[,] player, player_in_game;//�����U���a�֦����P�B�C�����|�H�۪��a�X�P���ܰʪ��P�}�C
    public Button startgame;//�}�l�C�������s
    public Vector2 mousepos,table_card_scale;//�ƹ��y�СB�P�b�ୱ�W�ɷ|�ܦ����j�p
    public Vector2[] cardnormalscale, cardchoosescale;//�U�i�P�b�|�쪱�a�������j�p�B�Q����ɪ��j�p
    public int[] card_number;//�P��W��m���P

    //�|�쪱�a����P�`���z���סB�̥��䪺��P����V��m(���a0�B2��x�b�j�p�A���a1�B3��y�b�j�p)�B�|�쪱�a��P���a�V��m(���a0�B2��y�b�j�p�A���a1�B3��x�b�j�p)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;

    public int[] handcardnum;//�|�쪱�a����P�ƶq
    public int red_score, blue_score, red_goal_score, blue_goal_score;//�ⶤ�Y���[�ơB�ⶤ�ؼм[��
    public int call_number, want_number;//�۵P��U�s���Ʀr�ݳ̲׳ۨ쪺�Ʀr�A�Ȯɳۨ쪺�Ʀr
    public int pass, temp_king, want_king;//��pass�����ơB���b�۵P�����a�B�Ȯɳۨ쪺���B�۵P��U�n�s�����
    public int table_card_number, playing_player;//��W�w�����P�ơB���찵�ƪ����a
    public string king;//�̲׳ۨ쪺���
    public bool call_card_finish,AI_mode;//�O�_�۵P�����F�B�O�_�O�q���۰ʥX�P�Ҧ�
    public string must_color, temp_color;//��P�����N�@�w�n���X�����B�Ȧs�Ϊ�����ܼ�


    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
    }
    private void Start()
    {
        
        AI_mode = false;
    }

    public void Startcard()
    {
        //�N�U���ܼƭ��s��s
        short[,] playerindex = new short[4, 13];
        short[] index = new short[52];
        short b, k, temp, i, f, rcard;
        player = new GameObject[4, 13];
        rcard = 52;
        handcardnum = new int[] { 13, 13, 13, 13 };
        table_card = new GameObject[4];
        player_in_game = new GameObject[4, 13];
        call_number = 1;
        temp_king = 0;
        pass = 0;
        table_card_number = 0;
        call_card_finish = false;
        blue_score = 0;
        red_score = 0;
        for (i = 0; i < 4; i++)
        {
            playerUI[i].SetActive(true);
            player_call_card[i].SetActive(true);
            player_call_card[i].GetComponent<Text>().text = "�|���۵P";
        }//���s�U���a���۵P���pUI
        red_goal_UI.SetActive(false);
        blue_goal_UI.SetActive(false);
        king_UI.SetActive(false);

        //AI�Ҧ����]�w
        if (AI_mode == true)
        {
            
            AI_control.SetActive(true);
            foreach (GameObject h in number_button)
                h.GetComponent<Button>().interactable = true;
            foreach (GameObject h in color_button)
                h.GetComponent<Button>().interactable = true;
        }


        color_distribute();//��C�i�P�ФW���

        //�H�����t�U�i�P��index��U���a�⤤
        for (i = 0; i < 52; i++)
            index[i] = i;
        for (i = 0; i < 52; i++)
        {
            b = (short)Random.Range(0, rcard);
            if (i < 13)
                playerindex[0, i] = index[b];
            else if (i < 26)
                playerindex[1, i - 13] = index[b];
            else if (i < 39)
                playerindex[2, i - 26] = index[b];
            else
                playerindex[3, i - 39] = index[b];
            rcard--;
            for (k = b; k < rcard; k++)
                index[k] = index[k + 1];
        }

        for (k = 0; k < 4; k++)
        {
            for (b = 0; b < 12; b++)
            {
                for (f = 0; f < 12 - b; f++)
                {
                    if (playerindex[k, f] > playerindex[k, f + 1])
                    {
                        temp = playerindex[k, f];
                        playerindex[k, f] = playerindex[k, f + 1];
                        playerindex[k, f + 1] = temp;
                    }
                }
            }//��C�쪱�a�⤤���P��index�Ƨ�
            for (i = 0; i < 13; i++)
            {
                Instantiate(Poker[playerindex[k, i]], new Vector2(-0.63f, 0.09f), Quaternion.identity);
                player[k, i] = GameObject.Find(Poker[playerindex[k, i]].name + "(Clone)");
                player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k));
                player[k, i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
                if (k == 0)
                {
                    player[k, i].transform.position = new Vector2(leftcardhorizonpos[k] + i * (handcardlength[k] / 12), cardverticalpos[k]);
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 1)
                {
                    player[k, i].transform.position = new Vector2(cardverticalpos[k], leftcardhorizonpos[k] + i * (handcardlength[k] / 12));
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 2)
                {
                    player[k, i].transform.position = new Vector2(leftcardhorizonpos[k] - i * (handcardlength[k] / 12), cardverticalpos[k]);
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 3)
                {
                    player[k, i].transform.position = new Vector2(cardverticalpos[k], leftcardhorizonpos[k] - i * (handcardlength[k] / 12));
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                
                player[k, i].AddComponent<BoxCollider2D>();
                
                player[k, i].GetComponent<BoxCollider2D>().isTrigger = true;
                if (AI_mode == false)
                    player[k, i].AddComponent<mousecontrol>();
                else if (k == 0)
                    player[k, i].AddComponent<mousecontrol>();
                player_in_game[k, i] = player[k, i];
            }//�̷�index��U�i�P���t��U���a�⤤�A�ó]�n��m�B�ݩ�
        }//��C�쪱�a�⤤���P��index�ƧǡA�è̷�index��U�i�P���t��U���a�⤤�A�ó]�n��m�B�ݩ�

        for (i = 1; i < 4; i++)
        {
            if(count_card_point(i, "clover")+ count_card_point(i, "diamond") + count_card_point(i, "heart")+ count_card_point(i, "spade")<4)
                restart_button.GetComponent<Button>().onClick.Invoke();
        }//�˵P����
    }//�}�l�@���C���A���s�U���ƾڡA��P�o�n��z��U���a�⤤
    
    public Vector2 cardtoplayer(GameObject[,] player, GameObject poker)
    {
        Vector2 a;
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                if (player[i, k] == poker)
                {
                    a = new Vector2(i, k);
                    return a;
                }
        print("no person own the card");
        a = new Vector2(-1, -1);
        return a;
    }//�Ǧ^�ӵP���ݪ����a�s���M�ӵP����P������ƨӲĴX�i�P
    public void red_getscore()
    {
        manager.red_score++;
        GameObject.Find("teamred").GetComponent<Text>().text = "�����[�ơG" + manager.red_score;
    }//�����o���ɭn������
    public void blue_getscore()
    {
        manager.blue_score++;
        GameObject.Find("teamblue").GetComponent<Text>().text = "�Ŷ��[�ơG" + manager.blue_score;
    }//�Ŷ��o���ɭn������
    public void reset_score()
    {
        GameObject.Find("teamblue").GetComponent<Text>().text = "�Ŷ��[�ơG" + 0;
        GameObject.Find("teamred").GetComponent<Text>().text = "�����[�ơG" + 0;
    }//�N�ⶤ�[����ܭ��s��0

    public int number_of_card(GameObject card)
    {
        for (int i = 0; i < 52; i++)
            if (Poker[i].name + "(Clone)" == card.name)
                return i;
        return -1;
    }//�Ǧ^�Y�i�P���Ʀr
    public void card_compare()
    {
        bool big;
        GameObject temp;

        for (int i = 0; i < 3; i++)
        {
            big = false;
            if (table_card[i].tag == table_card[i + 1].tag)
            {
                if (number_of_card(table_card[i]) > number_of_card(table_card[i + 1]))
                    big = true;

            }

            else if (table_card[i].tag == "kingcolor")
            {
                big = true;
            }
            else if (table_card[i].tag == "mastercolor" && table_card[i + 1].tag != "kingcolor")
                big = true;
            if (big == true)
            {
                temp = table_card[i];
                table_card[i] = table_card[i + 1];
                table_card[i + 1] = temp;
            }
        }

        if (cardtoplayer(player, table_card[3]).x == 0 || cardtoplayer(player, table_card[3]).x == 2)
        {
            red_getscore();
            
            if (cardtoplayer(player, table_card[3]).x == 0)
            {
                pointer[playing_player].SetActive(false);
                playing_player = 0;
                pointer[0].SetActive(true);
            }
            else
            {
                pointer[playing_player].SetActive(false);
                playing_player = 2;
                pointer[2].SetActive(true);
            }
        }
        else
        {
            
            blue_getscore();
            if (cardtoplayer(player, table_card[3]).x == 1)
            {
                pointer[playing_player].SetActive(false);
                playing_player = 1;
                pointer[1].SetActive(true);
            }
            else
            {
                pointer[playing_player].SetActive(false);
                playing_player = 3;
                pointer[3].SetActive(true);
            }
        }
    }//�����@���P��A����֦Y��[�A����ƭp��n�A�}�l�U�^�X�C


    public void inactive_button()
    {

        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(true);
        }
        restart_button.SetActive(true);
    }//�]�m�}�l�۵P�����s
    public void destroy_button()
    {
        for (int i = 0; i < 6; i++)
        {
            color_button[i].SetActive(false);
        }
        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(false);
        }
    }//�����Ҧ��۵P�����s
    public int compute_color(string color, int player)
    {
        int num = 0;
        for (int i = 0; i < handcardnum[player]; i++)
        {
            if (player_in_game[player, i].tag == color)
                num++;
        }
        return num;
    }//�Ǧ^�Y�@���b�Y�@���a�⤤���ƶq
    public void put_card_on_table(GameObject gameObject,ref bool ontable)
        {
        ontable = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        //��P���X�h��Aplayer_in_game�}�C�������ӱi�P�B�⤤�ѤU���P���s��n��m
        if (cardtoplayer(player, gameObject).x == 0)
        {
            handcardnum[0]--;
            for (int k = (int)cardtoplayer(player_in_game, gameObject).y; k < handcardnum[0]; k++)
                player_in_game[0, k] = player_in_game[0, k + 1];
            for (int i = 0; i < handcardnum[0]; i++)
                player_in_game[0, i].transform.position = new Vector2(leftcardhorizonpos[0] + handcardlength[0] / 24 * (13 - handcardnum[0]) + handcardlength[0] / 12 * i, cardverticalpos[0]);
            gameObject.transform.position = new Vector2(0, -1.7f);
            transform.localScale = table_card_scale;
        }
        else if (cardtoplayer(player, gameObject).x == 1)
        {
            handcardnum[1]--;
            for (int k = (int)cardtoplayer(player_in_game, gameObject).y; k < handcardnum[1]; k++)
                player_in_game[1, k] = player_in_game[1, k + 1];
            for (int i = 0; i < handcardnum[1]; i++)
                player_in_game[1, i].transform.position = new Vector2(cardverticalpos[1], leftcardhorizonpos[1] + handcardlength[1] / 24 * (13 - handcardnum[1]) + handcardlength[1] / 12 * i);

            gameObject.transform.position = new Vector2(1.5f, 0);
            transform.localScale = table_card_scale;


        }
        else if (cardtoplayer(player, gameObject).x == 2)
        {
            handcardnum[2]--;
            for (int k = (int)cardtoplayer(player_in_game, gameObject).y; k < handcardnum[2]; k++)
                player_in_game[2, k] = player_in_game[2, k + 1];
            for (int i = 0; i < handcardnum[2]; i++)
                player_in_game[2, i].transform.position = new Vector2(leftcardhorizonpos[2] - handcardlength[2] / 24 * (13 - handcardnum[2]) - handcardlength[2] / 12 * i, cardverticalpos[2]);

            gameObject.transform.position = new Vector2(0, 1.7f);
            transform.localScale = table_card_scale;

        }
        else
        {
            handcardnum[3]--;
            for (int k = (int)cardtoplayer(player_in_game, gameObject).y; k < handcardnum[3]; k++)
                player_in_game[3, k] = player_in_game[3, k + 1];
            for (int i = 0; i < handcardnum[3]; i++)
                player_in_game[3, i].transform.position = new Vector2(cardverticalpos[3], leftcardhorizonpos[3] - handcardlength[3] / 24 * (13 - handcardnum[3]) - handcardlength[3] / 12 * i);
            gameObject.transform.position = new Vector2(-1.5f, 0);
        }

        table_card[table_card_number] = gameObject;//�N�ӱi�P����x�s�ୱ�W���P���}�C
        table_card_number++;
    }//��⤤���P��Ԩ��W�|Ĳ�o���ʧ@�A�ݦP�ɶǤW�Ӹ}����ontable�Ѽ�
    public void after_call_card()
    {
        //�M�w��
        if (temp_king == 1)
        {
            king = "clover";
            king_UI.GetComponent<Text>().text = "���G����";
        }
        if (temp_king == 2)
        {
            king = "diamond";
            king_UI.GetComponent<Text>().text = "���G�j��";
        }
        if (temp_king == 3)
        {
            king = "heart";
            king_UI.GetComponent<Text>().text = "���G�R��";
        }
        if (temp_king == 4)
        {
            king = "spade";
            king_UI.GetComponent<Text>().text = "���G�®�";
        }
        if (temp_king == 5)
        {
            king = "mastercolor";
            king_UI.GetComponent<Text>().text = "�����L��";
        }

        //�������⪺tag�令kingcolor
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                if (player[i, k].tag == king)
                    player[i, k].tag = "kingcolor";
        


        if (playing_player == 0 || playing_player % 4 == 2)
        {
            blue_goal_score = call_number + 6;
            red_goal_score = 14 - blue_goal_score;
        }//�Ŷ��ۨ�
        else
        {
            red_goal_score = call_number + 6;
            blue_goal_score = 14 - red_goal_score;
        }//�����ۨ�
        
        //�]�m�����W����r
        red_goal_UI.GetComponent<Text>().text = "�����ؼм[�ơG" + red_goal_score;
        blue_goal_UI.GetComponent<Text>().text = "�Ŷ��ؼм[�ơG" + blue_goal_score;
        red_goal_UI.SetActive(true);
        blue_goal_UI.SetActive(true);
        king_UI.SetActive(true);
    }//�۪��P��ھڳ۵P���e�]�w�n�W�h
    public void restart_card() 
    {
        playing_player = 0;
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                DestroyImmediate(player[i, k]);
        inactive_button();
        table.SetActive(true);
        team_red.SetActive(true);
        team_blue.SetActive(true);
        pointer[0].SetActive(true);
        pointer[1].SetActive(false);
        pointer[2].SetActive(false);
        pointer[3].SetActive(false);
        reset_score();
        win_panel.SetActive(false);
        Startcard();
    }//���s�}�l�C��
    public void color_distribute() 
    {
        for (int i = 0; i < 52; i++)
            if (i < 13)
                Poker[i].tag = "clover";
            else if (i < 26)
                Poker[i].tag = "diamond";
            else if (i < 39)
                Poker[i].tag = "heart";
            else
                Poker[i].tag = "spade";
    }//��C�i�P�ФW���
    public bool can_be_select(bool ontable ,GameObject gameObject) 
    {
        
        if (table_card_number == 4 && table_card[3].activeSelf == true)
            return false;
        if (
            ontable == false //���b��W
            &&
            call_card_finish == true //�ۧ��P�F�~�ॴ�P
            &&
            (
                //�b�S�w���p�U���Y�Ǫ�⤣�ॴ
                table_card_number == 0 //�Ĥ@�ӤH�X�P�i�H�X������
                ||
                gameObject.tag == must_color //�u��X��Ĥ@�ӤH�@�˪����
                ||

                //�⤤�p�G�Ӫ��S�F�N�i�H���P�ιԵP
                compute_color(must_color, (int)cardtoplayer(player, gameObject).x) == 0
            )
            &&
            //����Ӫ��a�~��X�P
            (int)cardtoplayer(player, gameObject).x == playing_player
            )
            return true;
        else
            return false;
    }//�˴����i�P�ण�ॴ�A�i�H���ܤ~����
    public void playerUI_control(string color) 
    {
        player_call_card[(playing_player + 3) % 4].GetComponent<Text>().text = call_number + color;
        for (int i = 0; i < 3; i++)
            player_call_card[(playing_player + i) % 4].GetComponent<Text>().text = "�|���۵P";

    }
    public void game_finish() 
    {
        AI_control.SetActive(false);
        red_goal_UI.SetActive(false);
        blue_goal_UI.SetActive(false);
        king_UI.SetActive(false);
        blue_goal.GetComponent<Text>().text = "�Ŷ��ؼм[�ơG" + blue_goal_score;
        red_goal.GetComponent<Text>().text = "�����ؼм[�ơG" + red_goal_score;
        red_team_score.GetComponent<Text>().text = "������o�[�ơG" + red_score;
        blue_team_score.GetComponent<Text>().text = "�Ŷ���o�[�ơG" + blue_score;
    }

    public string transform_num_to_color(int a) 
    {
        if (a == 1)
            return "clover";
        if (a == 2)
            return "diamond";
        if (a == 3)
            return "heart";
        if (a == 4)
            return "spade";
        if (a == 5)
            return "noking";
        else {
            print("this color doesn't indicate any integer");
            return "null";
        }
    }

    public int count_card_point(int player,string color) 
    {
        int point = 0;
        if (color == "clover")
        {
            for (int i = 0; i < 13; i++)
            {
                if (number_of_card(manager.player[player, i]) >= 0 && number_of_card(manager.player[player, i]) < 13 && number_of_card(manager.player[player, i]) - 8 > 0)
                    point += (number_of_card(manager.player[player, i]) - 8);
            }
            return point;
        }
        else if (color == "diamond")
        {
            
            for (int i = 0; i < 13; i++)
            {
                if (number_of_card(manager.player[player, i]) >= 13 && number_of_card(manager.player[player, i]) < 26 && number_of_card(manager.player[player, i]) - 21 > 0)
                    point += (number_of_card(manager.player[player, i]) - 21);
            }
            return point;
        }
        else if (color == "heart")
        {
            for (int i = 0; i < 13; i++)
            {
                if (number_of_card(manager.player[player, i]) >= 26 && number_of_card(manager.player[player, i]) < 39 && number_of_card(manager.player[player, i]) - 34 > 0)
                    point += (number_of_card(manager.player[player, i]) - 34);  
            }
            return point;
        }
        else if (color == "spade")
        {
            for (int i = 0; i < 13; i++)
            {
                if (number_of_card(manager.player[player, i]) >= 39 && number_of_card(manager.player[player, i]) < 52 && number_of_card(manager.player[player, i]) - 47 > 0)
                    point += (number_of_card(manager.player[player, i]) - 47);
            }
            return point;
        }
        print("wrong color");
        return -1;
    }
}

