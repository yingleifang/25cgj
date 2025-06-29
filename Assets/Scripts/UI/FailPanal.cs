using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace QFramework.Coward
{
	public class FailPanalData : UIPanelData
	{
	}
	public partial class FailPanal : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as FailPanalData ?? new FailPanalData();
            // please add init code here

            //打开文字教程的页面
            Back_to_Front.onClick.AddListener(() =>
            {
                //关闭自己
                CloseSelf();
                Debug.Log("回到首页");
                SceneManager.LoadScene("TestFrontPagePanal");
            });


        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
