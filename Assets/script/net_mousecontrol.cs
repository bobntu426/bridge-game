using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class net_mousecontrol : NetworkBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    int templayer;
    Vector2 tempposition, tempscale;
    public bool ontable = false;
    public float i;
    public float temp_y_pos;

    void OnMouseEnter()//滑鼠碰到牌的瞬間會做的事
    {
        tempscale = gameObject.transform.localScale;
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }
    void OnMouseOver() //滑鼠在持續接觸牌時會做的事
    {
        //檢測那張牌能不能打，可以的話才能選取 
        if (transform.position.y == netgamemanager.netmanager.cardverticalpos[0])
        {
            //選取時牌變大，排在其他卡前面
            transform.localScale = netgamemanager.netmanager.cardchoosescale[0];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
            temp_y_pos = transform.position.y;
        }

        //牌在桌面的話，要將牌變成已設定的大小(放上去一瞬間還是會變大最後一次，所以要加這行)
        if (ontable == true) 
            transform.localScale = netgamemanager.netmanager.table_card_scale;
    }
    void OnMouseExit() //滑鼠離開牌的瞬間會做的事
    {
        if (ontable == false)
        {
            transform.localScale = tempscale;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
        }
    }
    void OnMouseDrag() //滑鼠點著牌不放時會做的事
    {

        if (temp_y_pos == netgamemanager.netmanager.cardverticalpos[0])
        {
            netgamemanager.netmanager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            transform.position = netgamemanager.netmanager.mousepos;
        }
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
        }//牌沒放到桌上
        else
        {


            ontable = true;
            NetworkClient.localPlayer.SendMessage("Cmd_put_card", gameObject);
            
        }//牌有放到桌上
    }
    
}
