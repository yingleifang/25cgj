using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    
    public float PreSanity ;//初始san值
    public BindableProperty<float> CurrentSanity { get; } = new BindableProperty<float>();//当前san值
    public float timer;
    public float changeinterval = 1f;

    void Start()
    {timer = 0;
        CurrentSanity.Value = PreSanity;
        //CurrenSanity.Value 每秒减一

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeinterval)
        {
            timer = 0;
            CurrentSanity.Value-=1.0f;
        }
    }

    

}
