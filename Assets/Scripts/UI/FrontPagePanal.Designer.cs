using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	// Generate Id:2a42c02a-3e37-4c26-87b8-02c0a0e4a1e4
	public partial class FrontPagePanal
	{
		public const string Name = "FrontPagePanal";
		
		[SerializeField]
		public UnityEngine.UI.Button GameStartButton;
		[SerializeField]
		public UnityEngine.UI.Button StaffButton;
		[SerializeField]
		public UnityEngine.UI.Button ExitButton;
		[SerializeField]
		public UnityEngine.UI.Button RuleButton;
		
		private FrontPagePanalData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			GameStartButton = null;
			StaffButton = null;
			ExitButton = null;
			RuleButton = null;
			
			mData = null;
		}
		
		public FrontPagePanalData Data
		{
			get
			{
				return mData;
			}
		}
		
		FrontPagePanalData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new FrontPagePanalData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
