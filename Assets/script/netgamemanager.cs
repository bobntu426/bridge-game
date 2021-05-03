using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class netgamemanager : NetworkBehaviour

{
    public static netgamemanager netmanager;
    //遊戲畫面上的桌子、顯示隊伍分數的文字、獲勝的畫布、兩隊目標分數與獲得分數的顯示文字UI、遊戲進行中的重新遊戲按鈕
    public GameObject table, team_red, team_blue, win_panel, red_goal, red_team_score, blue_team_score, blue_goal, restart_button, bride_game

        //遊戲進行中的兩隊目標墩數文字UI、顯示王的文字UI、管理AI叫牌的物件、AI模式的按鈕、單人操縱模式的按鈕、返回主頁面的按鈕、開始的按鈕、遊戲設定按鈕
        , red_goal_UI, blue_goal_UI, king_UI, AI_control, AI_pointer, AI_mode_button, one_player_button, return_button, startbutton, setting_button;

    //儲存所有撲克牌元素的陣列、喊數字的按鈕、喊花色的按鈕、打在桌上的牌、各玩家喊了甚麼的顯示字體UI、顯示player文字的UI
    public GameObject[] Poker, number_button, color_button, pointer, table_card, player_call_card, playerUI;

    public GameObject[,] player, player_in_game;//紀錄各玩家擁有的牌、遊戲中會隨著玩家出牌而變動的牌陣列
    public Button startgame;//開始遊戲的按鈕
    public Vector2 mousepos, table_card_scale;//滑鼠座標、牌在桌面上時會變成的大小
    public Vector2[] cardnormalscale, cardchoosescale;//各張牌在四位玩家中的的大小、被選取時的大小
    

    //四位玩家的手牌總物理長度、最左邊的手牌的橫向位置(玩家0、2為x軸大小，玩家1、3為y軸大小)、四位玩家手牌的縱向位置(玩家0、2為y軸大小，玩家1、3為x軸大小)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;
    public int[] handcardnum,card_number;//四位玩家的手牌數量、牌桌上放置的牌
    [SyncVar]
    public int red_score, blue_score, red_goal_score, blue_goal_score, call_number, want_number,pass, temp_king, want_king, table_card_number, playing_player;
    //兩隊吃的墩數、兩隊目標墩數、喊牌當下叫的數字兼最終喊到的數字，暫時喊到的數字、喊pass的次數、正在喊牌的玩家、暫時喊到的花色、喊牌當下要叫的花色、桌上已打的牌數、輪到做事的玩家
    [SyncVar]
    public bool call_card_finish;//是否喊牌結束了、是否是電腦自動出牌模式、是否是網路連線模式
    [SyncVar]
    public string must_color, temp_color,king;//手牌中有就一定要打出的花色、暫存用的花色變數、最終喊到的花色
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
        //將各項變數重製更新
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
            player_call_card[i].GetComponent<Text>().text = "尚未喊牌";
        }//重製各玩家的喊牌狀況UI*/
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
        }//把每位玩家手中的牌的index排序
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
        }//倒牌機制*/

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
    }//把每張牌標上花色



}
