using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{


    public float PreSanity;//��ʼsanֵ
    public BindableProperty<float> CurrentSanity { get; } = new BindableProperty<float>();//��ǰsanֵ
    public float timer;
    public float changeinterval = 1f;
    //������
    public Animator animator;

    void Start()
    {   animator = GetComponent<Animator>();
        timer = 0;
        CurrentSanity.Value = PreSanity;
        //CurrenSanity.Value ÿ���һ

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeinterval)
        {
            timer = 0;
            CurrentSanity.Value -= 2.0f;
        }
    }


    //2D��ɫ������ײ�߼�
    private void OnCollisionEnter2D(Collision2D collision)
    { 
    if(collision.gameObject.tag == "Monster"){
            //����Է��ǵ���
            //��������ʲô״̬�����enum
            //����������ƶ�״̬�����������
            if (collision.gameObject.GetComponent<Monster>().monsterState == Monster.MonsterState.Moving)
            {
                Debug.Log("Player Dead");
                //ʹ��ҵ��ƶ����ƽű�ʧЧ
                GetComponent<TopDownCharacterController>().enabled = false;
                //��������ĵƹ�������Ľű��ر�
                foreach (Transform child in transform)
                {
                    child.GetComponent<HeadLampController>().enabled = false;
                }



                //������������
                animator.SetTrigger("Dead");



                //Ԥ������ҳ�����ת�߼�
                //����һ��2s��Э�����ڴ�ʧ��ҳ��
                StartCoroutine(OpenFailPage());













            }
            else
            {

            }
        }
    }

    //��ʧ��ҳ���Э�̺���
    IEnumerator OpenFailPage()
    {
        yield return new WaitForSeconds(1.5f);
        //��ת��ʧ��ҳ��
        Debug.Log("Game Over OHHHHHHHH!");
        Debug.Log("OHHHHHHHH!");            //�Ƴ���ǰScene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //������Ϸ�������
        SceneManager.LoadScene("TestFailPanal");

    }



}
