using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousecontrol : MonoBehaviour
{
    int templayer;
    Vector2 tempposition, tempscale;

    void OnMouseEnter()//滑鼠碰到牌的瞬間會做的事
    {
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        tempscale = gameObject.transform.localScale;
    }
    void OnMouseOver() //滑鼠在持續接觸牌時會做的事
    {
        if(gameObject.tag != "tablecard")
            transform.localScale = gamemanager.manager.cardchoosescale[gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject)];
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
    }
    void OnMouseExit() //滑鼠離開牌的瞬間會做的事
    {
        if (gameObject.tag != "tablecard")
            transform.localScale = tempscale;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
    }
    void OnMouseDrag() //滑鼠點著牌不放時會做的事
    {
        gamemanager.manager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        transform.position = gamemanager.manager.mousepos;
    }
    void OnMouseDown() //滑鼠點擊牌的瞬間會做的事
    {
        tempposition = transform.position;
    }
    void OnMouseUp()//持續點擊著牌的滑鼠放開的那一瞬間會做的事
    {
        if (tablecontrol.istrigger == false&& gameObject.tag!="tablecard")
        {
            transform.localPosition = tempposition;
        }
        else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject) == 0)
        {
            gameObject.tag = "tablecard";
            gameObject.transform.position = new Vector2(0, -1.7f);
            transform.localScale = new Vector2(2, 1.9f);
        }
        else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject) == 1)
        {
            gameObject.tag = "tablecard";
            gameObject.transform.position = new Vector2(1.5f, 0);
            transform.localScale = new Vector2(2, 1.9f);
            
        }
        else if (gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject) == 2)
        {
            gameObject.tag = "tablecard";
            gameObject.transform.position = new Vector2(0, 1.7f);
            transform.localScale = new Vector2(2, 1.9f);
            
        }
        else
        {
            gameObject.tag = "tablecard";
            gameObject.transform.position = new Vector2(-1.5f, 0);
            transform.localScale = new Vector2(2, 1.9f);
        }
    }
    void Update()
    {
        
    }

}