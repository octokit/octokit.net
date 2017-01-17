using System.Linq;
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
            readonly ObservableEventsClient _eventsClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllForRepositoryMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }
            [IntegrationTest]
            public async Task ReturnsRepositoryEvents()
            {
                var repositoryEvents = await _eventsClient.GetAllForRepository(owner, name).ToList();

                Assert.NotEmpty(repositoryEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var repositoryEvents = await _eventsClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, repositoryEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositoryEvents = await _eventsClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, repositoryEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctRepositoryEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstRepositoryEventsPage = await _eventsClient.GetAllForRepository(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondRepositoryEventsPage = await _eventsClient.GetAllForRepository(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstRepositoryEventsPage[0].Id, secondRepositoryEventsPage[0].Id);
                Assert.NotEqual(firstRepositoryEventsPage[1].Id, secondRepositoryEventsPage[1].Id);
                Assert.NotEqual(firstRepositoryEventsPage[2].Id, secondRepositoryEventsPage[2].Id);
                Assert.NotEqual(firstRepositoryEventsPage[3].Id, secondRepositoryEventsPage[3].Id);
                Assert.NotEqual(firstRepositoryEventsPage[4].Id, secondRepositoryEventsPage[4].Id);
            }
        }

        public class TheGetAllIssuesForRepositoryMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllIssuesForRepositoryMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsRepositoryEvents()
            {
                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 5
                };

                var repositoryEvents = await _eventsClient.GetAllIssuesForRepository(owner, name, options).ToList();

                Assert.NotEmpty(repositoryEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var repositoryEvents = await _eventsClient.GetAllIssuesForRepository(owner, name, options).ToList();

                Assert.Equal(5, repositoryEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositoryEvents = await _eventsClient.GetAllIssuesForRepository(owner, name, options).ToList();

                Assert.Equal(5, repositoryEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctRepositoryEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstRepositoryEventsPage = await _eventsClient.GetAllIssuesForRepository(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondRepositoryEventsPage = await _eventsClient.GetAllIssuesForRepository(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstRepositoryEventsPage[0].Id, secondRepositoryEventsPage[0].Id);
                Assert.NotEqual(firstRepositoryEventsPage[1].Id, secondRepositoryEventsPage[1].Id);
                Assert.NotEqual(firstRepositoryEventsPage[2].Id, secondRepositoryEventsPage[2].Id);
                Assert.NotEqual(firstRepositoryEventsPage[3].Id, secondRepositoryEventsPage[3].Id);
                Assert.NotEqual(firstRepositoryEventsPage[4].Id, secondRepositoryEventsPage[4].Id);
            }
        }

        public class TheGetAllForRepositoryNetworkMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllForRepositoryNetworkMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }
            [IntegrationTest]
            public async Task ReturnsRepositoryNetworkEvents()
            {
                var repositoryNetworkEvents = await _eventsClient.GetAllForRepositoryNetwork(owner, name).ToList();

                Assert.NotEmpty(repositoryNetworkEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryNetworkEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var repositoryNetworkEvents = await _eventsClient.GetAllForRepositoryNetwork(owner, name, options).ToList();

                Assert.Equal(5, repositoryNetworkEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoryNetworkEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositoryNetworkEvents = await _eventsClient.GetAllForRepositoryNetwork(owner, name, options).ToList();

                Assert.Equal(5, repositoryNetworkEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctRepositoryNetworkEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstRepositoryNetworkEventsPage = await _eventsClient.GetAllForRepositoryNetwork(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondRepositoryNetworkEventsPage = await _eventsClient.GetAllForRepositoryNetwork(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstRepositoryNetworkEventsPage[0].Id, secondRepositoryNetworkEventsPage[0].Id);
                Assert.NotEqual(firstRepositoryNetworkEventsPage[1].Id, secondRepositoryNetworkEventsPage[1].Id);
                Assert.NotEqual(firstRepositoryNetworkEventsPage[2].Id, secondRepositoryNetworkEventsPage[2].Id);
                Assert.NotEqual(firstRepositoryNetworkEventsPage[3].Id, secondRepositoryNetworkEventsPage[3].Id);
                Assert.NotEqual(firstRepositoryNetworkEventsPage[4].Id, secondRepositoryNetworkEventsPage[4].Id);
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string organization = "octokit";

            public TheGetAllForOrganizationMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsOrganizationEvents()
            {
                var organizationEvents = await _eventsClient.GetAllForOrganization(organization).ToList();

                Assert.NotEmpty(organizationEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOrganizationEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var organizationEvents = await _eventsClient.GetAllForOrganization(organization, options).ToList();

                Assert.Equal(5, organizationEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOrganizationEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var organizationEvents = await _eventsClient.GetAllForOrganization(organization, options).ToList();

                Assert.Equal(5, organizationEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctOrganizationEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstOrganizationEventsPage = await _eventsClient.GetAllForOrganization(organization, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondOrganizationEventsPage = await _eventsClient.GetAllForOrganization(organization, skipStartOptions).ToList();

                Assert.NotEqual(firstOrganizationEventsPage[0].Id, secondOrganizationEventsPage[0].Id);
                Assert.NotEqual(firstOrganizationEventsPage[1].Id, secondOrganizationEventsPage[1].Id);
                Assert.NotEqual(firstOrganizationEventsPage[2].Id, secondOrganizationEventsPage[2].Id);
                Assert.NotEqual(firstOrganizationEventsPage[3].Id, secondOrganizationEventsPage[3].Id);
                Assert.NotEqual(firstOrganizationEventsPage[4].Id, secondOrganizationEventsPage[4].Id);
            }
        }

        public class TheGetAllUserReceivedMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string user = "shiftkey";

            public TheGetAllUserReceivedMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsUserReceivedEvents()
            {
                var userReceivedEvents = await _eventsClient.GetAllUserReceived(user).ToList();

                Assert.NotEmpty(userReceivedEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserReceivedEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var userReceivedEvents = await _eventsClient.GetAllUserReceived(user, options).ToList();

                Assert.Equal(5, userReceivedEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserReceivedEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var userReceivedEvents = await _eventsClient.GetAllUserReceived(user, options).ToList();

                Assert.Equal(5, userReceivedEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctUserReceivedEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstUserReceivedEventsPage = await _eventsClient.GetAllUserReceived(user, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondUserReceivedEventsPage = await _eventsClient.GetAllUserReceived(user, skipStartOptions).ToList();

                Assert.NotEqual(firstUserReceivedEventsPage[0].Id, secondUserReceivedEventsPage[0].Id);
                Assert.NotEqual(firstUserReceivedEventsPage[1].Id, secondUserReceivedEventsPage[1].Id);
                Assert.NotEqual(firstUserReceivedEventsPage[2].Id, secondUserReceivedEventsPage[2].Id);
                Assert.NotEqual(firstUserReceivedEventsPage[3].Id, secondUserReceivedEventsPage[3].Id);
                Assert.NotEqual(firstUserReceivedEventsPage[4].Id, secondUserReceivedEventsPage[4].Id);
            }
        }

        public class TheGetAllUserReceivedPublicMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string user = "shiftkey";

            public TheGetAllUserReceivedPublicMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsUserReceivedPublicEvents()
            {
                var userReceivedPublicEvents = await _eventsClient.GetAllUserReceivedPublic(user).ToList();

                Assert.NotEmpty(userReceivedPublicEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserReceivedPublicEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var userReceivedPublicEvents = await _eventsClient.GetAllUserReceivedPublic(user, options).ToList();

                Assert.Equal(5, userReceivedPublicEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserReceivedPublicEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var userReceivedPublicEvents = await _eventsClient.GetAllUserReceivedPublic(user, options).ToList();

                Assert.Equal(5, userReceivedPublicEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctUserReceivedPublicEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstUserReceivedPublicEventsPage = await _eventsClient.GetAllUserReceivedPublic(user, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondUserReceivedPublicEventsPage = await _eventsClient.GetAllUserReceivedPublic(user, skipStartOptions).ToList();

                Assert.NotEqual(firstUserReceivedPublicEventsPage[0].Id, secondUserReceivedPublicEventsPage[0].Id);
                Assert.NotEqual(firstUserReceivedPublicEventsPage[1].Id, secondUserReceivedPublicEventsPage[1].Id);
                Assert.NotEqual(firstUserReceivedPublicEventsPage[2].Id, secondUserReceivedPublicEventsPage[2].Id);
                Assert.NotEqual(firstUserReceivedPublicEventsPage[3].Id, secondUserReceivedPublicEventsPage[3].Id);
                Assert.NotEqual(firstUserReceivedPublicEventsPage[4].Id, secondUserReceivedPublicEventsPage[4].Id);
            }
        }

        public class TheGetAllUserPerformedMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string user = "shiftkey";

            public TheGetAllUserPerformedMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsUserPerformedEvents()
            {
                var userPerformedEvents = await _eventsClient.GetAllUserPerformed(user).ToList();

                Assert.NotEmpty(userPerformedEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserPerformedEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var userPerformedEvents = await _eventsClient.GetAllUserPerformed(user, options).ToList();

                Assert.Equal(5, userPerformedEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserPerformedEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var userPerformedEvents = await _eventsClient.GetAllUserPerformed(user, options).ToList();

                Assert.Equal(5, userPerformedEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctUserPerformedEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstUserPerformedEventsPage = await _eventsClient.GetAllUserPerformed(user, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondUserPerformedEventsPage = await _eventsClient.GetAllUserPerformed(user, skipStartOptions).ToList();

                Assert.NotEqual(firstUserPerformedEventsPage[0].Id, secondUserPerformedEventsPage[0].Id);
                Assert.NotEqual(firstUserPerformedEventsPage[1].Id, secondUserPerformedEventsPage[1].Id);
                Assert.NotEqual(firstUserPerformedEventsPage[2].Id, secondUserPerformedEventsPage[2].Id);
                Assert.NotEqual(firstUserPerformedEventsPage[3].Id, secondUserPerformedEventsPage[3].Id);
                Assert.NotEqual(firstUserPerformedEventsPage[4].Id, secondUserPerformedEventsPage[4].Id);
            }
        }

        public class TheGetAllUserPerformedPublicMethod
        {
            readonly ObservableEventsClient _eventsClient;
            const string user = "shiftkey";

            public TheGetAllUserPerformedPublicMethod()
            {
                _eventsClient = new ObservableEventsClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsUserPerformedPublicEvents()
            {
                var userPerformedPublicEvents = await _eventsClient.GetAllUserPerformedPublic(user).ToList();

                Assert.NotEmpty(userPerformedPublicEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserPerformedPublicEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var userPerformedPublicEvents = await _eventsClient.GetAllUserPerformedPublic(user, options).ToList();

                Assert.Equal(5, userPerformedPublicEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserPerformedPublicEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var userPerformedPublicEvents = await _eventsClient.GetAllUserPerformedPublic(user, options).ToList();

                Assert.Equal(5, userPerformedPublicEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctUserPerformedPublicEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstUserPerformedPublicEventsPage = await _eventsClient.GetAllUserPerformedPublic(user, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondUserPerformedPublicEventsPage = await _eventsClient.GetAllUserPerformedPublic(user, skipStartOptions).ToList();

                Assert.NotEqual(firstUserPerformedPublicEventsPage[0].Id, secondUserPerformedPublicEventsPage[0].Id);
                Assert.NotEqual(firstUserPerformedPublicEventsPage[1].Id, secondUserPerformedPublicEventsPage[1].Id);
                Assert.NotEqual(firstUserPerformedPublicEventsPage[2].Id, secondUserPerformedPublicEventsPage[2].Id);
                Assert.NotEqual(firstUserPerformedPublicEventsPage[3].Id, secondUserPerformedPublicEventsPage[3].Id);
                Assert.NotEqual(firstUserPerformedPublicEventsPage[4].Id, secondUserPerformedPublicEventsPage[4].Id);
            }
        }

        public class TheGetAllForAnOrganizationMethod
        {
            readonly ObservableEventsClient _eventsClient;
            readonly string _organization;
            readonly string _user;

            public TheGetAllForAnOrganizationMethod()
            {
                var github = Helper.GetAuthenticatedClient();
                _eventsClient = new ObservableEventsClient(github);
                _user = Helper.UserName;
                _organization = Helper.Organization;
            }

            [IntegrationTest]
            public async Task ReturnsUserOrganizationEvents()
            {
                var userOrganizationEvents = await _eventsClient.GetAllForAnOrganization(_user, _organization).ToList();

                Assert.NotEmpty(userOrganizationEvents);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserOrganizationEventsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var userOrganizationEvents = await _eventsClient.GetAllForAnOrganization(_user, _organization, options).ToList();

                Assert.Equal(5, userOrganizationEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfUserOrganizationEventsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var userOrganizationEvents = await _eventsClient.GetAllForAnOrganization(_user, _organization, options).ToList();

                Assert.Equal(5, userOrganizationEvents.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctUserOrganizationEventsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstUserOrganizationEventsPage = await _eventsClient.GetAllForAnOrganization(_user, _organization, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondUserOrganizationEventsPage = await _eventsClient.GetAllForAnOrganization(_user, _organization, skipStartOptions).ToList();

                Assert.NotEqual(firstUserOrganizationEventsPage[0].Id, secondUserOrganizationEventsPage[0].Id);
                Assert.NotEqual(firstUserOrganizationEventsPage[1].Id, secondUserOrganizationEventsPage[1].Id);
                Assert.NotEqual(firstUserOrganizationEventsPage[2].Id, secondUserOrganizationEventsPage[2].Id);
                Assert.NotEqual(firstUserOrganizationEventsPage[3].Id, secondUserOrganizationEventsPage[3].Id);
                Assert.NotEqual(firstUserOrganizationEventsPage[4].Id, secondUserOrganizationEventsPage[4].Id);
            }
        }
    }
}
