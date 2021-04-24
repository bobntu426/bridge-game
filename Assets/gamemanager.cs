using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager manager;

    //遊戲畫面上的桌子、顯示隊伍分數的文字、獲勝的畫布、兩隊目標分數與獲得分數的顯示文字UI、遊戲進行中的重新遊戲按鈕
    public GameObject table, team_red, team_blue, win_panel, red_goal, red_team_score, blue_team_score, blue_goal, restart_button, bride_game

        //遊戲進行中的兩隊目標墩數文字UI、顯示王的文字UI、管理AI叫牌的物件
        , red_goal_UI, blue_goal_UI, king_UI,AI_control,AI_pointer;

    //儲存所有撲克牌元素的陣列、喊數字的按鈕、喊花色的按鈕、打在桌上的牌、各玩家喊了甚麼的顯示字體UI、顯示player文字的UI
    public GameObject[] Poker, number_button, color_button, pointer, table_card,player_call_card,playerUI;

    public GameObject[,] player, player_in_game;//紀錄各玩家擁有的牌、遊戲中會隨著玩家出牌而變動的牌陣列
    public Button startgame;//開始遊戲的按鈕
    public Vector2 mousepos,table_card_scale;//滑鼠座標、牌在桌面上時會變成的大小
    public Vector2[] cardnormalscale, cardchoosescale;//各張牌在四位玩家中的的大小、被選取時的大小
    public int[] card_number;//牌桌上放置的牌

    //四位玩家的手牌總物理長度、最左邊的手牌的橫向位置(玩家0、2為x軸大小，玩家1、3為y軸大小)、四位玩家手牌的縱向位置(玩家0、2為y軸大小，玩家1、3為x軸大小)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;

    public int[] handcardnum;//四位玩家的手牌數量
    public int red_score, blue_score, red_goal_score, blue_goal_score;//兩隊吃的墩數、兩隊目標墩數
    public int call_number, want_number;//喊牌當下叫的數字兼最終喊到的數字，暫時喊到的數字
    public int pass, temp_king, want_king;//喊pass的次數、正在喊牌的玩家、暫時喊到的花色、喊牌當下要叫的花色
    public int table_card_number, playing_player;//桌上已打的牌數、輪到做事的玩家
    public string king;//最終喊到的花色
    public bool call_card_finish,AI_mode;//是否喊牌結束了、是否是電腦自動出牌模式
    public string must_color, temp_color;//手牌中有就一定要打出的花色、暫存用的花色變數


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
        //將各項變數重製更新
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
            player_call_card[i].GetComponent<Text>().text = "尚未喊牌";
        }//重製各玩家的喊牌狀況UI
        red_goal_UI.SetActive(false);
        blue_goal_UI.SetActive(false);
        king_UI.SetActive(false);

        //AI模式的設定
        if (AI_mode == true)
        {
            
            AI_control.SetActive(true);
            foreach (GameObject h in number_button)
                h.GetComponent<Button>().interactable = true;
            foreach (GameObject h in color_button)
                h.GetComponent<Button>().interactable = true;
        }


        color_distribute();//把每張牌標上花色

        //隨機分配各張牌的index到各玩家手中
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
            }//把每位玩家手中的牌的index排序
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
            }//依照index把各張牌分配到各玩家手中，並設好位置、屬性
        }//把每位玩家手中的牌的index排序，並依照index把各張牌分配到各玩家手中，並設好位置、屬性

        for (i = 1; i < 4; i++)
        {
            if(count_card_point(i, "clover")+ count_card_point(i, "diamond") + count_card_point(i, "heart")+ count_card_point(i, "spade")<4)
                restart_button.GetComponent<Button>().onClick.Invoke();
        }//倒牌機制
    }//開始一場遊戲，重製各項數據，把牌發好整理到各玩家手中
    
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
    }//傳回該牌所屬的玩家編號和該牌為手牌中左邊數來第幾張牌
    public void red_getscore()
    {
        manager.red_score++;
        GameObject.Find("teamred").GetComponent<Text>().text = "紅隊墩數：" + manager.red_score;
    }//紅隊得分時要做的事
    public void blue_getscore()
    {
        manager.blue_score++;
        GameObject.Find("teamblue").GetComponent<Text>().text = "藍隊墩數：" + manager.blue_score;
    }//藍隊得分時要做的事
    public void reset_score()
    {
        GameObject.Find("teamblue").GetComponent<Text>().text = "藍隊墩數：" + 0;
        GameObject.Find("teamred").GetComponent<Text>().text = "紅隊墩數：" + 0;
    }//將兩隊墩數顯示重製為0

    public int number_of_card(GameObject card)
    {
        for (int i = 0; i < 52; i++)
            if (Poker[i].name + "(Clone)" == card.name)
                return i;
        return -1;
    }//傳回某張牌的數字
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
    }//打完一輪牌後，結算誰吃到墩，把分數計算好，開始下回合。


    public void inactive_button()
    {

        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(true);
        }
        restart_button.SetActive(true);
    }//設置開始喊牌的按鈕
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
    }//消除所有喊牌的按鈕
    public int compute_color(string color, int player)
    {
        int num = 0;
        for (int i = 0; i < handcardnum[player]; i++)
        {
            if (player_in_game[player, i].tag == color)
                num++;
        }
        return num;
    }//傳回某一花色在某一玩家手中的數量
    public void put_card_on_table(GameObject gameObject,ref bool ontable)
        {
        ontable = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        //把牌打出去後，player_in_game陣列中移除該張牌、手中剩下的牌重新喬好位置
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

        table_card[table_card_number] = gameObject;//將該張牌放到儲存桌面上的牌的陣列
        table_card_number++;
    }//把手中的牌拖拉到桌上會觸發的動作，需同時傳上該腳本的ontable參數
    public void after_call_card()
    {
        //決定王
        if (temp_king == 1)
        {
            king = "clover";
            king_UI.GetComponent<Text>().text = "王：梅花";
        }
        if (temp_king == 2)
        {
            king = "diamond";
            king_UI.GetComponent<Text>().text = "王：磚塊";
        }
        if (temp_king == 3)
        {
            king = "heart";
            king_UI.GetComponent<Text>().text = "王：愛心";
        }
        if (temp_king == 4)
        {
            king = "spade";
            king_UI.GetComponent<Text>().text = "王：黑桃";
        }
        if (temp_king == 5)
        {
            king = "mastercolor";
            king_UI.GetComponent<Text>().text = "本局無王";
        }

        //把王的花色的tag改成kingcolor
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                if (player[i, k].tag == king)
                    player[i, k].tag = "kingcolor";
        


        if (playing_player == 0 || playing_player % 4 == 2)
        {
            blue_goal_score = call_number + 6;
            red_goal_score = 14 - blue_goal_score;
        }//藍隊喊到
        else
        {
            red_goal_score = call_number + 6;
            blue_goal_score = 14 - red_goal_score;
        }//紅隊喊到
        
        //設置場景上的文字
        red_goal_UI.GetComponent<Text>().text = "紅隊目標墩數：" + red_goal_score;
        blue_goal_UI.GetComponent<Text>().text = "藍隊目標墩數：" + blue_goal_score;
        red_goal_UI.SetActive(true);
        blue_goal_UI.SetActive(true);
        king_UI.SetActive(true);
    }//喊玩牌後根據喊牌內容設定好規則
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
    }//重新開始遊戲
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
    public bool can_be_select(bool ontable ,GameObject gameObject) 
    {
        
        if (table_card_number == 4 && table_card[3].activeSelf == true)
            return false;
        if (
            ontable == false //不在桌上
            &&
            call_card_finish == true //喊完牌了才能打牌
            &&
            (
                //在特定情況下有某些花色不能打
                table_card_number == 0 //第一個人出牌可以出任何花色
                ||
                gameObject.tag == must_color //只能出跟第一個人一樣的花色
                ||

                //手中如果該花色沒了就可以切牌或墊牌
                compute_color(must_color, (int)cardtoplayer(player, gameObject).x) == 0
            )
            &&
            //輪到該玩家才能出牌
            (int)cardtoplayer(player, gameObject).x == playing_player
            )
            return true;
        else
            return false;
    }//檢測那張牌能不能打，可以的話才能選取
    public void playerUI_control(string color) 
    {
        player_call_card[(playing_player + 3) % 4].GetComponent<Text>().text = call_number + color;
        for (int i = 0; i < 3; i++)
            player_call_card[(playing_player + i) % 4].GetComponent<Text>().text = "尚未喊牌";

    }
    public void game_finish() 
    {
        AI_control.SetActive(false);
        red_goal_UI.SetActive(false);
        blue_goal_UI.SetActive(false);
        king_UI.SetActive(false);
        blue_goal.GetComponent<Text>().text = "藍隊目標墩數：" + blue_goal_score;
        red_goal.GetComponent<Text>().text = "紅隊目標墩數：" + red_goal_score;
        red_team_score.GetComponent<Text>().text = "紅隊獲得墩數：" + red_score;
        blue_team_score.GetComponent<Text>().text = "藍隊獲得墩數：" + blue_score;
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

