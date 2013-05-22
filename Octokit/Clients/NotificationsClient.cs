using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class NotificationsClient : ApiClient, INotificationsClient
    {
        public NotificationsClient(IApiConnection client) : base(client)
        {
        }

        public async Task<IReadOnlyCollection<Notification>> ListNotifications()
        {
            return await Client.GetAll<Notification>(new Uri("/notifications", UriKind.Relative));
        }
    }
}
