
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Text;

namespace O365Client
{
	[Activity (Label = "Email:")]			
	public class MessageBodyActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			string id = Intent.GetStringExtra ("Id") ?? "Data not available";
			if(string.IsNullOrEmpty(id))
				return;

			var message = (from msg in MyGlobalDeclaration.Messages
			               where msg.Id == id
			               select msg).ToList ().FirstOrDefault ();

			SetContentView (Resource.Layout.Message);
			var fromText = FindViewById<TextView> (Resource.Id.fromText);
			var bodyText = FindViewById<TextView> (Resource.Id.bodyText);

			fromText.Text = message.From.Address;
            bodyText.SetText(Html.FromHtml(message.Body.Content), TextView.BufferType.Spannable);
			// Create your application here
		}
	}
}

