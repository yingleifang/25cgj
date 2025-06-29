using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{//����GameScene��ʼ��ʱ
    public BindableProperty<float> CurrentGameTime { get; } = new BindableProperty<float>();//��ǰ��Ϸ����ʱ��
    //��ǰ�ؿ���
    public int currentLevel;
    //��GameScene�򿪺�Ϳ�ʼ��ʱ

    public int[] levelTime = { 10, 20, 30, 40 };//�ؿ�ʱ��
    public int currentLevelTime;
    public GameObject levelmgr;

    public ObjectEventSO gameStartEvent;
    private void Start()
    {
        gameStartEvent.RaiseEvent(null, this);
    //ʱ���ʼ��Ϊ0
        CurrentGameTime.Value = 0;
        //������Ϸ��Ĭ�ϵ�һ��
        currentLevel = 1;
        currentLevelTime = levelTime[0];
        //nextLevel();
        
    
    }

    //ÿ֡���¼�ʱ
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
        //���ʲôЧ��
        Debug.Log("Win OHHHHHHHH!");
        Debug.Log("OHHHHHHHH!");            //�Ƴ���ǰScene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //������Ϸ�������
        SceneManager.LoadScene("TestWinPanal");
    }

}
