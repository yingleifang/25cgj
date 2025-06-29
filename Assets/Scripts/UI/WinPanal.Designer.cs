using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	// Generate Id:2a86b240-ddbd-41cd-94f3-c0451a401826
	public partial class WinPanal
	{
		public const string Name = "WinPanal";
		
		[SerializeField]
		public UnityEngine.UI.Button Back_to_Front;
		
		private WinPanalData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Back_to_Front = null;
			
			mData = null;
		}
		
		public WinPanalData Data
		{
			get
			{
				return mData;
			}
		}
		
		WinPanalData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new WinPanalData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
