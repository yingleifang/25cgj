using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace QFramework.Coward
{
	public class FrontPagePanalData : UIPanelData
	{
	}
	public partial class FrontPagePanal : UIPanel
	{
        
        
        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as FrontPagePanalData ?? new FrontPagePanalData();
            // please add init code here
            
			//初始化UI
			GameStartButton.onClick.AddListener(() =>
            {
                
                //关闭自己
                CloseSelf();
                Debug.Log("Let's Go!!!");
                SceneManager.LoadScene("SampleScene");
            });

			ExitButton.onClick.AddListener(() =>
            {
                QuitGame();
            });

            //打开文字教程的页面
            RuleButton.onClick.AddListener(() =>
            {
                //关闭自己
                CloseSelf();
                Debug.Log("打开文字教程");
                SceneManager.LoadScene("TestTuitionPanal");
            });

            //打开staff页面
            StaffButton.onClick.AddListener(() =>
            {
                //关闭自己
                CloseSelf();
                Debug.Log("打开staff");
                SceneManager.LoadScene("TestStaffPanal");
            });

        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
            //GotoUISO.RaiseEvent(null,this);
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
        public void QuitGame()
        {
            // 在编辑器中打印日志，方便调试
            Debug.Log("QuitGame method called.");

            // 如果是在Unity编辑器中运行
#if UNITY_EDITOR
            // 停止编辑器播放模式
            EditorApplication.isPlaying = false;
#else
        // 在构建的应用程序中退出
        Application.Quit();
#endif
        }


    }
}
