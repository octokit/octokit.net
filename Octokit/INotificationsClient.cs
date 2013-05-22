using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface INotificationsClient
    {
        Task<IReadOnlyCollection<Notification>> ListNotifications();
    }
}
