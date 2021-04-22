
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class buttoncontrol : MonoBehaviour
{
    
    public void on_start_button()
    {
        gamemanager.manager.playing_player = 0;
        GameObject.Find("startbutton").SetActive(false);
        GameObject.Find("settingbutton").SetActive(false);
        gamemanager.manager.inactive_button();
        gamemanager.manager.table.SetActive(true);
        gamemanager.manager.team_red.SetActive(true);
        gamemanager.manager.team_blue.SetActive(true);
        gamemanager.manager.pointer[0].SetActive(true);
        gamemanager.manager.bride_game.SetActive(false);
        gamemanager.manager.Startcard();

    }
    public void on_setting_button()
    {

    }

    public void on_restart_button()
    {
        gamemanager.manager.restart_card();
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
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
        gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 1) % 4;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            gamemanager.manager.color_button[i].SetActive(false);
        }
        if (gamemanager.manager.pass == 3)
        {
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
            gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
            gamemanager.manager.call_card_finish = true;
            gamemanager.manager.destroy_button();
            if (gamemanager.manager.temp_king == 0)
            {
                gamemanager.manager.restart_card();
            }
            gamemanager.manager.after_call_card();
        }
        
    }
}
