using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableUserEmailsClientTests
    {
        private readonly ObservableUserEmailsClient _emailClient
            = new ObservableUserEmailsClient(Helper.GetAuthenticatedClient());

        [IntegrationTest]
        public async Task CanGetEmail()
        {
            var client = new ObservableUserEmailsClient(Helper.GetAuthenticatedClient());

            var email = await client.GetAll();
            Assert.NotNull(email);
        }

        [IntegrationTest]
        public async Task CanGetEmailWithApiOptions()
        {
            var email = await _emailClient.GetAll(ApiOptions.None);
            Assert.NotNull(email);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfEmailsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var emails = await _emailClient.GetAll(options).ToList();

            Assert.NotEmpty(emails);
        }
    }
}
