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
            
			//��ʼ��UI
			GameStartButton.onClick.AddListener(() =>
            {
                
                //�ر��Լ�
                CloseSelf();
                Debug.Log("Let's Go!!!");
                SceneManager.LoadScene("SampleScene");
            });

			ExitButton.onClick.AddListener(() =>
            {
                QuitGame();
            });

            //�����̵ֽ̳�ҳ��
            RuleButton.onClick.AddListener(() =>
            {
                //�ر��Լ�
                CloseSelf();
                Debug.Log("�����ֽ̳�");
                SceneManager.LoadScene("TestTuitionPanal");
            });

            //��staffҳ��
            StaffButton.onClick.AddListener(() =>
            {
                //�ر��Լ�
                CloseSelf();
                Debug.Log("��staff");
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
            // �ڱ༭���д�ӡ��־���������
            Debug.Log("QuitGame method called.");

            // �������Unity�༭��������
#if UNITY_EDITOR
            // ֹͣ�༭������ģʽ
            EditorApplication.isPlaying = false;
#else
        // �ڹ�����Ӧ�ó������˳�
        Application.Quit();
#endif
        }


    }
}
