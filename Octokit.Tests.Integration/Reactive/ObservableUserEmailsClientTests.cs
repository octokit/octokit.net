using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableUserEmailsClientTests
    {
        readonly ObservableUserEmailsClient _emailClient;

        public ObservableUserEmailsClientTests()
        {
            var github = Helper.GetAuthenticatedClient();

            _emailClient = new ObservableUserEmailsClient(github);
        }

        [IntegrationTest]
        public async Task CanGetEmail()
        {
            var email = await _emailClient.GetAll();
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
