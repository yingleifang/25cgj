using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace QFramework.Coward
{
	public class WinPanalData : UIPanelData
	{
	}
	public partial class WinPanal : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as WinPanalData ?? new WinPanalData();
            // please add init code here
            
			Back_to_Front.onClick.AddListener(() =>
            {
                //�ر��Լ�
                CloseSelf();
                Debug.Log("�ص���ҳ");
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
