using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new RepositoryHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received(1)
                    .GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"),
                        options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAll(1, options);

                connection.Received(1)
                    .GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                await client.Get("fake", "repo", 12345678);

                connection.Received().Get<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                await client.Get(1, 12345678);

                connection.Received().Get<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 123));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var hook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Create("fake", "repo", hook);

                connection.Received().Post<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"), hook);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var hook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Create(1, hook);

                connection.Received().Post<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                var config = new Dictionary<string, string> { { "config", "" } };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewRepositoryHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewRepositoryHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewRepositoryHook("name", config)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewRepositoryHook("name", config)));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var hook = new EditRepositoryHook();

                client.Edit("fake", "repo", 12345678, hook);

                connection.Received().Patch<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"), hook);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                var hook = new EditRepositoryHook();

                client.Edit(1, 12345678, hook);

                connection.Received().Patch<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks/12345678"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(null, "name", 12345678, new EditRepositoryHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", null, 12345678, new EditRepositoryHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "name", 12345678, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, 12345678, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("", "name", 12345678, new EditRepositoryHook()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("owner", "", 12345678, new EditRepositoryHook()));
            }
        }

        public class TheTestMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Test("fake", "repo", 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678/tests"));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Test(1, 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks/12345678/tests"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Test(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Test("owner", null, 12345678));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Test("", "name", 12345678));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Test("owner", "", 12345678));
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Ping("fake", "repo", 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678/pings"));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Ping(1, 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks/12345678/pings"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Ping(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Ping("owner", null, 12345678));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Ping("", "name", 12345678));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Ping("owner", "", 12345678));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Delete("fake", "repo", 12345678);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryHooksClient(connection);

                client.Delete(1, 12345678);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryHooksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 12345678));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 12345678));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 12345678));
            }
        }
    }
}
