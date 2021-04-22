using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mousecontrol : MonoBehaviour
{
    int templayer;
    Vector2 tempposition, tempscale;
    public bool ontable =false;
    public static string tempcolor;


    void OnMouseEnter()//�ƹ��I��P�������|������
    {
        if (gamemanager.manager.can_be_select(ontable, gameObject) == true)
        {
            templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            tempscale = gameObject.transform.localScale;
        }
    }
    void OnMouseOver() //�ƹ��b����Ĳ�P�ɷ|������
    {
        //�˴����i�P�ण�ॴ�A�i�H���ܤ~���� 
        if (gamemanager.manager.can_be_select(ontable, gameObject) ==true)     
        {
            //����ɵP�ܤj�A�Ʀb��L�d�e��
            transform.localScale = gamemanager.manager.cardchoosescale[(int)gamemanager.manager.cardtoplayer(gamemanager.manager.player, gameObject).x];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
        }

        //�P�b�ୱ���ܡA�n�N�P�ܦ��w�]�w���j�p(��W�h�@�����٬O�|�ܤj�̫�@���A�ҥH�n�[�o��)
        if (ontable ==true)
                transform.localScale = gamemanager.manager.table_card_scale;
    }
    void OnMouseExit() //�ƹ����}�P�������|������
    {
        if (gamemanager.manager.can_be_select(ontable, gameObject) == true)
        {
            transform.localScale = tempscale;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
        }
    }
    void OnMouseDrag() //�ƹ��I�۵P����ɷ|������
    {
        gamemanager.manager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        if (gamemanager.manager.can_be_select(ontable, gameObject) == true)
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
        }//�P�S����W
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
                    tempcolor = gameObject.tag;
                    for (int i = 0; i < 4; i++)
                        for (int k = 0; k < 13; k++)
                            if (gamemanager.manager.player[i, k].tag == tempcolor)
                            {
                                gamemanager.manager.player[i, k].tag = "mastercolor";
                            }
                    gamemanager.manager.must_color = gameObject.tag;
                }//�p�G���i�P����⤣�O���A���i�P������ܦ�mastercolor�A�j�a���o�X���Ӫ��
                else//�p�G���i�P�����O���A�j�a���o�X��
                    gamemanager.manager.must_color = gameObject.tag;
            }//���X���P�O�Ĥ@�i�P

            if (gamemanager.manager.table_card_number == 4)
            {
                gamemanager.manager.card_compare();//��j�p

                //��ୱ���P�^���A�P���ܦ^0
                for (int i = 0; i < 4; i++)
                    gamemanager.manager.table_card[i].SetActive(false);
                gamemanager.manager.table_card_number = 0;

                //��쥻tag�]��mastercolor���P��tag�]�^��
                for (int i = 0; i < 4; i++)
                    for (int k = 0; k < 13; k++)
                        if (gamemanager.manager.player[i, k].tag == "mastercolor")
                            gamemanager.manager.player[i, k].tag = tempcolor;

                //�p�G�Y���F��ؼФ��ơA�����C���A��ܹC�����G
                if (gamemanager.manager.red_score == gamemanager.manager.red_goal_score) 
                {
                    gamemanager.manager.win_panel.SetActive(true);
                    GameObject.Find("winner").GetComponent<Text>().text = "�����ӧQ";
                }
                if (gamemanager.manager.blue_score == gamemanager.manager.blue_goal_score)
                {
                    gamemanager.manager.win_panel.SetActive(true);
                    GameObject.Find("winner").GetComponent<Text>().text = "�Ŷ��ӧQ";
                }
                gamemanager.manager.blue_goal.GetComponent<Text>().text = "�Ŷ��ؼм[�ơG" + gamemanager.manager.blue_goal_score;
                gamemanager.manager.red_goal.GetComponent<Text>().text = "�����ؼм[�ơG" + gamemanager.manager.red_goal_score;
                gamemanager.manager.red_team_score.GetComponent<Text>().text = "������o�[�ơG" + gamemanager.manager.red_score;
                gamemanager.manager.blue_team_score.GetComponent<Text>().text = "�Ŷ���o�[�ơG" + gamemanager.manager.blue_score;
                
            }//���X���P�O�ĥ|�i�P
        }//�P������W
    }
}