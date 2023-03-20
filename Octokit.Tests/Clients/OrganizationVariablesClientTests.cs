using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationVariablesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationVariablesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.GetAll("org");

                connection.Received()
                    .Get<OrganizationVariablesCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.Get("org", "variable");

                connection.Received()
                    .Get<OrganizationVariable>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", ""));
            }
        }

        public class CreateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);
                var createVariable = new CreateOrganizationVariable
                {
                    Value = "value",
                    Visibility = "private"
                };
                await client.Create("org", "variable", createVariable);

                connection.Received()
                    .Put<OrganizationVariable>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable"), createVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                var createVariable = new CreateOrganizationVariable
                {
                    Value = "value",
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, createVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "variable", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "variable", new CreateOrganizationVariable()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "variable", createVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", createVariable));
            }
        }
        public class UpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);
                var updateVariable = new UpdateOrganizationVariable
                {
                    Value = "value",
                    Visibility = "private"
                };
                await client.Update("org", "variable", updateVariable);

                connection.Received()
                    .Put<OrganizationVariable>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable"), updateVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                var updateVariable = new UpdateOrganizationVariable
                {
                    Value = "value",
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, updateVariable));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "variable", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "variable", new UpdateOrganizationVariable()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "variable", updateVariable));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", updateVariable));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.Delete("org", "variable");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", ""));
            }
        }

        public class GetSelectedRepositoriesForVariableMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.GetSelectedRepositoriesForVariable("org", "variable");

                connection.Received()
                    .Get<OrganizationVariableRepositoryCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable/repositories"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForVariable(null, "variable"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForVariable("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForVariable("", "variable"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForVariable("org", ""));
            }
        }

        public class SetSelectedRepositoriesForVariableMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                var repoIds = new List<long>
                    {
                        1,
                        2,
                        3
                    };
                var repos = new SelectedRepositoryCollection(repoIds);

                await client.SetSelectedRepositoriesForVariable("org", "variable", repos);

                connection.Received()
                    .Put<SelectedRepositoryCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable/repositories"), repos);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                var repoIds = new List<long>
                    {
                        1,
                        2,
                        3
                    };
                var repos = new SelectedRepositoryCollection(repoIds);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForVariable(null, "variable", repos));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForVariable("org", null, repos));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForVariable("org", "variable", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForVariable("org", "variable", new SelectedRepositoryCollection()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.SetSelectedRepositoriesForVariable("", "variable", repos));
                await Assert.ThrowsAsync<ArgumentException>(() => client.SetSelectedRepositoriesForVariable("org", "", repos));
            }
        }

        public class AddRepoToOrganizationVariableMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.AddRepoToOrganizationVariable("org", "variable", 1);

                connection.Received()
                    .Put(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable/repositories/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepoToOrganizationVariable(null, "variable", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepoToOrganizationVariable("org", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRepoToOrganizationVariable("", "variable", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRepoToOrganizationVariable("org", "", 1));
            }
        }

        public class RemoveRepoFromOrganizationVariableMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationVariablesClient(connection);

                await client.RemoveRepoFromOrganizationVariable("org", "variable", 1);

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/variables/variable/repositories/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationVariablesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepoFromOrganizationVariable(null, "variable", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepoFromOrganizationVariable("org", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepoFromOrganizationVariable("", "variable", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepoFromOrganizationVariable("org", "", 1));
            }
        }
    }
}
