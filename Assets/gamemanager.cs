using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager manager;
    public GameObject table, team_red, team_blue;
    public GameObject[] Poker, number_button, color_button,pointer;
    public GameObject[,] player, player_in_game;
    public Button startgame;
    public Vector2 mousepos;
    public Vector2[] cardnormalscale, cardchoosescale;

    //�|�쪱�a����P�`���z���סB�̥��䪺��P����V��m(���a0�B2��x�b�j�p�A���a1�B3��y�b�j�p)�B�|�쪱�a��P���a�V��m(���a0�B2��y�b�j�p�A���a1�B3��x�b�j�p)
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;
    public int[] handcardnum;//�|�쪱�a����P�ƶq
    public int red_score, blue_score,red_goal_score,blue_goal_score;//�ⶤ�Y���[�ơB�ⶤ�ؼм[��
    public int call_number, want_number;//�۵P��U�s���Ʀr�ݳ̲׳ۨ쪺�Ʀr�A�Ȯɳۨ쪺�Ʀr
    public int pass,calling_player,temp_king,want_king,click_number;//��pass�����ơB���b�۵P�����a�B�Ȯɳۨ쪺���B�۵P��U�n�s�����B�I���C����s������
    public string king;//�̲׳ۨ쪺���

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
        player_in_game = new GameObject[4, 13];

        call_number = 0;
        temp_king = 0;
        pass = 0;
        calling_player = 0;
        click_number = 0;
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
    public void on_start_button()
    {
        GameObject.Find("startbutton").SetActive(false);
        GameObject.Find("settingbutton").SetActive(false);
        inactive_button();
        manager.table.SetActive(true);
        manager.team_red.SetActive(true);
        manager.team_blue.SetActive(true);
        manager.pointer[0].SetActive(true);
        manager.Startcard();
    }
    public void on_setting_button()
    {

    }

    public void on_restart_button()
    {
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                DestroyImmediate(manager.player[i, k]);
        inactive_button();
        manager.table.SetActive(true);
        manager.team_red.SetActive(true);
        manager.team_blue.SetActive(true);
        manager.pointer[0].SetActive(true);
        manager.Startcard();
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
    public void gameplay()
    {

    }
    public void card_compare()
    {

    }

    public void on_num_button1()
    {
        want_number = 1;
        for (int i = 0; i < manager.temp_king; i++)
            color_button[i].SetActive(false);
    }
    public void on_num_button2()
    {
        want_number = 2;
        if (manager.call_number == 2)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_num_button3()
    {
        want_number = 3;
        if (manager.call_number == 3)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_num_button4()
    {
        want_number = 4;
        if (manager.call_number == 4)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_num_button5()
    {
        want_number = 5;
        if (manager.call_number == 5)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_num_button6()
    {
        want_number = 6;
        if (manager.call_number == 6)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_num_button7()
    {
        want_number = 7;
        if (manager.call_number == 7)
        {
            for (int i = 0; i < manager.temp_king; i++)
                color_button[i].SetActive(false);
        }

    }
    public void on_clover_button()
    {
        manager.call_number = manager.want_number;
        manager.temp_king = 1;
        for (int i = 0; i < manager.call_number - 1; i++)
            manager.number_button[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            manager.color_button[i].SetActive(true);
        manager.pass = 0;
    }
    public void on_diamond_button()
    {
        temp_king = 2;
        manager.call_number = want_number;

        for (int i = 0; i < manager.call_number - 1; i++)
            number_button[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            manager.color_button[i].SetActive(true);
        manager.pass = 0;
    }
    public void on_heart_button()
    {
        temp_king = 3;
        manager.call_number = want_number;
 
        for (int i = 0; i < manager.call_number - 1; i++)
            number_button[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            manager.color_button[i].SetActive(true);
        manager.pass = 0;
    }
    public void on_spade_button()
    {
        temp_king = 4;
        manager.call_number = want_number;
        for (int i = 0; i < manager.call_number; i++)
            number_button[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            manager.color_button[i].SetActive(true);
        manager.pass = 0;
    }
    public void on_pass_button()
    {
        pass++;
        for (int i = 0; i < 4; i++)
        {
            color_button[i].SetActive(true);
        }
    }
    public void inactive_button()
    {
        for (int i = 0; i < 4; i++)
        {
            color_button[i].SetActive(true);
        }
        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(true);
        }
    }
    public void destroy_button()
    {
        for (int i = 0; i < 4; i++)
        {
            color_button[i].SetActive(false);
        }
        for (int i = 0; i < 8; i++)
        {
            number_button[i].SetActive(false);
        }
    }
    public void on_all_color_button() 
    {
        manager.click_number++;
        if (manager.click_number % 4 == 0)
        {
            manager.pointer[0].SetActive(true);
            manager.pointer[3].SetActive(false);
        }
        if (manager.click_number % 4 == 1)
        {
            manager.pointer[1].SetActive(true);
            manager.pointer[0].SetActive(false);
        }
        if (manager.click_number % 4 == 2)
        {
            manager.pointer[2].SetActive(true);
            manager.pointer[1].SetActive(false);
        }
        if (manager.click_number % 4 == 3)
        {
            manager.pointer[3].SetActive(true);
            manager.pointer[2].SetActive(false);
        }
        if(manager.pass==3)
        {
            destroy_button();
            if (manager.temp_king == 1)
                manager.king = "clover";
            if (manager.temp_king == 2)
                manager.king = "diamond";
            if (manager.temp_king == 3)
                manager.king = "heart";
            if (manager.temp_king == 4)
                manager.king = "spade";
            if (manager.click_number % 4 == 0)
            {
                manager.red_goal_score = manager.call_number + 6;
                manager.blue_goal_score = 14 - manager.red_goal_score;
                manager.pointer[3].SetActive(true);
                manager.pointer[0].SetActive(false);
            }
            else if (manager.click_number % 4 == 1)
            {
                manager.blue_goal_score = manager.call_number + 6;
                manager.red_goal_score = 14 - manager.blue_goal_score;
                manager.pointer[0].SetActive(true);
                manager.pointer[1].SetActive(false);
            }
            else if (manager.click_number % 4 == 2)
            {
                manager.red_goal_score = manager.call_number + 6;
                manager.blue_goal_score = 14 - manager.red_goal_score;
                manager.pointer[1].SetActive(true);
                manager.pointer[2].SetActive(false);
            }
            else
            {
                manager.blue_goal_score = manager.call_number + 6;
                manager.red_goal_score = 14 - manager.blue_goal_score;
                manager.pointer[2].SetActive(true);
                manager.pointer[3].SetActive(false);
            }

        }
    }
}

