using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	// Generate Id:4e65d725-d235-4ade-afaa-f155e05be78a
	public partial class FailPanal
	{
		public const string Name = "FailPanal";
		
		[SerializeField]
		public UnityEngine.UI.Button Back_to_Front;
		
		private FailPanalData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Back_to_Front = null;
			
			mData = null;
		}
		
		public FailPanalData Data
		{
			get
			{
				return mData;
			}
		}
		
		FailPanalData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new FailPanalData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
