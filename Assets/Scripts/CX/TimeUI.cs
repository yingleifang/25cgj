using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using TMPro;

public class TimeUI : MonoBehaviour
{//ʱ�������
    public GameObject timeMGR;
    public TextMeshProUGUI timeText;
    void Start()
    {
        
        timeMGR.GetComponent<TimeManager>().CurrentGameTime.Register(value =>
        {//��value��¼��ʱ��ȡ��
            int time = (int)value;
            int timeleft = timeMGR.GetComponent<TimeManager>().currentLevelTime-time;//ʣ��ʱ��
           timeText.text = timeleft.ToString();//�������ʱ
        }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
}
