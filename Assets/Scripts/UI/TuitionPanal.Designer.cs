using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Coward
{
	// Generate Id:3787b55c-136c-4cee-89c9-39ec1c0091d3
	public partial class TuitionPanal
	{
		public const string Name = "TuitionPanal";
		
		
		private TuitionPanalData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public TuitionPanalData Data
		{
			get
			{
				return mData;
			}
		}
		
		TuitionPanalData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TuitionPanalData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
