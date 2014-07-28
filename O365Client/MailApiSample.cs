using Android.Content;
using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365Client
{
    public static class MailApiSample
    {
        const string ExchangeResourceId = "https://outlook.office365.com";
        const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

        public static async Task<IEnumerable<IMessage>> GetMessages(Context context)
        {
            var client = await EnsureClientCreated(context);

            var messageResults = await (from i in client.Me.Inbox.Messages
                                     orderby i.DateTimeSent descending
                                     select i).ExecuteAsync();

            return messageResults.CurrentPage;
        }

        private static async Task<ExchangeClient> EnsureClientCreated(Context context)
        {
            Authenticator authenticator = new Authenticator(context);
            var authInfo = await authenticator.AuthenticateAsync(ExchangeResourceId);

            return new ExchangeClient(new Uri(ExchangeServiceRoot), authInfo.GetAccessToken);
        }
    }
}
