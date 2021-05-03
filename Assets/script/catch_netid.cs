using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class catch_netid : NetworkBehaviour
{

    public static int m, n;
    public static uint[,] playerid;
    public override void OnStartClient()
    {
        gameObject.AddComponent<net_mousecontrol>();
        if (m == 4) 
        {
            m = 0;
            n = 0;
        }
        playerid[m, n] = gameObject.GetComponent<NetworkIdentity>().netId;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = n;
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        n++;
        if (n == 13)
        {
            m++;
            n = 0;
        }
       
    }
}
