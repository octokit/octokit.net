using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableNotificationsClientTests
{
    public class TheMarkAsReadMethod
    {
        [IntegrationTest]
        public async Task MarksNotificationsRead()
        {
            var client = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

            await client.Activity.Notifications.MarkAsRead();
        }
    }

    public class TheMarkAsReadForRepositoryMethod
    {
        [IntegrationTest]
        public async Task MarksNotificationsRead()
        {
            var owner = "octokit";
            var repo = "octokit.net";

            var client = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

            await client.Activity.Notifications.MarkAsReadForRepository(owner, repo);
        }

        [IntegrationTest]
        public async Task MarksNotificationsReadForRepositoryId()
        {
            var repositoryId = 7528679;

            var client = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

            await client.Activity.Notifications.MarkAsReadForRepository(repositoryId);
        }
    }
}