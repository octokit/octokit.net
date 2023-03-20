using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryVariablesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryVariablesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);

                await client.GetAll("owner", "repo");

                connection.Received()
                    .Get<RepositoryVariablesCollection>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);

                await client.Get("owner", "repo", "variable");

                connection.Received()
                    .Get<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", ""));
            }
        }

        public class CreateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);
                var createVariable = new CreateRepositoryVariable
                {
                    Value = "value",
                };
                await client.Create("owner", "repo", "variable", createVariable);

                connection.Received()
                    .Put<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variable"), createVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                var createVariable = new CreateRepositoryVariable
                {
                    Value = "value",
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", null, createVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", "variable", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", "variable", new CreateRepositoryVariable()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "repo", "", createVariable));
            }
        }

        public class UpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);
                var updateVariable = new UpdateRepositoryVariable
                {
                    Value = "value",
                };
                await client.Update("owner", "repo", "variable", updateVariable);

                connection.Received()
                    .Put<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variable"), updateVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                var updateVariable = new UpdateRepositoryVariable
                {
                    Value = "value",
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "repo", "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", null, updateVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", "variable", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", "variable", new UpdateRepositoryVariable()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "repo", "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "repo", "", updateVariable));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);

                await client.Delete("owner", "repo", "variable");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "repo", ""));
            }
        }
    }
}
