using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   //该类为一个单例类，用于管理游戏中的音效
   //保持在各个场景中出现
   public static AudioManager instance;

   public AudioSource bgmAudioSource;//用于播放背景音乐的音源


   public AudioSource Drug_effectAudioSource;//用于播放药品音效的音源

    //用于播放怪物音效的音源
    public AudioSource Monster_effectAudioSource;

    //用于播放游戏进程音效的音源
    public AudioSource Game_process_effectAudioSource;
    //鼠标音效使用的音源
    public AudioSource Mouse_effectAudioSource;

    



    //游戏进程音效的音源
    public   AudioSource TimeAudioSource;//用于播放时间音效的音源

    //游戏音效
    //首页背景音乐音效
    public AudioClip FrontPagebgmClip;
    //白噪背景音效
    public AudioClip WhiteNoiseClip;
    //游戏开始音效

    //药品掉落音效
    public AudioClip drugdropClip;
    //药品吃掉音效
    public AudioClip drugeatClip;
    //怪物被定音效
    public AudioClip monsterstopClip;
    //时间滴答音效
    public AudioClip timetickClip;
    //鼠标点击音效
    public AudioClip mouseClickClip;
    //主角死亡音效
    public AudioClip mainCharDeadClip;
    //关卡切换音效
    public AudioClip levelChangeClip;
    //游戏胜利音效
    public AudioClip gameWinClip;


   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }

      //注册各种需要播放音效的事件

   }
    //播放背景音乐的函数
    //当前有在播放的背景音乐，先停止播放，再播放新的背景音乐
    //播放时是循环播放的
   public void PlayBGM(AudioClip clip)
   {
        //先停止播放
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        bgmAudioSource.clip = clip;

        bgmAudioSource.Play();


    }

   public void Play_drugsdrop_Effect()//播放有关药品掉落的音效
   {
      Drug_effectAudioSource.clip = drugdropClip;
      Drug_effectAudioSource.Play();
   }

    //播放有关药品吃掉的音效
    public void Play_eatDrugs_Effect()
    {
        Drug_effectAudioSource.clip = drugeatClip;
        Drug_effectAudioSource.Play();
    }




    //播放怪物被定住的音效
    public void Play_enemyStop_Effect()
    {
            Monster_effectAudioSource.clip = monsterstopClip;
            Monster_effectAudioSource.Play();
    }

    public void Play_game_process_Effect(AudioClip clip)
    {       Game_process_effectAudioSource.clip = clip;
                Game_process_effectAudioSource.Play();
    }
//播放游戏进程音效

    public void Play_mouse_Effect()//播放鼠标音效,只响一下

    {Debug.Log("play mouse effect");
        Mouse_effectAudioSource.clip = mouseClickClip;
        Mouse_effectAudioSource.Play();
    }

    public void Play_time_Effect()//播放时间音效，用背景音源
    {
        TimeAudioSource.clip = timetickClip;
        TimeAudioSource.Play();
    }

    //播放首页bgm
    public void Play_FrontPage_bgm()
    {//如果当前播放的背景音乐不是首页背景音乐，则先停止播放，再播放首页背景音乐
        if( bgmAudioSource.clip != FrontPagebgmClip)
        {
            PlayBGM(FrontPagebgmClip);
        }
        
    }

    //使用游戏进程音源播放游戏失败音效
    public void Lose()
    {
        Play_game_process_Effect(mainCharDeadClip);

    }

    //使用游戏进程音源播放游戏胜利音效
    public void Win()
    {
        
        Play_game_process_Effect(gameWinClip);
    }

    public void heard()
    {
        Debug.Log("heard");
    }

    public void Play_WhiteNoise()
    {Debug.Log("play white noise");
        PlayBGM(WhiteNoiseClip);
    }
    //暂停时间音效
    public void Pause_time_Effect()
    {
        TimeAudioSource.Pause();
    }

    public void levelChange()
    {
        Game_process_effectAudioSource.clip = levelChangeClip;
        Game_process_effectAudioSource.Play();
    }
}
