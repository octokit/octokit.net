using System.Threading.Tasks;
using Octokit.Tests.Integration;
using Xunit;

public class NotificationsClientTests
{
    public class TheMarkAsReadMethod
    {
        [IntegrationTest]
        public async Task MarksNotificationsRead()
        {
            var github = Helper.GetAuthenticatedClient();

            await github.Activity.Notifications.MarkAsRead();
        }
    }

    public class TheMarkAsReadForRepositoryMethod
    {
        [IntegrationTest]
        public async Task MarksNotificationsRead()
        {
            var owner = "octokit";
            var repo = "octokit.net";

            var github = Helper.GetAuthenticatedClient();

            await github.Activity.Notifications.MarkAsReadForRepository(owner, repo);
        }

        [IntegrationTest]
        public async Task MarksNotificationsReadForRepositoryId()
        {
            var repositoryId = 7528679;

            var github = Helper.GetAuthenticatedClient();

            await github.Activity.Notifications.MarkAsReadForRepository(repositoryId);
        }
    }
}