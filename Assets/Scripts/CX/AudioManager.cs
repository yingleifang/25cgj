using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   //����Ϊһ�������࣬���ڹ�����Ϸ�е���Ч
   //�����ڸ��������г���
   public static AudioManager instance;

   public AudioSource bgmAudioSource;//���ڲ��ű������ֵ���Դ


   public AudioSource Drug_effectAudioSource;//���ڲ���ҩƷ��Ч����Դ

    //���ڲ��Ź�����Ч����Դ
    public AudioSource Monster_effectAudioSource;

    //���ڲ�����Ϸ������Ч����Դ
    public AudioSource Game_process_effectAudioSource;
    //�����Чʹ�õ���Դ
    public AudioSource Mouse_effectAudioSource;

    //����ʱ�䱳�����ֵ���Դ
    public AudioSource Time_effectAudioSource;



    //��Ϸ������Ч����Դ
    public   AudioSource gameProcessAudioSource;//���ڲ�����Ϸ������Ч����Դ

    //��Ϸ��Ч
    //��ҳ����������Ч
    public AudioClip FrontPagebgmClip;
    //���뱳����Ч
    public AudioClip WhiteNoiseClip;
    //��Ϸ��ʼ��Ч

    //ҩƷ������Ч
    public AudioClip drugdropClip;
    //ҩƷ�Ե���Ч
    public AudioClip drugeatClip;
    //���ﱻ����Ч
    public AudioClip monsterstopClip;
    //ʱ��δ���Ч
    public AudioClip timetickClip;
    //�������Ч
    public AudioClip mouseClickClip;
    //����������Ч
    public AudioClip mainCharDeadClip;
    //�ؿ��л���Ч
    public AudioClip levelChangeClip;
    //��Ϸʤ����Ч
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

      //ע�������Ҫ������Ч���¼�

   }
    //���ű������ֵĺ���
    //��ǰ���ڲ��ŵı������֣���ֹͣ���ţ��ٲ����µı�������
   public void PlayBGM(AudioClip clip)
   {
      if (bgmAudioSource.isPlaying)
      {
         bgmAudioSource.Stop();
      } 
      bgmAudioSource.clip = clip;
      bgmAudioSource.Play();
   }

   public void Play_drugs_Effect(AudioClip clip)//�����й�ҩƷ����Ч
   {
      Drug_effectAudioSource.clip = clip;
      Drug_effectAudioSource.Play();
   }

    //�����йع������Ч
    public void Play_enemy_Effect(AudioClip clip)
    {
            Monster_effectAudioSource.clip = clip;
            Monster_effectAudioSource.Play();
    }

    public void Play_game_process_Effect(AudioClip clip)
    {       gameProcessAudioSource.clip = clip;
            gameProcessAudioSource.Play();
    }
//������Ϸ������Ч

    public void Play_mouse_Effect(AudioClip clip)//���������Ч
    {
        Mouse_effectAudioSource.clip = clip;
        Mouse_effectAudioSource.Play();
    }

    public void Play_time_Effect(AudioClip clip)//����ʱ����Ч�����ñ�����Դ
    {
        Time_effectAudioSource.clip = clip;
        Time_effectAudioSource.Play();
    }

    //������ҳbgm
    public void Play_FrontPage_bgm()
    {
        PlayBGM(FrontPagebgmClip);
    }


}
