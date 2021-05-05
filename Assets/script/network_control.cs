using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class network_control : NetworkBehaviour
{

    public override void OnStartLocalPlayer()
    {
        if (netgamemanager.netmanager.g == 0) 
        { 
            gameObject.tag = "player1";
            netgamemanager.netmanager.g++;
        }
        else if (netgamemanager.netmanager.g == 1)
        {
            gameObject.tag = "player2";
            Cmd_g();
        }
        else if (netgamemanager.netmanager.g == 2)
        {
            gameObject.tag = "player3";
            Cmd_g();
        }
        else if (netgamemanager.netmanager.g == 3)
        {
            gameObject.tag = "player4";
        }
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    private void Update()
    {
        if (isLocalPlayer) {
            if(Input.GetKeyDown(KeyCode.UpArrow))
                transform.position = (Vector2)transform.position + new Vector2(0, 0.5f);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position = (Vector2)transform.position + new Vector2(0, -0.5f);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position = (Vector2)transform.position + new Vector2(-0.5f, 0);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position = (Vector2)transform.position + new Vector2(0.5f, 0);
        }
        if (Input.GetKeyDown("a"))
        {
             Cmdstart_card();
        }
        if (Input.GetKeyDown("b"))
        {
            
            Cmdstart_card_2();
            Cmdsetcard();
        }

    }
    [Command]
    public void Cmdstart_card()
    {
        
        Rpcstart_card();
    }
    [ClientRpc]
    public void Rpcstart_card()
    {
        netgamemanager.netmanager.start_card();
        print(1);
    }
    [Command]
    public void Cmdstart_card_2()
    {
        
        Rpcstart_card_2();


    }
    [ClientRpc]
    public void Rpcstart_card_2()
    {
        netgamemanager.netmanager.start_card_2();
    }
    [ClientRpc]
    public void Rpcsetcard()//依照index把各張牌分配到各玩家手中，並設好位置、屬性
    {
        
        for (int k = 0; k < 4; k++)
        {

            for (int i = 0; i < 13; i++)
            {
                
                if (NetworkClient.localPlayer.gameObject.tag == "player1")
                {
                    netgamemanager.netmanager.player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k));
                    netgamemanager.netmanager.player[k, i].GetComponent<SpriteRenderer>().sortingOrder = i;
                    if (k == 0)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[0] + i * (netgamemanager.netmanager.handcardlength[0] / 12), netgamemanager.netmanager.cardverticalpos[0]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[0];
                    }
                    if (k == 1)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[1], netgamemanager.netmanager.leftcardhorizonpos[1] + i * (netgamemanager.netmanager.handcardlength[1] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[1];
                    }
                    if (k == 2)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[2] - i * (netgamemanager.netmanager.handcardlength[2] / 12), netgamemanager.netmanager.cardverticalpos[2]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[2];
                    }
                    if (k == 3)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[3], netgamemanager.netmanager.leftcardhorizonpos[3] - i * (netgamemanager.netmanager.handcardlength[3] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[3];
                    }
                }
                else if (NetworkClient.localPlayer.gameObject.tag == "player2")
                {
                    netgamemanager.netmanager.player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k-90));
                    netgamemanager.netmanager.player[k, i].GetComponent<SpriteRenderer>().sortingOrder = i;
                    if (k == 1)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[0] + i * (netgamemanager.netmanager.handcardlength[0] / 12), netgamemanager.netmanager.cardverticalpos[0]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[0];
                    }
                    if (k == 2)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[1], netgamemanager.netmanager.leftcardhorizonpos[1] + i * (netgamemanager.netmanager.handcardlength[1] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[1];
                    }
                    if (k == 3)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[2] - i * (netgamemanager.netmanager.handcardlength[2] / 12), netgamemanager.netmanager.cardverticalpos[2]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[2];
                    }
                    if (k == 0)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[3], netgamemanager.netmanager.leftcardhorizonpos[3] - i * (netgamemanager.netmanager.handcardlength[3] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[3];
                    }
                    
                }
                else if (NetworkClient.localPlayer.gameObject.tag == "player3")
                {
                    netgamemanager.netmanager.player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k - 180));
                    if (k == 2)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[0] + i * (netgamemanager.netmanager.handcardlength[0] / 12), netgamemanager.netmanager.cardverticalpos[0]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[0];
                    }
                    if (k == 3)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[1], netgamemanager.netmanager.leftcardhorizonpos[1] + i * (netgamemanager.netmanager.handcardlength[1] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[1];
                    }
                    if (k == 0)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[2] - i * (netgamemanager.netmanager.handcardlength[2] / 12), netgamemanager.netmanager.cardverticalpos[2]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[2];
                    }
                    if (k == 1)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[3], netgamemanager.netmanager.leftcardhorizonpos[3] - i * (netgamemanager.netmanager.handcardlength[3] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[3];
                    }

                }
                else if (NetworkClient.localPlayer.gameObject.tag == "player4")
                {
                    netgamemanager.netmanager.player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k - 270));
                    if (k == 3)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[0] + i * (netgamemanager.netmanager.handcardlength[0] / 12), netgamemanager.netmanager.cardverticalpos[0]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[0];
                    }
                    if (k == 0)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[1], netgamemanager.netmanager.leftcardhorizonpos[1] + i * (netgamemanager.netmanager.handcardlength[1] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[1];
                    }
                    if (k == 1)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.leftcardhorizonpos[2] - i * (netgamemanager.netmanager.handcardlength[2] / 12), netgamemanager.netmanager.cardverticalpos[2]);
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[2];
                    }
                    if (k == 2)
                    {
                        netgamemanager.netmanager.player[k, i].transform.position = new Vector2(netgamemanager.netmanager.cardverticalpos[3], netgamemanager.netmanager.leftcardhorizonpos[3] - i * (netgamemanager.netmanager.handcardlength[3] / 12));
                        netgamemanager.netmanager.player[k, i].transform.localScale = netgamemanager.netmanager.cardnormalscale[3];
                    }
                }
                
            }
        }

    }
    [Command]
    public void Cmdsetcard()
    {
        Rpcsetcard();
    }
    [Command]
    public void Cmd_g()
    {
        netgamemanager.netmanager.g++;
    }
    [Command]
    public void Cmd_put_card(GameObject gameObject)
    {
        Rpc_put_card(gameObject);
    }
    [ClientRpc]
    public void Rpc_put_card(GameObject gameObject)
    {
        netgamemanager.netmanager.put_card_on_table(gameObject);
    }
}
