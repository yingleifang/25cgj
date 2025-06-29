using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send_Button_hit : MonoBehaviour
{
    // Start is called before the first frame update

    public ObjectEventSO button_hit_event;
    public ObjectEventSO game_start_event;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //传出播放点击的音效事件
    public void SendButtonHit()
    {
        button_hit_event.RaiseEvent(null, this);
    }

    public void SendGameStart()
    {
        game_start_event.RaiseEvent(null, this);
    }
}
