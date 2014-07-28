using System;
using Android.App;
using System.Collections.Generic;
using Microsoft.Office365.Exchange;
using Android.Runtime;

namespace O365Client
{
	[Application]
	public class MyGlobalDeclaration : Android.App.Application
	{
		public MyGlobalDeclaration (IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{
		}

		public static List<IMessage> Messages { get; set;}

		public override void OnCreate() 
		{
			base.OnCreate();
			Messages = new List<IMessage> ();
		}
	}
}

