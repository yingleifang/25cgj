using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    
    public float PreSanity ;//��ʼsanֵ
    public BindableProperty<float> CurrentSanity { get; } = new BindableProperty<float>();//��ǰsanֵ
    public float timer;
    public float changeinterval = 1f;

    void Start()
    {timer = 0;
        CurrentSanity.Value = PreSanity;
        //CurrenSanity.Value ÿ���һ

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
