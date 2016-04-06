using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableEventsClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableEventsClient _eventsClient;            

            public TheGetAllMethod()
            {                
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }
            [IntegrationTest]
            public async Task ReturnsEvents()
            {
                var events = await _eventsClient.GetAll().ToList();

                Assert.NotEmpty(events);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var events = await _eventsClient.GetAll(options).ToList();

                Assert.Equal(5, events.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var events = await _eventsClient.GetAll(options).ToList();

                Assert.Equal(5, events.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstEventsPage = await _eventsClient.GetAll(startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondEventsPage = await _eventsClient.GetAll(skipStartOptions).ToList();

                Assert.NotEqual(firstEventsPage[0].Id, secondEventsPage[0].Id);
                Assert.NotEqual(firstEventsPage[1].Id, secondEventsPage[1].Id);
                Assert.NotEqual(firstEventsPage[2].Id, secondEventsPage[2].Id);
                Assert.NotEqual(firstEventsPage[3].Id, secondEventsPage[3].Id);
                Assert.NotEqual(firstEventsPage[4].Id, secondEventsPage[4].Id);
            }

        }

        public class TheGetAllForRepositoryMethod
        {
           
        }

        public class TheGetAllForRepositoryNetworkMethod
        {
           
        }

        public class TheGetAllForOrganizationMethod
        {
           
        }

        public class TheGetAllUserReceivedPublicMethod
        {
           
        }

        public class TheGetAllUserPerformedMethod
        {
           
        }

        public class TheGetAllUserPerformedPublicMethod
        {
           
        }

        public class TheGetAllForAnOrganizationMethod
        {
           
        }
    }
}
