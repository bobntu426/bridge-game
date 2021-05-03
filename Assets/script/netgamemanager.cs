using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class netgamemanager : NetworkBehaviour

{
    public static netgamemanager netmanager;
    //�C���e���W����l�B��ܶ�����ƪ���r�B��Ӫ��e���B�ⶤ�ؼФ��ƻP��o���ƪ���ܤ�rUI�B�C���i�椤�����s�C�����s
    public GameObject table, team_red, team_blue, win_panel, red_goal, red_team_score, blue_team_score, blue_goal, restart_button, bride_game

        //�C���i�椤���ⶤ�ؼм[�Ƥ�rUI�B��ܤ�����rUI�B�޲zAI�s�P������BAI�Ҧ������s�B��H���a�Ҧ������s�B��^�D���������s�B�}�l�����s�B�C���]�w���s
        , red_goal_UI, blue_goal_UI, king_UI, AI_control, AI_pointer, AI_mode_button, one_player_button, return_button, startbutton, setting_button;

    //�x�s�Ҧ����J�P�������}�C�B�ۼƦr�����s�B�۪�⪺���s�B���b��W���P�B�U���a�ۤF�ƻ���ܦr��UI�B���player��r��UI
    public GameObject[] Poker, number_button, color_button, pointer, table_card, player_call_card, playerUI;

    public GameObject[,] player, player_in_game;//�����U���a�֦����P�B�C�����|�H�۪��a�X�P���ܰʪ��P�}�C
    public Button startgame;//�}�l�C�������s
    public Vector2 mousepos, table_card_scale;//�ƹ��y�СB�P�b�ୱ�W�ɷ|�ܦ����j�p
    public Vector2[] cardnormalscale, cardchoosescale;//�U�i�P�b�|�쪱�a�������j�p�B�Q����ɪ��j�p
    

    //�|�쪱�a����P�`���z���סB�̥��䪺��P����V��m(���a0�B2��x�b�j�p�A���a1�B3��y�b�j�p)�B�|�쪱�a��P���a�V��m(���a0�B2��y�b�j�p�A���a1�B3��x�b�j�p)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;
    public int[] handcardnum,card_number;//�|�쪱�a����P�ƶq�B�P��W��m���P
    [SyncVar]
    public int red_score, blue_score, red_goal_score, blue_goal_score, call_number, want_number,pass, temp_king, want_king, table_card_number, playing_player;
    //�ⶤ�Y���[�ơB�ⶤ�ؼм[�ơB�۵P��U�s���Ʀr�ݳ̲׳ۨ쪺�Ʀr�A�Ȯɳۨ쪺�Ʀr�B��pass�����ơB���b�۵P�����a�B�Ȯɳۨ쪺���B�۵P��U�n�s�����B��W�w�����P�ơB���찵�ƪ����a
    [SyncVar]
    public bool call_card_finish;//�O�_�۵P�����F�B�O�_�O�q���۰ʥX�P�Ҧ��B�O�_�O�����s�u�Ҧ�
    [SyncVar]
    public string must_color, temp_color,king;//��P�����N�@�w�n���X�����B�Ȧs�Ϊ�����ܼơB�̲׳ۨ쪺���
    [SyncVar]
    public int g;




    private void Awake() 
    {
        if (netmanager == null)
        {
            netmanager = this;
        }
    }

    private void Start()
    {
        catch_netid.m = 0;
        catch_netid.n = 0;
        catch_netid.playerid = new uint[4, 13];
        netgamemanager.netmanager.g = 0;
        handcardnum=new int[] {0,0,0,0};
        playing_player = 0; 
        card_number= new int[] { 0, 0, 0, 0 };
    }


    private void Update()
    {
        
    }
    public void start_card()
    {
        //�N�U���ܼƭ��s��s
        short[,] playerindex = new short[4, 13];
        short[] index = new short[52];
        short b, k, temp, i, f, rcard;
        player = new GameObject[4, 13];
        rcard = 52;
        handcardnum = new int[4];
        table_card = new GameObject[4];
        player_in_game = new GameObject[4, 13];
        call_number = 1;
        temp_king = 0;
        pass = 0;
        table_card_number = 0;
        call_card_finish = false;
        blue_score = 0;
        red_score = 0;
        catch_netid.playerid = new uint[4, 13];
        catch_netid.m = 0;
        catch_netid.n = 0;



        /*for (i = 0; i < 4; i++)
        {
            playerUI[i].SetActive(true);
            player_call_card[i].SetActive(true);
            player_call_card[i].GetComponent<Text>().text = "�|���۵P";
        }//���s�U���a���۵P���pUI*/
        red_goal_UI.SetActive(false);
        blue_goal_UI.SetActive(false);
        king_UI.SetActive(false);
        color_distribute();
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
        }//��C�쪱�a�⤤���P��index�Ƨ�
        if (isServer)
        {
            for (k = 0; k < 4; k++)
            {
                for (i = 0; i < 13; i++)
                {

                    player[k, i] = Instantiate(Poker[playerindex[k, i]], new Vector2(-0.63f, 0.09f), Quaternion.identity);
                    NetworkServer.Spawn(player[k, i]);
                }
            }
            table=Instantiate(table, new Vector2(0,0), Quaternion.identity);
            NetworkServer.Spawn(table);
        }


    }
    public void start_card_2()
    {
        int k, i;
        for (k = 0; k < 4; k++)
        {
            
            for (i = 0; i < 13; i++)
            {

                player[k, i] = NetworkIdentity.spawned[catch_netid.playerid[k, i]].gameObject;
                player_in_game[k, i] = player[k, i];
            }

        }
        table= NetworkIdentity.spawned[catch_table.id].gameObject; 
        /*for (i = 1; i < 4; i++)
        {
            if (count_card_point(i, "clover") + count_card_point(i, "diamond") + count_card_point(i, "heart") + count_card_point(i, "spade") < 4)
                restart_button.GetComponent<Button>().onClick.Invoke();
        }//�˵P����*/

    }
    public int count_card_point(int player, string color)
    {
        netgamemanager a = GetComponent<netgamemanager>();
        int point = 0;
        if (color == "clover")
        {
            for (int i = 0; i < 13; i++)
            {
                if (gamemanager.manager.number_of_card(a.player[player, i]) >= 0 && gamemanager.manager.number_of_card(a.player[player, i]) < 13 && gamemanager.manager.number_of_card(a.player[player, i]) - 8 > 0)
                    point += (gamemanager.manager.number_of_card(a.player[player, i]) - 8);
            }
            return point;
        }
        else if (color == "diamond")
        {

            for (int i = 0; i < 13; i++)
            {
                if (gamemanager.manager.number_of_card(a.player[player, i]) >= 13 && gamemanager.manager.number_of_card(a.player[player, i]) < 26 && gamemanager.manager.number_of_card(a.player[player, i]) - 21 > 0)
                    point += (gamemanager.manager.number_of_card(a.player[player, i]) - 21);
            }
            return point;
        }
        else if (color == "heart")
        {
            for (int i = 0; i < 13; i++)
            {
                if (gamemanager.manager.number_of_card(a.player[player, i]) >= 26 && gamemanager.manager.number_of_card(a.player[player, i]) < 39 && gamemanager.manager.number_of_card(a.player[player, i]) - 34 > 0)
                    point += (gamemanager.manager.number_of_card(a.player[player, i]) - 34);
            }
            return point;
        }
        else if (color == "spade")
        {
            for (int i = 0; i < 13; i++)
            {
                if (gamemanager.manager.number_of_card(a.player[player, i]) >= 39 && gamemanager.manager.number_of_card(a.player[player, i]) < 52 && gamemanager.manager.number_of_card(a.player[player, i]) - 47 > 0)
                    point += (gamemanager.manager.number_of_card(a.player[player, i]) - 47);
            }
            return point;
        }
        print("wrong color");
        return -1;
    }
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



}
