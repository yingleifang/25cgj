using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{


    public float PreSanity;//初始san值
    public BindableProperty<float> CurrentSanity { get; } = new BindableProperty<float>();//当前san值
    public float timer;
    public float changeinterval = 1f;
    //动画器
    public Animator animator;

    void Start()
    {   animator = GetComponent<Animator>();
        timer = 0;
        CurrentSanity.Value = PreSanity;
        //CurrenSanity.Value 每秒减一

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


    //2D角色死亡碰撞逻辑
    private void OnCollisionEnter2D(Collision2D collision)
    { 
    if(collision.gameObject.tag == "Monster"){
            //如果对方是敌人
            //检测敌人是什么状态，检测enum
            //如果敌人是移动状态，则玩家死亡
            if (collision.gameObject.GetComponent<Monster>().monsterState == Monster.MonsterState.Moving)
            {
                Debug.Log("Player Dead");
                //使玩家的移动控制脚本失效
                GetComponent<TopDownCharacterController>().enabled = false;
                //将子物体的灯光控制器的脚本关闭
                foreach (Transform child in transform)
                {
                    child.GetComponent<HeadLampController>().enabled = false;
                }



                //播放死亡动画
                animator.SetTrigger("Dead");



                //预备死亡页面的跳转逻辑
                //设置一个2s的协程用于打开失败页面
                StartCoroutine(OpenFailPage());













            }
            else
            {

            }
        }
    }

    //打开失败页面的协程函数
    IEnumerator OpenFailPage()
    {
        yield return new WaitForSeconds(1.5f);
        //跳转到失败页面
        Debug.Log("Game Over OHHHHHHHH!");
        Debug.Log("OHHHHHHHH!");            //移除当前Scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //加载游戏结束面板
        SceneManager.LoadScene("TestFailPanal");

    }



}
