using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttoncontrol : MonoBehaviour
{
    public void on_start_button()
    {
        GameObject.Find("startbutton").SetActive(false);
        GameObject.Find("settingbutton").SetActive(false);
        gamemanager.manager.inactive_button();
        gamemanager.manager.table.SetActive(true);
        gamemanager.manager.team_red.SetActive(true);
        gamemanager.manager.team_blue.SetActive(true);
        gamemanager.manager.pointer[0].SetActive(true);
        gamemanager.manager.Startcard();
    }
    public void on_setting_button()
    {

    }

    public void on_restart_button()
    {
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 13; k++)
                DestroyImmediate(gamemanager.manager.player[i, k]);
        gamemanager.manager.inactive_button();
        gamemanager.manager.table.SetActive(true);
        gamemanager.manager.team_red.SetActive(true);
        gamemanager.manager.team_blue.SetActive(true);
        gamemanager.manager.pointer[0].SetActive(true);
        gamemanager.manager.pointer[1].SetActive(false);
        gamemanager.manager.pointer[2].SetActive(false);
        gamemanager.manager.pointer[3].SetActive(false);
        gamemanager.manager.Startcard();
        gamemanager.manager.reset_score();
    }
    public void on_num_button1()
    {
        gamemanager.manager.want_number = 1;

    }
    public void on_num_button2()
    {
        gamemanager.manager.want_number = 2;

    }
    public void on_num_button3()
    {
        gamemanager.manager.want_number = 3;
    }
    public void on_num_button4()
    {
        gamemanager.manager.want_number = 4;
    }
    public void on_num_button5()
    {
        gamemanager.manager.want_number = 5;
    }
    public void on_num_button6()
    {
        gamemanager.manager.want_number = 6;
    }
    public void on_num_button7()
    {
        gamemanager.manager.want_number = 7;
    }
    public void on_all_number_button()
    {
        if (gamemanager.manager.want_number == gamemanager.manager.call_number)
        {
            for (int i = 4; i > gamemanager.manager.temp_king - 1; i--)
                gamemanager.manager.color_button[i].SetActive(true);
            for (int i = 0; i < gamemanager.manager.temp_king; i++)
                gamemanager.manager.color_button[i].SetActive(false);
        }
        else
            for (int i = 0; i < 5; i++)
                gamemanager.manager.color_button[i].SetActive(true);

    }
    public void on_clover_button()
    {
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        gamemanager.manager.temp_king = 1;
        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_diamond_button()
    {
        gamemanager.manager.temp_king = 2;
        gamemanager.manager.call_number = gamemanager.manager.want_number;

        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_heart_button()
    {
        gamemanager.manager.temp_king = 3;
        gamemanager.manager.call_number = gamemanager.manager.want_number;

        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_spade_button()
    {
        gamemanager.manager.temp_king = 4;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }

    public void on_no_king_button()
    {
        gamemanager.manager.temp_king = 5;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        for (int i = 0; i < gamemanager.manager.call_number; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_pass_button()
    {
        gamemanager.manager.pass++;
    }
    public void on_all_color_button()
    {
        for (int i = 0; i < 5; i++)
        {
            gamemanager.manager.color_button[i].SetActive(false);
        }
        gamemanager.manager.click_number++;
        if (gamemanager.manager.click_number % 4 == 0)
        {
            gamemanager.manager.pointer[0].SetActive(true);
            gamemanager.manager.pointer[3].SetActive(false);
        }
        if (gamemanager.manager.click_number % 4 == 1)
        {
            gamemanager.manager.pointer[1].SetActive(true);
            gamemanager.manager.pointer[0].SetActive(false);
        }
        if (gamemanager.manager.click_number % 4 == 2)
        {
            gamemanager.manager.pointer[2].SetActive(true);
            gamemanager.manager.pointer[1].SetActive(false);
        }
        if (gamemanager.manager.click_number % 4 == 3)
        {
            gamemanager.manager.pointer[3].SetActive(true);
            gamemanager.manager.pointer[2].SetActive(false);
        }
        if (gamemanager.manager.pass == 3)
        {
            gamemanager.manager.destroy_button();
            if (gamemanager.manager.temp_king == 1)
                gamemanager.manager.king = "clover";
            if (gamemanager.manager.temp_king == 2)
                gamemanager.manager.king = "diamond";
            if (gamemanager.manager.temp_king == 3)
                gamemanager.manager.king = "heart";
            if (gamemanager.manager.temp_king == 4)
                gamemanager.manager.king = "spade";
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < 13; k++)
                    if (gamemanager.manager.player[i, k].tag == gamemanager.manager.king)
                        gamemanager.manager.player[i, k].tag = "kingcolor";
            if (gamemanager.manager.click_number % 4 == 0)
            {
                gamemanager.manager.red_goal_score = gamemanager.manager.call_number + 6;
                gamemanager.manager.blue_goal_score = 14 - gamemanager.manager.red_goal_score;
                gamemanager.manager.pointer[3].SetActive(true);
                gamemanager.manager.pointer[0].SetActive(false);
            }
            else if (gamemanager.manager.click_number % 4 == 1)
            {
                gamemanager.manager.blue_goal_score = gamemanager.manager.call_number + 6;
                gamemanager.manager.red_goal_score = 14 - gamemanager.manager.blue_goal_score;
                gamemanager.manager.pointer[0].SetActive(true);
                gamemanager.manager.pointer[1].SetActive(false);
            }
            else if (gamemanager.manager.click_number % 4 == 2)
            {
                gamemanager.manager.red_goal_score = gamemanager.manager.call_number + 6;
                gamemanager.manager.blue_goal_score = 14 - gamemanager.manager.red_goal_score;
                gamemanager.manager.pointer[1].SetActive(true);
                gamemanager.manager.pointer[2].SetActive(false);
            }
            else
            {
                gamemanager.manager.blue_goal_score = gamemanager.manager.call_number + 6;
                gamemanager.manager.red_goal_score = 14 - gamemanager.manager.blue_goal_score;
                gamemanager.manager.pointer[2].SetActive(true);
                gamemanager.manager.pointer[3].SetActive(false);
            }

        }
    }
}
