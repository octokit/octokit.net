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

        public class GetAllOrganizationMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);

                await client.GetAllOrganization("owner", "repo");

                connection.Received()
                    .Get<RepositoryVariablesCollection>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/organization-variables"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllOrganization(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllOrganization("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllOrganization("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllOrganization("owner", ""));
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

                await client.Get("owner", "repo", "variableName");

                connection.Received()
                    .Get<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variableName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "variableName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "variableName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "variableName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "variableName"));
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
                var newVariable = new Variable("variableName", "variableValue");

                await client.Create("owner", "repo", newVariable);

                connection.Received()
                    .Post<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables"), newVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                var updatedVariable = new Variable("variableName", "variableValue");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", updatedVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, updatedVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", new Variable(null, "variableValue")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "repo", new Variable("variableName", null)));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", updatedVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "",  updatedVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "repo", new Variable("", "variableValue")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "repo", new Variable("variableName", "")));
            }
        }

        public class UpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);
                var variable = new Variable("variableName", "variableValue");

                await client.Update("owner", "repo", variable);

                connection.Received()
                    .Patch<RepositoryVariable>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variableName"), variable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                var updatedVariable = new Variable("variableName", "variableValue");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "repo", updatedVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, updatedVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", new Variable(null, "value")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "repo", new Variable("name", null)));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "repo", updatedVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", updatedVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "repo", new Variable("", "value")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "repo", new Variable("name", "")));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryVariablesClient(connection);

                await client.Delete("owner", "repo", "variableName");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/variables/variableName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", "variableName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "variableName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", "variableName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "variableName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "repo", ""));
            }
        }
    }
}
