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

    



    //��Ϸ������Ч����Դ
    public   AudioSource TimeAudioSource;//���ڲ���ʱ����Ч����Դ

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
    //����ʱ��ѭ�����ŵ�
   public void PlayBGM(AudioClip clip)
   {
        //��ֹͣ����
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        bgmAudioSource.clip = clip;

        bgmAudioSource.Play();


    }

   public void Play_drugsdrop_Effect()//�����й�ҩƷ�������Ч
   {
      Drug_effectAudioSource.clip = drugdropClip;
      Drug_effectAudioSource.Play();
   }

    //�����й�ҩƷ�Ե�����Ч
    public void Play_eatDrugs_Effect()
    {
        Drug_effectAudioSource.clip = drugeatClip;
        Drug_effectAudioSource.Play();
    }




    //���Ź��ﱻ��ס����Ч
    public void Play_enemyStop_Effect()
    {
            Monster_effectAudioSource.clip = monsterstopClip;
            Monster_effectAudioSource.Play();
    }

    public void Play_game_process_Effect(AudioClip clip)
    {       Game_process_effectAudioSource.clip = clip;
                Game_process_effectAudioSource.Play();
    }
//������Ϸ������Ч

    public void Play_mouse_Effect()//���������Ч,ֻ��һ��

    {Debug.Log("play mouse effect");
        Mouse_effectAudioSource.clip = mouseClickClip;
        Mouse_effectAudioSource.Play();
    }

    public void Play_time_Effect()//����ʱ����Ч���ñ�����Դ
    {
        TimeAudioSource.clip = timetickClip;
        TimeAudioSource.Play();
    }

    //������ҳbgm
    public void Play_FrontPage_bgm()
    {//�����ǰ���ŵı������ֲ�����ҳ�������֣�����ֹͣ���ţ��ٲ�����ҳ��������
        if( bgmAudioSource.clip != FrontPagebgmClip)
        {
            PlayBGM(FrontPagebgmClip);
        }
        
    }

    //ʹ����Ϸ������Դ������Ϸʧ����Ч
    public void Lose()
    {
        Play_game_process_Effect(mainCharDeadClip);

    }

    //ʹ����Ϸ������Դ������Ϸʤ����Ч
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
    //��ͣʱ����Ч
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
