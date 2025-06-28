using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    void Start()
    {
        //ע�����player�ϵ�sanity�ı仯����
        player.GetComponent<Player>().CurrentSanity.Register(value =>
        {
            //Debug.Log("��ǰsanity��" + value);
            //����Scrollbar����ʾ

            GetComponent<Scrollbar>().size = player.GetComponent<Player>().CurrentSanity.Value / player.GetComponent<Player>().PreSanity;
            //Debug.Log("scrollbar value:" + GetComponent<Scrollbar>().size);
        }
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
