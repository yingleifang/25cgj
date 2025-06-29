using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Pathfinding;


public class Monster : MonoBehaviour
{

    //敌人进入移动状态的sanity阈值
    public float enterMoveSanityThreshold ;//根据怪物不同的评级分设20，40，60，80
    //敌人状态标签的枚举
    public enum MonsterState
    {
        Moving,
        NotMoving,
    }
    //敌人状态标签
    public MonsterState monsterState;
    public Rigidbody2D rb;

    //动画器
    public Animator animator;

    
    void Start()
    {   rb = GetComponent<Rigidbody2D>();
        //注册对于玩家的sanity值的监听
        //获取动画器
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player!= null)
        {
            player.GetComponent<Player>().CurrentSanity.Register(value =>
            {//监测到玩家的sanity值变化到了threashold以下，则敌人进入移动状态
                if(value < enterMoveSanityThreshold)
                {
                    monsterState = MonsterState.Moving;
                    //开启AIDestinationSetter组件
                    //将rigid2D的dynaimc设置为true，可以被撞动
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    GetComponent<AIDestinationSetter>().enabled = true;
                    //开启AIPath组件
                    GetComponent<AIPath>().enabled = true;
                    //播放动画
                    animator.SetBool("Moving", true);

                }
                else
                {//监测到玩家的sanity值大于threashold，则敌人进入静止状态
                    monsterState = MonsterState.NotMoving;
                    //将rigid2D的bodytype设置为static，不可被撞动
                    rb.bodyType = RigidbodyType2D.Kinematic;

                    //关闭AIDestinationSetter组件
                    GetComponent<AIDestinationSetter>().enabled = false;
                    //关闭AIPath组件
                    GetComponent<AIPath>().enabled = false;
                    //播放动画
                    animator.SetBool("Back_to_Null", true);
                }


            }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        
        //敌人一进场就锁定了玩家，将其作为A*算法的追踪目标
        GetComponent<AIDestinationSetter>().target = player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
