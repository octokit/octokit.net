using Octokit.Tests.Integration.Fixtures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryHooksClientTests
    {
        [Collection(RepositoriesHooksCollection.Name)]
        public class TheGetAllMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheGetAllMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task ReturnsAllHooksFromRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);

                var actualHook = hooks[0];
                AssertHook(_fixture.ExpectedHook, actualHook);
            }

            [IntegrationTest]
            public async Task ReturnsAllHooksFromRepositoryWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryId);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);

                var actualHook = hooks[0];
                AssertHook(_fixture.ExpectedHook, actualHook);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, options);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithoutStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryId, options);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, options);

                Assert.Single(hooks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryId, options);

                Assert.Single(hooks);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var firstPage = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var firstPage = await github.Repository.Hooks.GetAll(_fixture.RepositoryId, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Hooks.GetAll(_fixture.RepositoryId, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheGetMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheGetMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task GetHookByCreatedId()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualHook = await github.Repository.Hooks.Get(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);

                AssertHook(_fixture.ExpectedHook, actualHook);
            }

            [IntegrationTest]
            public async Task GetHookByCreatedIdWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualHook = await github.Repository.Hooks.Get(_fixture.RepositoryId, _fixture.ExpectedHook.Id);

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        public class TheCreateMethod
        {
            [IntegrationTest]
            public async Task CreateAWebHookForTestRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var repoName = Helper.MakeNameWithTimestamp("create-hooks-test");
                var repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });

                var url = "http://test.com/example";
                var contentType = WebHookContentType.Json;
                var secret = "53cr37";

                var config = new Dictionary<string, string>
                {
                    { "hostname", "http://hostname.url" },
                    { "username", "username" },
                    { "password", "password" }
                };

                var parameters = new NewRepositoryWebHook("windowsazure", config, url)
                {
                    Events = new[] { "push" },
                    Active = false,
                    ContentType = contentType,
                    Secret = secret
                };

                var hook = await github.Repository.Hooks.Create(Helper.UserName, repository.Name, parameters.ToRequest());

                var baseHookUrl = CreateExpectedBaseHookUrl(repository.Url, hook.Id);
                var webHookConfig = CreateExpectedConfigDictionary(config, url, contentType, secret);

                Assert.Equal("windowsazure", hook.Name);
                Assert.Equal(new[] { "push" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/test", hook.TestUrl);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotEqual(default, hook.CreatedAt);
                Assert.NotEqual(default, hook.UpdatedAt);
                Assert.Equal(webHookConfig.Keys.OrderBy(x => x), hook.Config.Keys.OrderBy(x => x));
                Assert.Equal(webHookConfig.Values.OrderBy(x => x), hook.Config.Values.OrderBy(x => x));
                Assert.False(hook.Active);
            }

            [IntegrationTest]
            public async Task CreateAWebHookForTestRepositoryWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var repoName = Helper.MakeNameWithTimestamp("create-hooks-test");
                var repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });

                var url = "http://test.com/example";
                var contentType = WebHookContentType.Json;
                var secret = "53cr37";

                var config = new Dictionary<string, string>
                {
                    { "hostname", "http://hostname.url" },
                    { "username", "username" },
                    { "password", "password" }
                };

                var parameters = new NewRepositoryWebHook("windowsazure", config, url)
                {
                    Events = new[] { "push" },
                    Active = false,
                    ContentType = contentType,
                    Secret = secret
                };

                var hook = await github.Repository.Hooks.Create(repository.Id, parameters.ToRequest());

                var baseHookUrl = CreateExpectedBaseHookUrl(repository.Url, hook.Id);
                var webHookConfig = CreateExpectedConfigDictionary(config, url, contentType, secret);

                Assert.Equal("windowsazure", hook.Name);
                Assert.Equal(new[] { "push" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/test", hook.TestUrl);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotEqual(default, hook.CreatedAt);
                Assert.NotEqual(default, hook.UpdatedAt);
                Assert.Equal(webHookConfig.Keys.OrderBy(x => x), hook.Config.Keys.OrderBy(x => x));
                Assert.Equal(webHookConfig.Values.OrderBy(x => x), hook.Config.Values.OrderBy(x => x));
                Assert.False(hook.Active);
            }

            Dictionary<string, string> CreateExpectedConfigDictionary(Dictionary<string, string> config, string url, WebHookContentType contentType, string secret)
            {
                return new Dictionary<string, string>
                {
                    { "url", url },
                    { "content_type", contentType.ToString().ToLowerInvariant() },
                    { "secret", secret },
                    { "insecure_ssl", "False" }
                }.Union(config).ToDictionary(k => k.Key, v => v.Value);
            }

            string CreateExpectedBaseHookUrl(string url, int id)
            {
                return url + "/hooks/" + id;
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheEditMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheEditMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task EditHookWithNoNewConfigRetainsTheOldConfig()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHooks[0].Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
                Assert.Equal(new[] { "deployment", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }

            [IntegrationTest]
            public async Task EditHookWithNoNewConfigRetainsTheOldConfigWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryId, _fixture.ExpectedHooks[1].Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
                Assert.Equal(new[] { "push", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }

            [IntegrationTest]
            public async Task EditHookWithNewInformation()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook(new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } })
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHooks[2].Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } };
                Assert.Equal(new[] { "push", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }

            [IntegrationTest]
            public async Task EditHookWithNewInformationWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook(new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } })
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryId, _fixture.ExpectedHooks[3].Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } };
                Assert.Equal(new[] { "push", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheTestMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheTestMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task TestACreatedHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Test(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
            }

            [IntegrationTest]
            public async Task TestACreatedHookWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Test(_fixture.RepositoryId, _fixture.ExpectedHook.Id);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class ThePingMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public ThePingMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task PingACreatedHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Ping(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
            }

            [IntegrationTest]
            public async Task PingACreatedHookWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Ping(_fixture.RepositoryId, _fixture.ExpectedHook.Id);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheDeleteMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheDeleteMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task DeleteCreatedWebHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Delete(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName);

                Assert.Empty(hooks.Where(hook => hook.Id == _fixture.ExpectedHook.Id));
            }

            [IntegrationTest]
            public async Task DeleteCreatedWebHookWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Delete(_fixture.RepositoryId, _fixture.ExpectedHooks[1].Id);
                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryId);

                Assert.Empty(hooks.Where(hook => hook.Id == _fixture.ExpectedHooks[1].Id));
            }
        }

        static void AssertHook(RepositoryHook expectedHook, RepositoryHook actualHook)
        {
            Assert.Equal(expectedHook.Id, actualHook.Id);
            Assert.Equal(expectedHook.Active, actualHook.Active);
            Assert.Equal(expectedHook.Config, actualHook.Config);
            Assert.Equal(expectedHook.CreatedAt, actualHook.CreatedAt);
            Assert.Equal(expectedHook.Name, actualHook.Name);
            Assert.Equal(expectedHook.PingUrl, actualHook.PingUrl);
            Assert.Equal(expectedHook.TestUrl, actualHook.TestUrl);
            Assert.Equal(expectedHook.UpdatedAt, actualHook.UpdatedAt);
            Assert.Equal(expectedHook.Url, actualHook.Url);
        }
    }
}
