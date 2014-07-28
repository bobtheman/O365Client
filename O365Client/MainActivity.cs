using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System.Linq;

namespace O365Client
{
    [Activity(Label = "O365 Client", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			GetMessages (this);

        }
        const string ExchangeResourceId = "https://outlook.office365.com";
        const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

        public async Task<IEnumerable<IMessage>> GetMessages(Context context)
        {
            var client = await EnsureClientCreated(context);

            var messageResults = await (from i in client.Me.Inbox.Messages
                                        orderby i.DateTimeSent descending
                                        select i).ExecuteAsync();
			MyGlobalDeclaration.Messages = messageResults.CurrentPage.ToList ();
			ListAdapter = new HomeScreenAdapter(this, MyGlobalDeclaration.Messages);
            return messageResults.CurrentPage;
        }

        private async Task<ExchangeClient> EnsureClientCreated(Context context)
        {
            Authenticator authenticator = new Authenticator(context);
            var authInfo = await authenticator.AuthenticateAsync(ExchangeResourceId);

            return new ExchangeClient(new Uri(ExchangeServiceRoot), authInfo.GetAccessToken);
        }

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			var bodyActivity = new Intent (this, typeof(MessageBodyActivity));
			bodyActivity.PutExtra ("Id", v.Tag.ToString());
			StartActivity (bodyActivity);
		}
    }
}

