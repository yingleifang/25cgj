using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drugs : MonoBehaviour
{
    public float SanityGain;//药品的san值恢复量
    public ObjectEventSO EatenEvent;

    //当前物体为药品
    //当接触到Player以后，使玩家身上的San值增加
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {   
        //Debug.Log("发生了碰撞");
        if (collision.gameObject.tag == "Player")
        {
         EatenEvent.RaiseEvent(null,this);
            collision.GetComponent<Player>().CurrentSanity.Value =Mathf.Min( 100, collision.GetComponent<Player>().CurrentSanity.Value+SanityGain);
            Debug.Log("Player's Sanity increased by " + SanityGain);
            Destroy(gameObject);
        }
    }
}
