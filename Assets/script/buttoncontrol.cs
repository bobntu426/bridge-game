
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttoncontrol : MonoBehaviour
{
    private void Start()
    {
        netmode = false;
    }
    public bool netmode;
    public void on_start_button()
    {
        gamemanager.manager.playing_player = 0;
        GameObject.Find("startbutton").SetActive(false);
        GameObject.Find("settingbutton").SetActive(false);
        gamemanager.manager.inactive_button();
        Instantiate(gamemanager.manager.table, new Vector2(0, 0), Quaternion.identity);
        gamemanager.manager.team_red.SetActive(true);
        gamemanager.manager.team_blue.SetActive(true);
        gamemanager.manager.pointer[0].SetActive(true);
        gamemanager.manager.bride_game.SetActive(false);
        gamemanager.manager.return_button.SetActive(false);
        gamemanager.manager.Startcard();
        if (netmode == true)
        {
            catch_netid.m = 0;
            catch_netid.n = 0;
            catch_netid.playerid = new uint[4, 13];
            netgamemanager.netmanager.g = 0;
        }
    }
    public void on_setting_button()
    {
        gamemanager.manager.AI_mode_button.SetActive(true);
        gamemanager.manager.one_player_button.SetActive(true);
        gamemanager.manager.return_button.SetActive(true);
        gamemanager.manager.net_mode_button.SetActive(true);
        gamemanager.manager.startbutton.SetActive(false);
        gamemanager.manager.setting_button.SetActive(false);
    }
    public void on_restart_button()
    {
        gamemanager.manager.restart_card();
        AI_control.ontable1 = new bool[13];
        AI_control.ontable2 = new bool[13];
        AI_control.ontable3 = new bool[13];
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
        gamemanager.manager.playerUI_control("梅花");
        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_diamond_button()
    {
        gamemanager.manager.temp_king = 2;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        gamemanager.manager.playerUI_control("磚塊");
        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_heart_button()
    {
        gamemanager.manager.temp_king = 3;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        gamemanager.manager.playerUI_control("愛心");
        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }
    public void on_spade_button()
    {
        gamemanager.manager.temp_king = 4;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        gamemanager.manager.playerUI_control("黑桃"); 

        for (int i = 0; i < gamemanager.manager.call_number - 1; i++)
            gamemanager.manager.number_button[i].SetActive(false);

        gamemanager.manager.pass = 0;
    }

    public void on_no_king_button()
    {
        gamemanager.manager.temp_king = 5;
        gamemanager.manager.call_number = gamemanager.manager.want_number;
        gamemanager.manager.playerUI_control("無王");
        for (int i = 0; i < gamemanager.manager.call_number; i++)
            gamemanager.manager.number_button[i].SetActive(false);
        gamemanager.manager.pass = 0;
    }
    public void on_pass_button()
    {
        gamemanager.manager.pass++;
        gamemanager.manager.player_call_card[(gamemanager.manager.playing_player + 3) % 4].GetComponent<Text>().text = "pass";
        if (gamemanager.manager.pass == 3)
        {
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
            gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
            gamemanager.manager.call_card_finish = true;
            gamemanager.manager.destroy_button();
            for (int i = 0; i < 4; i++)
            {
                gamemanager.manager.playerUI[i].SetActive(false);
                gamemanager.manager.player_call_card[i].SetActive(false);
            }
            
            //沒人喊牌就重發牌
            if (gamemanager.manager.temp_king == 0)
                gamemanager.manager.restart_card();
            else
                gamemanager.manager.after_call_card();
        }
    }
    public void on_all_color_button()
    {
        if (gamemanager.manager.AI_mode == true)
            if (gamemanager.manager.playing_player != 3)
            {
                foreach (GameObject i in gamemanager.manager.number_button)
                    i.GetComponent<Button>().interactable = false;
                foreach (GameObject i in gamemanager.manager.color_button)
                    i.GetComponent<Button>().interactable = false;
            }
            else
            {
                foreach (GameObject i in gamemanager.manager.number_button)
                    i.GetComponent<Button>().interactable = true;
                foreach (GameObject i in gamemanager.manager.color_button)
                    i.GetComponent<Button>().interactable = true;
            }

        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
        gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 1) % 4;
        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
        for (int i = 0; i < 5; i++)
            gamemanager.manager.color_button[i].SetActive(false);
    }

    public void on_AI_mode_button() 
    {
        gamemanager.manager.AI_mode = true;
    }
    public void on_one_player_button()
    {
        gamemanager.manager.AI_mode = false;
    }
    public void on_return_button() 
    {
        gamemanager.manager.startbutton.SetActive(true);
        gamemanager.manager.setting_button.SetActive(true);
        gamemanager.manager.one_player_button.SetActive(false);
        gamemanager.manager.AI_mode_button.SetActive(false);
        gamemanager.manager.net_mode_button.SetActive(false);
    }
    public void on_netmode_button()
    {
        gamemanager.manager.net_mode_button.SetActive(false);
        gamemanager.manager.AI_mode_button.SetActive(false);
        gamemanager.manager.one_player_button.SetActive(false);
        gamemanager.manager.bride_game.SetActive(false);
        gamemanager.manager.restart_button.SetActive(false);
    }
}
