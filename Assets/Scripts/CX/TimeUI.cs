using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using TMPro;

public class TimeUI : MonoBehaviour
{//时间管理器
    public GameObject timeMGR;
    public TextMeshProUGUI timeText;
    void Start()
    {
        
        timeMGR.GetComponent<TimeManager>().CurrentGameTime.Register(value =>
        {//将value记录的时间取整
            int time = (int)value;
            int timeleft = timeMGR.GetComponent<TimeManager>().currentLevelTime-time;//剩余时间
           timeText.text = timeleft.ToString();//输出倒计时
        }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
}
