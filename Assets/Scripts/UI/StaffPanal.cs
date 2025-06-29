using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace QFramework.Coward
{
	public class StaffPanalData : UIPanelData
	{
	}
	public partial class StaffPanal : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as StaffPanalData ?? new StaffPanalData();
            // please add init code here

            Back_To_Front.onClick.AddListener(() =>
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
