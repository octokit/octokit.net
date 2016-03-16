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

            Assert.Equal(1, emails.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfEmailsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var emails = await _emailClient.GetAll(options).ToList();

            Assert.Equal(0, emails.Count);
        }

        //[IntegrationTest]
        //public async Task ReturnsDistinctResultsBasedOnStartPage()
        //{
        //    var startOptions = new ApiOptions
        //    {
        //        PageSize = 5,
        //        PageCount = 1
        //    };

        //    var firstPage = await _emailClient.GetAll(startOptions);

        //    var skipStartOptions = new ApiOptions
        //    {
        //        PageSize = 5,
        //        PageCount = 1,
        //        StartPage = 2
        //    };

        //    var secondPage = await _emailClient.GetAll(skipStartOptions);

        //    Assert.Equal(firstPage[0].Email, secondPage[0].Email);
        //}
    }
}
