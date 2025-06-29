using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class TimeManager : MonoBehaviour
{//进入GameScene后开始计时
    public BindableProperty<float> CurrentGameTime { get; } = new BindableProperty<float>();//当前游戏进行时间
    //当前关卡数
    public int currentLevel;
    //从GameScene打开后就开始计时

    public int[] levelTime = { 10, 20, 30, 40 };//关卡时间
    public int currentLevelTime;

    private void Start()
    { 
    //时间初始化为0
        CurrentGameTime.Value = 0;
        //进入游戏后默认第一关
        currentLevel = 1;
        currentLevelTime = levelTime[0];
        
    
    }

    //每帧更新计时
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
