using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_control : MonoBehaviour
{
    float i = 0;
    public static bool[] ontable1 = new bool[13];
    public static bool[] ontable2 = new bool[13];
    public static bool[] ontable3 = new bool[13];

    public void player1_call_card()
    {
        AI_call_card(1);
    }
    public void player2_call_card()
    {
        AI_call_card(2);
    }
    public void player3_call_card()
    {
        AI_call_card(3);
    }
    public void player1_play_card()
    {
        int decide_card_index=0;
        GameObject decide_card;
        GameObject[] legal_card=new GameObject[13];
        int j = 0;
        for (int i = 0; i < 13; i++)
        {
            if (gamemanager.manager.can_be_select(ontable1[i], gamemanager.manager.player[1, i]) == true)
            {
                legal_card[j] = gamemanager.manager.player[1, i];
                j++;
            }
        }//�T�w���ǵP�O�i�H����
        decide_card = legal_card[decide_card_index];//�M�w�n�����i�P
        print(decide_card);
        print(gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card));
        ontable1[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card).y] = true;
        decide_card.GetComponent<BoxCollider2D>().enabled = false;
        gamemanager.manager.handcardnum[1]--;
        for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, decide_card).y; k < gamemanager.manager.handcardnum[1]; k++)
            gamemanager.manager.player_in_game[1, k] = gamemanager.manager.player_in_game[1, k + 1];
        for (int i = 0; i < gamemanager.manager.handcardnum[1]; i++)
            gamemanager.manager.player_in_game[1, i].transform.position = new Vector2(gamemanager.manager.cardverticalpos[1], gamemanager.manager.leftcardhorizonpos[1] + gamemanager.manager.handcardlength[1] / 24 * (13 - gamemanager.manager.handcardnum[1]) + gamemanager.manager.handcardlength[1] / 12 * i);
        decide_card.transform.position = new Vector2(1.5f, 0);
        decide_card.transform.localScale = gamemanager.manager.table_card_scale;
        gamemanager.manager.table_card[gamemanager.manager.table_card_number] = decide_card;//�N�ӱi�P����x�s�ୱ�W���P���}�C
        gamemanager.manager.table_card_number++;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
        gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
        if (gamemanager.manager.table_card_number == 1)
        {
            if (decide_card.tag != "kingcolor")
            {
                gamemanager.manager.temp_color = decide_card.tag;
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == gamemanager.manager.temp_color)
                        {
                            gamemanager.manager.player[i, k].tag = "mastercolor";
                        }
                gamemanager.manager.must_color = decide_card.tag;
            }//�p�G���i�P����⤣�O���A���i�P������ܦ�mastercolor�A�j�a���o�X���Ӫ��
            else//�p�G���i�P�����O���A�j�a���o�X��
                gamemanager.manager.must_color = decide_card.tag;
        }//���X���P�O�Ĥ@�i�P

        if (gamemanager.manager.table_card_number == 4)
        {
            gamemanager.manager.card_compare();
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < 13; k++)
                    if (gamemanager.manager.player[i, k].tag == "mastercolor")
                        gamemanager.manager.player[i, k].tag = gamemanager.manager.temp_color;
            Invoke("put_card_on_table", 1f);

            //�j2���ୱ���P�^���A�P���ܦ^0
        }

        }
    public void player2_play_card()
    {

        int decide_card_index = 0;
        GameObject decide_card;
        GameObject[] legal_card = new GameObject[13];
        int j = 0;

        for (int i = 0; i < 13; i++)
        {
            if (gamemanager.manager.can_be_select(ontable2[i], gamemanager.manager.player[2, i]) == true)
            {
                legal_card[j] = gamemanager.manager.player[2, i];
                j++;
            }
        }//�T�w���ǵP�O�i�H����

        decide_card = legal_card[decide_card_index];//�M�w�n�����i�P
        print(decide_card);
        print(gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card));
        ontable2[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card).y] = true;
        decide_card.GetComponent<BoxCollider2D>().enabled = false;
        gamemanager.manager.handcardnum[2]--;
            for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, decide_card).y; k < gamemanager.manager.handcardnum[2]; k++)
                gamemanager.manager.player_in_game[2, k] = gamemanager.manager.player_in_game[2, k + 1];
            for (int i = 0; i < gamemanager.manager.handcardnum[2]; i++)
                gamemanager.manager.player_in_game[2, i].transform.position = new Vector2(gamemanager.manager.leftcardhorizonpos[2] - gamemanager.manager.handcardlength[2] / 24 * (13 - gamemanager.manager.handcardnum[2]) - gamemanager.manager.handcardlength[2] / 12 * i, gamemanager.manager.cardverticalpos[2]);

            decide_card.transform.position = new Vector2(0, 1.7f);
            decide_card.transform.localScale = gamemanager.manager.table_card_scale;
        gamemanager.manager.table_card[gamemanager.manager.table_card_number] = decide_card;//�N�ӱi�P����x�s�ୱ�W���P���}�C
        gamemanager.manager.table_card_number++;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
        gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
        if (gamemanager.manager.table_card_number == 1)
        {
            if (decide_card.tag != "kingcolor")
            {
                gamemanager.manager.temp_color = decide_card.tag;
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == gamemanager.manager.temp_color)
                        {
                            gamemanager.manager.player[i, k].tag = "mastercolor";
                        }
                gamemanager.manager.must_color = decide_card.tag;
            }//�p�G���i�P����⤣�O���A���i�P������ܦ�mastercolor�A�j�a���o�X���Ӫ��
            else//�p�G���i�P�����O���A�j�a���o�X��
                gamemanager.manager.must_color = decide_card.tag;
        }//���X���P�O�Ĥ@�i�P

        if (gamemanager.manager.table_card_number == 4)
        {

            gamemanager.manager.card_compare();
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < 13; k++)
                    if (gamemanager.manager.player[i, k].tag == "mastercolor")
                        gamemanager.manager.player[i, k].tag = gamemanager.manager.temp_color;
            Invoke("put_card_on_table", 1f);

            //�j2���ୱ���P�^���A�P���ܦ^0
        }

    



}
    public void player3_play_card()
    {
        int decide_card_index = 0;
        GameObject decide_card;
        GameObject[] legal_card = new GameObject[13];
        int j = 0;

        for (int i = 0; i < 13; i++)
        {
            if (gamemanager.manager.can_be_select(ontable3[i], gamemanager.manager.player[3, i]) == true)
            {
                legal_card[j] = gamemanager.manager.player[3, i];
                j++;
            }
        }//�T�w���ǵP�O�i�H����
        
        decide_card = legal_card[decide_card_index];//�M�w�n�����i�P
        print(decide_card);
        print(gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card));
        ontable3[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, decide_card).y] = true;
        decide_card.GetComponent<BoxCollider2D>().enabled = false;
        gamemanager.manager.handcardnum[3]--;
        for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, decide_card).y; k < gamemanager.manager.handcardnum[3]; k++)
            gamemanager.manager.player_in_game[3, k] = gamemanager.manager.player_in_game[3, k + 1];
        for (int i = 0; i < gamemanager.manager.handcardnum[3]; i++)
            gamemanager.manager.player_in_game[3, i].transform.position = new Vector2(gamemanager.manager.cardverticalpos[3], gamemanager.manager.leftcardhorizonpos[3] - gamemanager.manager.handcardlength[3] / 24 * (13 - gamemanager.manager.handcardnum[3]) - gamemanager.manager.handcardlength[3] / 12 * i);
        decide_card.transform.position = new Vector2(-1.5f, 0);
        decide_card.transform.localScale = gamemanager.manager.table_card_scale;
        gamemanager.manager.table_card[gamemanager.manager.table_card_number] = decide_card;//�N�ӱi�P����x�s�ୱ�W���P���}�C
        gamemanager.manager.table_card_number++;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
        gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
        if (gamemanager.manager.table_card_number == 1)
        {
            if (decide_card.tag != "kingcolor")
            {
                gamemanager.manager.temp_color = decide_card.tag;
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == gamemanager.manager.temp_color)
                        {
                            gamemanager.manager.player[i, k].tag = "mastercolor";
                        }
                gamemanager.manager.must_color = decide_card.tag;
            }//�p�G���i�P����⤣�O���A���i�P������ܦ�mastercolor�A�j�a���o�X���Ӫ��
            else//�p�G���i�P�����O���A�j�a���o�X��
                gamemanager.manager.must_color = decide_card.tag;
        }//���X���P�O�Ĥ@�i�P

        if (gamemanager.manager.table_card_number == 4)
        {
            gamemanager.manager.card_compare();
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < 13; k++)
                    if (gamemanager.manager.player[i, k].tag == "mastercolor")
                        gamemanager.manager.player[i, k].tag = gamemanager.manager.temp_color;
            Invoke("put_card_on_table",1f);

            //�j2���ୱ���P�^���A�P���ܦ^0
        }
    }


    public void Update()
    {
        //�]�wAI�۰ʥX�P�����j�ɶ�
        if (gamemanager.manager.call_card_finish == false)
        {
            if (gamemanager.manager.playing_player == 1)
            {
                while (true)
                {
                    if (i > 1)
                        player1_call_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
            if (gamemanager.manager.playing_player == 2)
            {
                while (true)
                {
                    if (i > 1)
                        player2_call_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
            if (gamemanager.manager.playing_player == 3)
            {
                while (true)
                {
                    if (i > 1)
                        player3_call_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
        }
        else
        {
            if (gamemanager.manager.playing_player == 1 )
            {
                if (gamemanager.manager.table_card_number == 4)
                    if (gamemanager.manager.table_card[3].activeSelf==true)
                        return;
                while (true)
                {
                    if (i > 1)
                        player1_play_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
            if (gamemanager.manager.playing_player == 2 )
            {
                if (gamemanager.manager.table_card_number == 4)
                    if (gamemanager.manager.table_card[3].activeSelf == true)
                        return;
                while (true)
                {
                    if (i > 1)
                        player2_play_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
            if (gamemanager.manager.playing_player == 3 )
            {
                if (gamemanager.manager.table_card_number == 4)
                    if (gamemanager.manager.table_card[3].activeSelf == true)
                        return;
                while (true)
                {
                    if (i > 1)
                        player3_play_card();
                    else
                    {
                        break;
                    }
                    i = 0;
                    break;
                }
                i += Time.deltaTime;
            }
        }

    }
    public void AI_call_card(int player)
    {
        int[] color_amount = new int[4];

        //�����Ӫ��a�U�ت�⦳�X�i
        for (int k = 0; k < 4; k++)
        {
            color_amount[k] = gamemanager.manager.compute_color(gamemanager.manager.transform_num_to_color(k + 1), player);
        }

        //�p�G�i�H�ۨ�̤p���Ʀr�A�B�Ӫ��j��3�i�N�ۡA�B�}��⦳�ܤ֤T�I
        for (int k = gamemanager.manager.temp_king; k < 4; k++)
        {
            if (color_amount[k] > 3 && gamemanager.manager.call_number < 3 && gamemanager.manager.count_card_point(player, gamemanager.manager.transform_num_to_color(k+1)) > 2)
            {
                gamemanager.manager.number_button[gamemanager.manager.call_number - 1].GetComponent<Button>().onClick.Invoke();
                gamemanager.manager.color_button[k].GetComponent<Button>().onClick.Invoke();
                return;
            }
        }

        //�p�G�Y��⦳�ܤ֤��i�A�B�Ӫ��ܤ֦�3�I�A�B�Ӫ���ثe�۪���⤣�@�ˡA�N�ۤW�h2
        for (int k = 0; k < 4; k++)
        {
            if (color_amount[k] > 4 
                && gamemanager.manager.call_number < 2
                && gamemanager.manager.count_card_point(player, gamemanager.manager.transform_num_to_color(k + 1)) > 2
                &&k+1!=gamemanager.manager.temp_king)
            {
                gamemanager.manager.number_button[gamemanager.manager.call_number].GetComponent<Button>().onClick.Invoke();
                gamemanager.manager.color_button[k].GetComponent<Button>().onClick.Invoke();
                return;
            }
        }

        //�p�G�Y��⦳�ܤ֤��i�A�B�Ӫ��ܤ֦�4�I�A�B�Ӫ���ثe�۪���⤣�@�ˡA�N�ۤW�h3
        for (int k = 0; k < 4; k++)
        {
            if (color_amount[k] > 5
                && gamemanager.manager.call_number < 3
                && gamemanager.manager.count_card_point(player, gamemanager.manager.transform_num_to_color(k + 1)) > 3
                && k + 1!= gamemanager.manager.temp_king)
            {
                gamemanager.manager.number_button[gamemanager.manager.call_number].GetComponent<Button>().onClick.Invoke();
                gamemanager.manager.color_button[k].GetComponent<Button>().onClick.Invoke();
                return;
            }
        }

        gamemanager.manager.color_button[5].GetComponent<Button>().onClick.Invoke();//���S�F���Npass
    }//AI�s�P����
    public void put_card_on_table()
    {
        for (int i = 0; i < 4; i++)
            gamemanager.manager.table_card[i].SetActive(false);
        gamemanager.manager.table_card_number = 0;
        //��쥻tag�]��mastercolor���P��tag�]�^��
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                if (gamemanager.manager.player[i, k].tag == "mastercolor")
                    gamemanager.manager.player[i, k].tag = gamemanager.manager.temp_color;

        //�p�G�Y���F��ؼФ��ơA�����C���A��ܹC�����G
        if (gamemanager.manager.red_score == gamemanager.manager.red_goal_score)
        {
            gamemanager.manager.win_panel.SetActive(true);
            GameObject.Find("winner").GetComponent<Text>().text = "�����ӧQ";
            gamemanager.manager.game_finish();
        }
        if (gamemanager.manager.blue_score == gamemanager.manager.blue_goal_score)
        {
            gamemanager.manager.win_panel.SetActive(true);
            GameObject.Find("winner").GetComponent<Text>().text = "�Ŷ��ӧQ";
            gamemanager.manager.game_finish();
        }

        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);

    }

}