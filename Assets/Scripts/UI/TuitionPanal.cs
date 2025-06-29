using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	public class TuitionPanalData : UIPanelData
	{
	}
	public partial class TuitionPanal : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TuitionPanalData ?? new TuitionPanalData();
			// please add init code here
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
