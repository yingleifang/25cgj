using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshMesh : MonoBehaviour
{
   //刷新网格的时间间隔
    public float refreshInterval = 3f;
    //计时器
    public float timer = 0f;

    
    void Update()
    {   timer += Time.deltaTime;
        if (timer >= refreshInterval)
        {
            timer = 0f;
            //刷新网格
            GetComponent<AstarPath>().Scan();
        
        }
        
    }
}
