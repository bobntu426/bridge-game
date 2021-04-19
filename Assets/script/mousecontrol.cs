using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousecontrol : MonoBehaviour
{
    int templayer;
    Vector2 tempposition, tempscale;


    void OnMouseEnter()//�ƹ��I��P�������|������
    {
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        tempscale = gameObject.transform.localScale;
    }
    void OnMouseOver() //�ƹ��b����Ĳ�P�ɷ|������
    {
        if (gameObject.tag != "tablecard")
            transform.localScale = gamemanager.manager.cardchoosescale[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x];
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
    }
    void OnMouseExit() //�ƹ����}�P�������|������
    {
        if (gameObject.tag != "tablecard")
            transform.localScale = tempscale;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
    }
    void OnMouseDrag() //�ƹ��I�۵P����ɷ|������
    {
        gamemanager.manager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        if (gameObject.tag != "tablecard")
            transform.position = gamemanager.manager.mousepos;
    }
    void OnMouseDown() //�ƹ��I���P�������|������
    {
        tempposition = transform.position;
    }
    void OnMouseUp()//�����I���۵P���ƹ���}�����@�����|������
    {
        if (tablecontrol.istrigger == false)
        {
            transform.position = tempposition;
        }
        else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x == 0)
        {
            gameObject.tag = "tablecard";
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gamemanager.manager.handcardnum[0]--;
            for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[0]; k++)
                gamemanager.manager.player_in_game[0, k] = gamemanager.manager.player_in_game[0, k + 1];
            for (int i = 0; i < gamemanager.manager.handcardnum[0]; i++)
                gamemanager.manager.player_in_game[0, i].transform.position = new Vector2(gamemanager.manager.leftcardhorizonpos[0]+gamemanager.manager.handcardlength[0]/24* (13-gamemanager.manager.handcardnum[0]) + gamemanager.manager.handcardlength[0]/12*i, gamemanager.manager.cardverticalpos[0]);
            gameObject.transform.position = new Vector2(0, -1.7f);
            transform.localScale = new Vector2(2, 1.9f);
        }
        else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x == 1)
        {
            gameObject.tag = "tablecard";
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
            gameObject.tag = "tablecard";
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
            gameObject.tag = "tablecard";
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gamemanager.manager.handcardnum[3]--;
            for (int k = (int)gamemanager.manager.cardtoplayer(gamemanager.manager.player_in_game, gameObject).y; k < gamemanager.manager.handcardnum[3]; k++)
                gamemanager.manager.player_in_game[3, k] = gamemanager.manager.player_in_game[3, k + 1];
            for (int i = 0; i < gamemanager.manager.handcardnum[3]; i++)
                gamemanager.manager.player_in_game[3, i].transform.position = new Vector2(gamemanager.manager.cardverticalpos[3], gamemanager.manager.leftcardhorizonpos[3] - gamemanager.manager.handcardlength[3] / 24 * (13 - gamemanager.manager.handcardnum[3]) - gamemanager.manager.handcardlength[3] / 12 * i);
            gameObject.transform.position = new Vector2(-1.5f, 0);
            transform.localScale = new Vector2(2, 1.9f);
        }
    }
    void Update()
    {
        
    }

}