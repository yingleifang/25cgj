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

    //播放时间背景音乐的音源
    public AudioSource Time_effectAudioSource;



    //游戏进程音效的音源
    public   AudioSource gameProcessAudioSource;//用于播放游戏进程音效的音源

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
   public void PlayBGM(AudioClip clip)
   {
      if (bgmAudioSource.isPlaying)
      {
         bgmAudioSource.Stop();
      } 
      bgmAudioSource.clip = clip;
      bgmAudioSource.Play();
   }

   public void Play_drugs_Effect(AudioClip clip)//播放有关药品的音效
   {
      Drug_effectAudioSource.clip = clip;
      Drug_effectAudioSource.Play();
   }

    //播放有关怪物的音效
    public void Play_enemy_Effect(AudioClip clip)
    {
            Monster_effectAudioSource.clip = clip;
            Monster_effectAudioSource.Play();
    }

    public void Play_game_process_Effect(AudioClip clip)
    {       gameProcessAudioSource.clip = clip;
            gameProcessAudioSource.Play();
    }
//播放游戏进程音效

    public void Play_mouse_Effect(AudioClip clip)//播放鼠标音效
    {
        Mouse_effectAudioSource.clip = clip;
        Mouse_effectAudioSource.Play();
    }

    public void Play_time_Effect(AudioClip clip)//播放时间音效，不用背景音源
    {
        Time_effectAudioSource.clip = clip;
        Time_effectAudioSource.Play();
    }

    //播放首页bgm
    public void Play_FrontPage_bgm()
    {
        PlayBGM(FrontPagebgmClip);
    }


}
