using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager manager;
    public GameObject table;
    public GameObject[] Poker;
    public GameObject[,] player;
    public Button startgame;
    public Vector2 mousepos;
    public Vector2[] cardnormalscale,cardchoosescale;
    public float[] handcardlength, leftcardhorizonpos, cardverticalpos;
    public int[] handcardnum;

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
    }

    public void Startcard(GameObject[] Poker,ref GameObject[,] player,GameObject a,float j,short b, short k, short temp, short i, short f,short rcard,short[,] playerindex,short[] index, 
    Vector2[] cardnormalscale,//2.28f, 2.247f
    float[] handcardlength,//12.295
    float[] leftcardhorizonpos,//-6.1
    float[] cardverticalpos//-3.49f
    )
    {
        rcard = 52;
        index = new short[52];
        player = new GameObject[4,13];
        playerindex = new short[4,13];
        for (i = 0; i < 52; i++)
            index[i] = i;
        for (i = 0; i < 52; i++)
        {
            b = (short)Random.Range(0, rcard);
            if(i<13)
                playerindex[0,i] = index[b];
            else if(i<26)
                playerindex[1,i-13] = index[b];
            else if (i < 39)
                playerindex[2,i-26] = index[b];
            else
                playerindex[3,i-39] = index[b];
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
                    if (playerindex[k,f] > playerindex[k, f+1])
                    {
                        temp = playerindex[k, f];
                        playerindex[k, f] = playerindex[k, f+1];
                        playerindex[k, f+1] = temp;
                    }
                }
            }
            for (i = 0; i < 13; i++)
            {
                a = Poker[playerindex[k, i]];
                j = (float)i;
                Instantiate(a, new Vector2(-0.63f,0.09f), Quaternion.identity);
                player[k,i] = GameObject.Find(a.name+"(Clone)");
                player[k, i].transform.Rotate(new Vector3(0, 0, 90 * k));
                player[k, i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
                if (k == 0)
                {
                    player[k, i].transform.position = new Vector2(leftcardhorizonpos[k] + j * (handcardlength[k] / 12), cardverticalpos[k]);
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 1)
                {
                    player[k, i].transform.position = new Vector2(cardverticalpos[k],leftcardhorizonpos[k] + j * (handcardlength[k] / 12) );
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 2)
                {
                    player[k, i].transform.position = new Vector2(leftcardhorizonpos[k] - j * (handcardlength[k] / 12), cardverticalpos[k]);
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                if (k == 3)
                {
                    player[k, i].transform.position = new Vector2(cardverticalpos[k], leftcardhorizonpos[k] - j * (handcardlength[k] / 12));
                    player[k, i].transform.localScale = cardnormalscale[k];
                }
                player[k,i].AddComponent<BoxCollider2D>();
                player[k,i].GetComponent<BoxCollider2D>().isTrigger = true;
                player[k, i].AddComponent<mousecontrol>();
                player[k, i].SetActive(false);
            }
        }
    }
    public void onplaycardbotton()
    {
        GameObject.Find("Canvas").SetActive(false);
        manager.table.SetActive(true);
        for (int k = 0; k < 4; k++)
        {
            for (int i = 0; i < 13; i++)
            {
                manager.player[k, i].SetActive(true);
            }
        }
    }
    public Vector2 cardtoplayer(GameObject[,] player,GameObject poker)
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
}

