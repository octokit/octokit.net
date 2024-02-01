using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Octokit.Tests.Clients
{
    public class AutolinksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AutolinksClient(null));
            }
        }


        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await client.Get("fakeOwner", "fakeRepo", 42);

                connection.Received().Get<Autolink>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepo/autolinks/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 42));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 42));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await client.GetAll("fakeOwner", "fakeRepo");

                connection.Received().GetAll<Autolink>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepo/autolinks"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAll("fakeOwner", "fakeRepo", options);

                connection.Received(1)
                    .GetAll<Autolink>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepo/autolinks"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }
        
        public class TheCreateMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var newAutolink = new AutolinkRequest("fakeKeyPrefix", "fakeUrlTemplate", true);
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await client.Create("fakeOwner", "fakeRepo", newAutolink);

                connection.Received().Post<Autolink>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepo/autolinks"),
                    Arg.Is<AutolinkRequest>(a => a.KeyPrefix == "fakeKeyPrefix" 
                        && a.UrlTemplate == "fakeUrlTemplate" 
                        && a.IsAlphanumeric == true));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                var newAutolink = new AutolinkRequest("fakeKeyPrefix", "fakeUrlTemplate", true);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", newAutolink));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newAutolink));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                var newAutolink = new AutolinkRequest("fakeKeyPrefix", "fakeUrlTemplate", true);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", newAutolink));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newAutolink));
            }
        }
    
        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await client.Delete("fakeOwner", "fakeRepo", 42);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepo/autolinks/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 42));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AutolinksClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }
    }
}
