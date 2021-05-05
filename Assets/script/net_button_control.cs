using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class net_button_control : NetworkBehaviour
{

    public void on_start_button()
    {
        netgamemanager.netmanager.playing_player = 0;
        GameObject.Find("startbutton").SetActive(false);
        GameObject.Find("settingbutton").SetActive(false);
        netgamemanager.netmanager.inactive_button();
        Instantiate(netgamemanager.netmanager.table, new Vector2(0, 0), Quaternion.identity);
        netgamemanager.netmanager.team_red.SetActive(true);
        netgamemanager.netmanager.team_blue.SetActive(true);
        netgamemanager.netmanager.pointer[0].SetActive(true);
        netgamemanager.netmanager.bride_game.SetActive(false);
        netgamemanager.netmanager.return_button.SetActive(false);
        netgamemanager.netmanager.start_card();
        
    }
    public void on_setting_button()
    {
        netgamemanager.netmanager.AI_mode_button.SetActive(true);
        netgamemanager.netmanager.one_player_button.SetActive(true);
        netgamemanager.netmanager.return_button.SetActive(true);
        gamemanager.manager.net_mode_button.SetActive(true);
        netgamemanager.netmanager.startbutton.SetActive(false);
        netgamemanager.netmanager.setting_button.SetActive(false);
    }
    public void on_restart_button()
    {
        netgamemanager.netmanager.restart_card();
        AI_control.ontable1 = new bool[13];
        AI_control.ontable2 = new bool[13];
        AI_control.ontable3 = new bool[13];
    }
    public void on_num_button1()
    {
        netgamemanager.netmanager.want_number = 1;

    }
    public void on_num_button2()
    {
        netgamemanager.netmanager.want_number = 2;

    }
    public void on_num_button3()
    {
        netgamemanager.netmanager.want_number = 3;
    }
    public void on_num_button4()
    {
        netgamemanager.netmanager.want_number = 4;
    }
    public void on_num_button5()
    {
        netgamemanager.netmanager.want_number = 5;
    }
    public void on_num_button6()
    {
        netgamemanager.netmanager.want_number = 6;
    }
    public void on_num_button7()
    {
        netgamemanager.netmanager.want_number = 7;
    }
    public void on_all_number_button()
    {
        if (netgamemanager.netmanager.want_number == netgamemanager.netmanager.call_number)
        {
            for (int i = 4; i > netgamemanager.netmanager.temp_king - 1; i--)
                netgamemanager.netmanager.color_button[i].SetActive(true);
            for (int i = 0; i < netgamemanager.netmanager.temp_king; i++)
                netgamemanager.netmanager.color_button[i].SetActive(false);
        }
        else
            for (int i = 0; i < 5; i++)
                netgamemanager.netmanager.color_button[i].SetActive(true);

    }
    public void on_clover_button()
    {
        netgamemanager.netmanager.call_number = netgamemanager.netmanager.want_number;
        netgamemanager.netmanager.temp_king = 1;
        netgamemanager.netmanager.playerUI_control("梅花");
        for (int i = 0; i < netgamemanager.netmanager.call_number - 1; i++)
            netgamemanager.netmanager.number_button[i].SetActive(false);

        netgamemanager.netmanager.pass = 0;
    }
    public void on_diamond_button()
    {
        netgamemanager.netmanager.temp_king = 2;
        netgamemanager.netmanager.call_number = netgamemanager.netmanager.want_number;
        netgamemanager.netmanager.playerUI_control("磚塊");
        for (int i = 0; i < netgamemanager.netmanager.call_number - 1; i++)
            netgamemanager.netmanager.number_button[i].SetActive(false);

        netgamemanager.netmanager.pass = 0;
    }
    public void on_heart_button()
    {
        netgamemanager.netmanager.temp_king = 3;
        netgamemanager.netmanager.call_number = netgamemanager.netmanager.want_number;
        netgamemanager.netmanager.playerUI_control("愛心");
        for (int i = 0; i < netgamemanager.netmanager.call_number - 1; i++)
            netgamemanager.netmanager.number_button[i].SetActive(false);

        netgamemanager.netmanager.pass = 0;
    }
    public void on_spade_button()
    {
        netgamemanager.netmanager.temp_king = 4;
        netgamemanager.netmanager.call_number = netgamemanager.netmanager.want_number;
        netgamemanager.netmanager.playerUI_control("黑桃");

        for (int i = 0; i < netgamemanager.netmanager.call_number - 1; i++)
            netgamemanager.netmanager.number_button[i].SetActive(false);

        netgamemanager.netmanager.pass = 0;
    }

    public void on_no_king_button()
    {
        netgamemanager.netmanager.temp_king = 5;
        netgamemanager.netmanager.call_number = netgamemanager.netmanager.want_number;
        netgamemanager.netmanager.playerUI_control("無王");
        for (int i = 0; i < netgamemanager.netmanager.call_number; i++)
            netgamemanager.netmanager.number_button[i].SetActive(false);
        netgamemanager.netmanager.pass = 0;
    }
    public void on_pass_button()
    {
        netgamemanager.netmanager.pass++;
        netgamemanager.netmanager.player_call_card[(netgamemanager.netmanager.playing_player + 3) % 4].GetComponent<Text>().text = "pass";
        if (netgamemanager.netmanager.pass == 3)
        {
            netgamemanager.netmanager.pointer[netgamemanager.netmanager.playing_player].SetActive(false);
            netgamemanager.netmanager.playing_player = (netgamemanager.netmanager.playing_player + 3) % 4;
            netgamemanager.netmanager.pointer[netgamemanager.netmanager.playing_player].SetActive(true);
            netgamemanager.netmanager.call_card_finish = true;
            netgamemanager.netmanager.destroy_button();
            for (int i = 0; i < 4; i++)
            {
                netgamemanager.netmanager.playerUI[i].SetActive(false);
                netgamemanager.netmanager.player_call_card[i].SetActive(false);
            }

            //沒人喊牌就重發牌
            if (netgamemanager.netmanager.temp_king == 0)
                netgamemanager.netmanager.restart_card();
            else
                netgamemanager.netmanager.after_call_card();
        }
    }
    public void on_all_color_button()
    {
       
        foreach (GameObject i in netgamemanager.netmanager.number_button)
            i.GetComponent<Button>().interactable = true;
        foreach (GameObject i in netgamemanager.netmanager.color_button)
            i.GetComponent<Button>().interactable = true;
            

        netgamemanager.netmanager.pointer[netgamemanager.netmanager.playing_player].SetActive(false);
        netgamemanager.netmanager.playing_player = (netgamemanager.netmanager.playing_player + 1) % 4;
        netgamemanager.netmanager.pointer[netgamemanager.netmanager.playing_player].SetActive(true);
        for (int i = 0; i < 5; i++)
            netgamemanager.netmanager.color_button[i].SetActive(false);
    }

    
    public void on_return_button()
    {
        netgamemanager.netmanager.startbutton.SetActive(true);
        netgamemanager.netmanager.setting_button.SetActive(true);
        netgamemanager.netmanager.one_player_button.SetActive(false);
        netgamemanager.netmanager.AI_mode_button.SetActive(false);
        gamemanager.manager.net_mode_button.SetActive(false);
    }
    public void on_netmode_button()
    {
        gamemanager.manager.net_mode_button.SetActive(false);
        netgamemanager.netmanager.AI_mode_button.SetActive(false);
        netgamemanager.netmanager.one_player_button.SetActive(false);
        netgamemanager.netmanager.bride_game.SetActive(false);
        netgamemanager.netmanager.restart_button.SetActive(false);
    }
}
