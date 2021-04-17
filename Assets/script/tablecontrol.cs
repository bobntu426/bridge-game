using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablecontrol : MonoBehaviour
{
    Color temp;
    public static bool istrigger;
    private void OnTriggerStay2D(Collider2D other)
    {
        
        istrigger = true;
        gameObject.GetComponent<Renderer>().material.color = new Vector4(1.0f, 0.92f, 0.016f, 1.0f);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        gameObject.GetComponent<Renderer>().material.color = temp;
        istrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        temp = gameObject.GetComponent<Renderer>().material.color;
    }
}
