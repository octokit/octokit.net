using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterprisePreReceiveHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EnterprisePreReceiveHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);

                await client.GetAll();

                connection.Received().GetAll<PreReceiveHook>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks"),
                    null,
                    "application/vnd.github.v3+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAll(options);

                connection.Received().GetAll<PreReceiveHook>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks"),
                    null,
                    "application/vnd.github.v3+json",
                    options);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);

                await client.Get(1);

                connection.Received().Get<PreReceiveHook>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks/1"),
                    null,
                    "application/vnd.github.v3+json");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);
                var data = new NewPreReceiveHook("name", "repo", "script", 1);

                await client.Create(data);

                connection.Received().Post<PreReceiveHook>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks"),
                    data,
                    "application/vnd.github.v3+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnterprisePreReceiveHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));

                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook(null, "repo", "script", 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("", "repo", "script", 1));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook("name", null, "script", 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("name", "", "script", 1));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook("name", "repo", null, 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("name", "repo", "", 1));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);
                var data = new UpdatePreReceiveHook
                {
                    Name = "name",
                    ScriptRepository = new RepositoryReference
                    {
                        FullName = "repo"
                    },
                    Script = "script",
                    Environment = new PreReceiveEnvironmentReference
                    {
                        Id = 1
                    }
                };

                await client.Edit(1, data);

                connection.Received().Patch<PreReceiveHook>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks/1"),
                    data,
                    "application/vnd.github.v3+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnterprisePreReceiveHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveHooksClient(connection);

                await client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-hooks/1"),
                    Arg.Any<object>(),
                    "application/vnd.github.v3+json");
            }
        }
    }
}
