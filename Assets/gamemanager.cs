using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager manager;
    public GameObject table, team_red, team_blue,win_panel, red_goal, red_team_score, blue_team_score, blue_goal, restart_button;//�C���e���W����l�B��ܶ�����ƪ���r�B��Ӫ��e���B�ⶤ�ؼФ��ƻP��o���ƪ���ܤ�rUI�B�C���i�椤�����s�C�����s
    public GameObject[] Poker, number_button, color_button,pointer,table_card;//�x�s�Ҧ����J�P�������}�C�B�ۼƦr�����s�B�۪�⪺���s�B���b��W���P
    public GameObject[,] player, player_in_game;
    public Button startgame;
    public Vector2 mousepos;
    public Vector2[] cardnormalscale, cardchoosescale;
    public int[] card_number;//����

    //�|�쪱�a����P�`���z���סB�̥��䪺��P����V��m(���a0�B2��x�b�j�p�A���a1�B3��y�b�j�p)�B�|�쪱�a��P���a�V��m(���a0�B2��y�b�j�p�A���a1�B3��x�b�j�p)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;
    public int[] handcardnum;//�|�쪱�a����P�ƶq
    public int red_score, blue_score,red_goal_score,blue_goal_score;//�ⶤ�Y���[�ơB�ⶤ�ؼм[��
    public int call_number, want_number;//�۵P��U�s���Ʀr�ݳ̲׳ۨ쪺�Ʀr�A�Ȯɳۨ쪺�Ʀr
    public int pass,temp_king,want_king,click_number;//��pass�����ơB���b�۵P�����a�B�Ȯɳۨ쪺���B�۵P��U�n�s�����B�I���C����s������
    public int table_card_number,playing_player;//��W�w�����P�ơB���찵�ƪ����a
    public string king;//�̲׳ۨ쪺���
    public bool call_card_finish;
    public string must_color;//��P�����N�@�w�n���X�����


    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
    }

    public void Startcard()
    {
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
        click_number = 0;
        table_card_number = 0;
        call_card_finish = false;
        for (i = 0; i < 52; i++) 
            if (i < 13)
                Poker[i].tag = "clover";
            else if (i < 26)
                Poker[i].tag = "diamond";
            else if (i < 39)
                Poker[i].tag = "heart";
            else
                Poker[i].tag = "spade";
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
            }
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
                player[k, i].AddComponent<mousecontrol>();
                player_in_game[k, i] = player[k, i];
            }
        }
    }
    public void call_card()
    {
        pass = 0;
    }
    
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
    }
    public void red_getscore()
    {
        manager.red_score++;
        GameObject.Find("teamred").GetComponent<Text>().text = "�����[�ơG" + manager.red_score;
    }
    public void blue_getscore()
    {
        manager.blue_score++;
        GameObject.Find("teamblue").GetComponent<Text>().text = "�Ŷ��[�ơG" + manager.blue_score;
    }
    public void reset_score()
    {
        GameObject.Find("teamblue").GetComponent<Text>().text = "�Ŷ��[�ơG" + 0;
        GameObject.Find("teamred").GetComponent<Text>().text = "�����[�ơG" + 0;
    }
    public void gameplay()
    {

    }

    public int number_of_card(GameObject card)
    {
        for (int i = 0; i < 52; i++)
            if (Poker[i].name + "(Clone)" == card.name)
                return i;
        return -1;
    }
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

        if (cardtoplayer(player, table_card[3]).x == 0|| cardtoplayer(player, table_card[3]).x == 2)
        {
            red_getscore();
            pointer[playing_player].SetActive(false);
            if (cardtoplayer(player, table_card[3]).x == 0)
            {
                playing_player = 0;
                pointer[0].SetActive(true);
            }
            else
            {
                playing_player = 2;
                pointer[2].SetActive(true);
            }
        }
        else
        {
            blue_getscore();
            pointer[playing_player].SetActive(false);
            if (cardtoplayer(player, table_card[3]).x == 1)
            {
                playing_player = 1;
                pointer[1].SetActive(true);
            }
            else
            {
                playing_player = 3;
                pointer[3].SetActive(true);
            }
        }

    }

   
    public void inactive_button()
    {

        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(true);
        }
        restart_button.SetActive(true);
    }
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
    }
    public int compute_color(string color,GameObject a) 
    {
        int num=0,player_num= (int)cardtoplayer(player, a).x;
        for (int i = 0; i < handcardnum[player_num]; i++)
        {
            if (player_in_game[player_num, i].tag == color)
                num++;
        }
        return num;
    }
}

