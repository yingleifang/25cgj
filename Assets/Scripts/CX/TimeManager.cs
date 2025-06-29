using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{//进入GameScene后开始计时
    public BindableProperty<float> CurrentGameTime { get; } = new BindableProperty<float>();//当前游戏进行时间
    //当前关卡数
    public int currentLevel;
    //从GameScene打开后就开始计时

    public int[] levelTime = { 10, 20, 30, 40 };//关卡时间
    public int currentLevelTime;
    public GameObject levelmgr;

    public ObjectEventSO gameStartEvent;
    private void Start()
    {
        gameStartEvent.RaiseEvent(null, this);
    //时间初始化为0
        CurrentGameTime.Value = 0;
        //进入游戏后默认第一关
        currentLevel = 1;
        currentLevelTime = levelTime[0];
        //nextLevel();
        
    
    }

    //每帧更新计时
    private void Update()
    {
        CurrentGameTime.Value += Time.deltaTime;
        if(CurrentGameTime.Value >= levelTime[currentLevel-1])
        {
            if (currentLevel==2)
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
        switch (currentLevel)
        {   
            case 1:
                Debug.Log("Level 1");
                levelmgr.GetComponent<LevelManager>().LoadLevel1();
                break;
            case 2:
                Debug.Log("Level 2");
                levelmgr.GetComponent<LevelManager>().LoadLevel2();
                break;
            case 3:
                Debug.Log("Level 3");
                levelmgr.GetComponent<LevelManager>().LoadLevel3();
                break;
            case 4:
                Debug.Log("Level 4");
                levelmgr.GetComponent<LevelManager>().LoadLevel4();
                break;
            

        }

    }

    public void win()
    {
        //别的什么效果
        Debug.Log("Win OHHHHHHHH!");
        Debug.Log("OHHHHHHHH!");            //移除当前Scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //加载游戏结束面板
        SceneManager.LoadScene("TestWinPanal");
    }

}
