using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseOrganizationClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseOrganizationClient(connection);

                string expectedUri = "admin/organizations";
                client.Create(new NewOrganization("org", "admin", "org name"));
                
                connection.Received().Post<Organization>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseOrganizationClient(connection);

                client.Create(new NewOrganization("org", "admin", "org name"));

                connection.Received().Post<Organization>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewOrganization>(a =>
                        a.Login == "org"
                        && a.Admin == "admin"
                        && a.ProfileName == "org name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseOrganizationClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }
        }
    }
}
