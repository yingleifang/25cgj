using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	// Generate Id:102ccbe6-e0c3-4297-b4ec-6b12d904bc5e
	public partial class StaffPanal
	{
		public const string Name = "StaffPanal";
		
		[SerializeField]
		public UnityEngine.UI.Button Back_To_Front;
		
		private StaffPanalData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Back_To_Front = null;
			
			mData = null;
		}
		
		public StaffPanalData Data
		{
			get
			{
				return mData;
			}
		}
		
		StaffPanalData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new StaffPanalData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
