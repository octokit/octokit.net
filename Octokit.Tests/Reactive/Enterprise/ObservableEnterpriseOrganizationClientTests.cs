using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseOrganizationClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseOrganizationClient(github);

                client.Create(new NewOrganization("org", "admin", "org name"));
                github.Enterprise.Organization.Received(1).Create(
                    Arg.Is<NewOrganization>(a => 
                        a.Login == "org" 
                        && a.Admin == "admin" 
                        && a.ProfileName == "org name"));
            }
        }
    }
}
