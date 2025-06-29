using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class TimeManager : MonoBehaviour
{//����GameScene��ʼ��ʱ
    public BindableProperty<float> CurrentGameTime { get; } = new BindableProperty<float>();//��ǰ��Ϸ����ʱ��
    //��ǰ�ؿ���
    public int currentLevel;
    //��GameScene�򿪺�Ϳ�ʼ��ʱ

    public int[] levelTime = { 10, 20, 30, 40 };//�ؿ�ʱ��
    public int currentLevelTime;

    private void Start()
    { 
    //ʱ���ʼ��Ϊ0
        CurrentGameTime.Value = 0;
        //������Ϸ��Ĭ�ϵ�һ��
        currentLevel = 1;
        currentLevelTime = levelTime[0];
        
    
    }

    //ÿ֡���¼�ʱ
    private void Update()
    {
        CurrentGameTime.Value += Time.deltaTime;
        if(CurrentGameTime.Value >= levelTime[currentLevel-1])
        {
            if (currentLevel==4)
            {
                Debug.Log("Congratulations! You have completed all the levels!");
                win();
            }
            currentLevel++;
            CurrentGameTime.Value = 0;
            currentLevelTime = levelTime[currentLevel-1];
            nextLevel();

        }
        
    }

    public void nextLevel()
    {

    }

    public void win()
    {

    }

}
