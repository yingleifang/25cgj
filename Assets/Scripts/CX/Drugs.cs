using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drugs : MonoBehaviour
{
    public float SanityGain;//ҩƷ��sanֵ�ָ���
    public ObjectEventSO EatenEvent;

    //��ǰ����ΪҩƷ
    //���Ӵ���Player�Ժ�ʹ������ϵ�Sanֵ����
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {   
        //Debug.Log("��������ײ");
        if (collision.gameObject.tag == "Player")
        {
         EatenEvent.RaiseEvent(null,this);
            collision.GetComponent<Player>().CurrentSanity.Value =Mathf.Min( 100, collision.GetComponent<Player>().CurrentSanity.Value+SanityGain);
            Debug.Log("Player's Sanity increased by " + SanityGain);
            Destroy(gameObject);
        }
    }
}
