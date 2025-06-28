using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    void Start()
    {
        //注册对于player上的sanity的变化监听
        player.GetComponent<Player>().CurrentSanity.Register(value =>
        {
            //更新Scrollbar的显示

            GetComponent<Scrollbar>().size = player.GetComponent<Player>().CurrentSanity.Value / player.GetComponent<Player>().PreSanity;
        }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
