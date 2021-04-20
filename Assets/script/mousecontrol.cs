using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousecontrol : MonoBehaviour
{
    int templayer;
    Vector2 tempposition, tempscale;
    bool ontable;
    public static string tempcolor;


    void OnMouseEnter()//滑鼠碰到牌的瞬間會做的事
    {
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        tempscale = gameObject.transform.localScale;
    }
    void OnMouseOver() //滑鼠在持續接觸牌時會做的事
    {
        if (ontable == false )
        transform.localScale = gamemanager.manager.cardchoosescale[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x];
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
    }
    void OnMouseExit() //滑鼠離開牌的瞬間會做的事
    {
        if (ontable == false)
            transform.localScale = tempscale;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
    }
    void OnMouseDrag() //滑鼠點著牌不放時會做的事
    {
        gamemanager.manager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        if (ontable == false)
            transform.position = gamemanager.manager.mousepos;
    }
    void OnMouseDown() //滑鼠點擊牌的瞬間會做的事
    {
        tempposition = transform.position;
    }
    void OnMouseUp()//持續點擊著牌的滑鼠放開的那一瞬間會做的事
    {
        if (tablecontrol.istrigger == false)
        {
            transform.position = tempposition;
        }
        else
        {
            ontable = true;
            if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x == 0)
            {
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gamemanager.manager.handcardnum[0]--;
                for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[0]; k++)
                    gamemanager.manager.player_in_game[0, k] = gamemanager.manager.player_in_game[0, k + 1];
                for (int i = 0; i < gamemanager.manager.handcardnum[0]; i++)
                    gamemanager.manager.player_in_game[0, i].transform.position = new Vector2(gamemanager.manager.leftcardhorizonpos[0] + gamemanager.manager.handcardlength[0] / 24 * (13 - gamemanager.manager.handcardnum[0]) + gamemanager.manager.handcardlength[0] / 12 * i, gamemanager.manager.cardverticalpos[0]);
                gameObject.transform.position = new Vector2(0, -1.7f);
                transform.localScale = new Vector2(2, 1.9f);
                print(transform.localScale);
            }
            else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x == 1)
            {
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gamemanager.manager.handcardnum[1]--;
                for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[1]; k++)
                    gamemanager.manager.player_in_game[1, k] = gamemanager.manager.player_in_game[1, k + 1];
                for (int i = 0; i < gamemanager.manager.handcardnum[1]; i++)
                    gamemanager.manager.player_in_game[1, i].transform.position = new Vector2(gamemanager.manager.cardverticalpos[1], gamemanager.manager.leftcardhorizonpos[1] + gamemanager.manager.handcardlength[1] / 24 * (13 - gamemanager.manager.handcardnum[1]) + gamemanager.manager.handcardlength[1] / 12 * i);

                gameObject.transform.position = new Vector2(1.5f, 0);
                transform.localScale = new Vector2(2, 1.9f);


            }
            else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x == 2)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gamemanager.manager.handcardnum[2]--;
                for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[2]; k++)
                    gamemanager.manager.player_in_game[2, k] = gamemanager.manager.player_in_game[2, k + 1];
                for (int i = 0; i < gamemanager.manager.handcardnum[2]; i++)
                    gamemanager.manager.player_in_game[2, i].transform.position = new Vector2(gamemanager.manager.leftcardhorizonpos[2] - gamemanager.manager.handcardlength[2] / 24 * (13 - gamemanager.manager.handcardnum[2]) - gamemanager.manager.handcardlength[2] / 12 * i, gamemanager.manager.cardverticalpos[2]);

                gameObject.transform.position = new Vector2(0, 1.7f);
                transform.localScale = new Vector2(2, 1.9f);

            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gamemanager.manager.handcardnum[3]--;
                for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[3]; k++)
                    gamemanager.manager.player_in_game[3, k] = gamemanager.manager.player_in_game[3, k + 1];
                for (int i = 0; i < gamemanager.manager.handcardnum[3]; i++)
                    gamemanager.manager.player_in_game[3, i].transform.position = new Vector2(gamemanager.manager.cardverticalpos[3], gamemanager.manager.leftcardhorizonpos[3] - gamemanager.manager.handcardlength[3] / 24 * (13 - gamemanager.manager.handcardnum[3]) - gamemanager.manager.handcardlength[3] / 12 * i);
                gameObject.transform.position = new Vector2(-1.5f, 0);
                transform.localScale = new Vector2(2, 1.9f);

            }
            
            gamemanager.manager.table_card_number++;
            gamemanager.manager.table_card[gamemanager.manager.table_card_number - 1] = gameObject;

            if (gamemanager.manager.table_card_number == 1 && gameObject.tag != "kingcolor")
            {
                tempcolor = gameObject.tag;
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == gameObject.tag)
                        {
                            gamemanager.manager.player[i, k].tag = "mastercolor";
                        }
            }
            if (gamemanager.manager.table_card_number == 4)
            {
                gamemanager.manager.card_compare();
                for (int i = 0; i < 4; i++)
                    gamemanager.manager.table_card[i].SetActive(false);
                gamemanager.manager.table_card_number = 0;
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == "mastercolor")
                        {
                            print(tempcolor);
                            gamemanager.manager.player[i, k].tag = tempcolor;
                        }
            }
        }
    
    }
    void Update()
    {
        
    }

}