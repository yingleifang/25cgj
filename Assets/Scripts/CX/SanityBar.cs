using QFramework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Sprite[] StateIcons;
    public GameObject Icon;


    void Start()
    {
        //ע�����player�ϵ�sanity�ı仯����
        player.GetComponent<Player>().CurrentSanity.Register(value =>
        {
            //Debug.Log("��ǰsanity��" + value);
            //����Scrollbar����ʾ

            GetComponent<Scrollbar>().size = player.GetComponent<Player>().CurrentSanity.Value / player.GetComponent<Player>().PreSanity;
            //Debug.Log("scrollbar value:" + GetComponent<Scrollbar>().size);

            //如果san值在70以上，给Icon设为第一个sprite
            if (player.GetComponent<Player>().CurrentSanity.Value >= 70)
            {
                Icon.GetComponent<Image>().sprite = StateIcons[0];
            }
            //如果san值在30-70之间，给Icon设为第二个sprite
            else if (player.GetComponent<Player>().CurrentSanity.Value >= 30)
            {
                Icon.GetComponent<Image>().sprite = StateIcons[1];
            }
            //如果san值在0-30之间，给Icon设为第三个sprite
            else
            {
                Icon.GetComponent<Image>().sprite = StateIcons[2];
            }



            //如果san值掉到0或0以下
            if (player.GetComponent<Player>().CurrentSanity.Value <= 0)
            {
                GetComponent<Scrollbar>().size = 0;
                //使玩家的移动控制脚本失效
                player.GetComponent<TopDownCharacterController>().enabled = false;
                //将子物体的灯光控制器的脚本关闭
                foreach (Transform child in player.transform)
                {
                    child.GetComponent<HeadLampController>().enabled = false;
                }



                player.GetComponent<Player>().animator.SetTrigger("Dead");
                StartCoroutine(OpenFailPage());

            }

        }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OpenFailPage()
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
