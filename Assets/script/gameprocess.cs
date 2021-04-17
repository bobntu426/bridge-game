using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameprocess : MonoBehaviour
{
    float j;
    GameObject a;
    short b, k, temp, i, f, rcard;
    short[,] playerindex;
    short[] index;
    void Start()
    {
        gamemanager.manager.Startcard(gamemanager.manager.Poker, ref gamemanager.manager.player, a, j, b, k, temp, i, f, rcard, playerindex, index, gamemanager.manager.cardnormalscale,gamemanager.manager.handcardlength,gamemanager.manager.leftcardhorizonpos,gamemanager.manager.cardverticalpos);
    }
    void Update()
    { 

    }
}
