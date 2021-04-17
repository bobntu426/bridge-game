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
        if(gameObject.tag != "tablecard")
            transform.localScale = gamemanager.manager.cardchoosescale[gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject)];
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
        transform.position = gamemanager.manager.mousepos;
    }
    void OnMouseDown() //�ƹ��I���P�������|������
    {
        tempposition = transform.position;
    }
    void OnMouseUp()//�����I���۵P���ƹ���}�����@�����|������
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