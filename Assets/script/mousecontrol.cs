using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mousecontrol : MonoBehaviour
{
    int templayer;
    Vector2 tempposition, tempscale;
    public bool ontable = false;
    public float i;

    void OnMouseEnter()//滑鼠碰到牌的瞬間會做的事
    {
        tempscale = gameObject.transform.localScale;
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }
    void OnMouseOver() //滑鼠在持續接觸牌時會做的事
    {
        //檢測那張牌能不能打，可以的話才能選取 
        if (gamemanager.manager.can_be_select(ontable, gameObject))
        {
            //選取時牌變大，排在其他卡前面
            transform.localScale = gamemanager.manager.cardchoosescale[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;

        }

        //牌在桌面的話，要將牌變成已設定的大小(放上去一瞬間還是會變大最後一次，所以要加這行)
        if (ontable == true)
            transform.localScale = gamemanager.manager.table_card_scale;
    }
    void OnMouseExit() //滑鼠離開牌的瞬間會做的事
    {
        if (gamemanager.manager.can_be_select(ontable, gameObject) == true)
        {
            transform.localScale = tempscale;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
        }
    }
    void OnMouseDrag() //滑鼠點著牌不放時會做的事
    {

        if (gamemanager.manager.can_be_select(ontable, gameObject) == true)
        {
            gamemanager.manager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            transform.position = gamemanager.manager.mousepos;
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
            gamemanager.manager.put_card_on_table(gameObject, ref ontable);
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(false);
            gamemanager.manager.playing_player = (gamemanager.manager.playing_player + 3) % 4;
            gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
            if (gamemanager.manager.table_card_number == 1)
            {
                if (gameObject.tag != "kingcolor")
                {
                    gamemanager.manager.temp_color = gameObject.tag;
                    for (int i = 0; i < 4; i++)
                        for (int k = 0; k < 13; k++)
                            if (gamemanager.manager.player[i, k].tag == gamemanager.manager.temp_color)
                            {
                                gamemanager.manager.player[i, k].tag = "mastercolor";
                            }
                    gamemanager.manager.must_color = gameObject.tag;
                }//如果那張牌的花色不是王，那張牌的花色變成mastercolor，大家都得出那個花色
                else//如果那張牌的花色是王，大家都得出王
                    gamemanager.manager.must_color = gameObject.tag;
            }//打出的牌是第一張牌

            if (gamemanager.manager.table_card_number == 4)
            {
                gamemanager.manager.card_compare();

                //把原本tag設成mastercolor的牌的tag設回來
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == "mastercolor")
                            gamemanager.manager.player[i, k].tag = gamemanager.manager.temp_color;
                Invoke("put_card_on_table", 1f);
                //隔2秒把桌面的牌回收，牌數變回0

            }//打出的牌是第四張牌
        }//牌有放到桌上
    }
    public void put_card_on_table()
    {
        for (int i = 0; i < 4; i++)
            gamemanager.manager.table_card[i].SetActive(false);
        gamemanager.manager.table_card_number = 0;

        //如果某隊達到目標分數，結束遊戲，顯示遊戲結果
        if (gamemanager.manager.red_score == gamemanager.manager.red_goal_score)
        {
            gamemanager.manager.win_panel.SetActive(true);
            GameObject.Find("winner").GetComponent<Text>().text = "紅隊勝利";
            gamemanager.manager.game_finish();
        }
        if (gamemanager.manager.blue_score == gamemanager.manager.blue_goal_score)
        {
            gamemanager.manager.win_panel.SetActive(true);
            GameObject.Find("winner").GetComponent<Text>().text = "藍隊勝利";
            gamemanager.manager.game_finish();
        }

        gamemanager.manager.pointer[gamemanager.manager.playing_player].SetActive(true);
    }
}

