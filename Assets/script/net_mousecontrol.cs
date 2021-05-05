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

    void OnMouseEnter()//�ƹ��I��P�������|������
    {
        tempscale = gameObject.transform.localScale;
        templayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }
    void OnMouseOver() //�ƹ��b����Ĳ�P�ɷ|������
    {
        //�˴����i�P�ण�ॴ�A�i�H���ܤ~���� 
        if (transform.position.y == netgamemanager.netmanager.cardverticalpos[0])
        {
            //����ɵP�ܤj�A�Ʀb��L�d�e��
            transform.localScale = netgamemanager.netmanager.cardchoosescale[0];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
            temp_y_pos = transform.position.y;
        }

        //�P�b�ୱ���ܡA�n�N�P�ܦ��w�]�w���j�p(��W�h�@�����٬O�|�ܤj�̫�@���A�ҥH�n�[�o��)
        if (ontable == true) 
            transform.localScale = netgamemanager.netmanager.table_card_scale;
    }
    void OnMouseExit() //�ƹ����}�P�������|������
    {
        if (ontable == false)
        {
            transform.localScale = tempscale;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = templayer;
        }
    }
    void OnMouseDrag() //�ƹ��I�۵P����ɷ|������
    {

        if (temp_y_pos == netgamemanager.netmanager.cardverticalpos[0])
        {
            netgamemanager.netmanager.mousepos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            transform.position = netgamemanager.netmanager.mousepos;
        }
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


            ontable = true;
            NetworkClient.localPlayer.SendMessage("Cmd_put_card", gameObject);
            
        }//�P������W
    }
    
}
