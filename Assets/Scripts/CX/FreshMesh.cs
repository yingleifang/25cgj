using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshMesh : MonoBehaviour
{
   //ˢ�������ʱ����
    public float refreshInterval = 3f;
    //��ʱ��
    public float timer = 0f;

    
    void Update()
    {   timer += Time.deltaTime;
        if (timer >= refreshInterval)
        {
            timer = 0f;
            //ˢ������
            GetComponent<AstarPath>().Scan();
        
        }
        
    }
}
