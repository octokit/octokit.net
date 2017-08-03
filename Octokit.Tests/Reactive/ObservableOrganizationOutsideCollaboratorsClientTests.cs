using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationOutsideCollaboratorsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(()
                    => new ObservableOrganizationOutsideCollaboratorsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.GetAll("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators"),
                    Args.EmptyDictionary,
                    "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationOutsideCollaboratorsClient(Substitute.For<IGitHubClient>());


                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));

                Assert.Throws<ArgumentException>(() => client.GetAll(""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.GetAll("org", OrganizationMembersFilter.All);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"), 
                    Args.EmptyDictionary, 
                    "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"), 
                    Args.EmptyDictionary, 
                    "application/vnd.github.korra-preview+json");
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.Delete("org", "user");

                gitHubClient.Organization.OutsideCollaborator.Received().Delete(
                    Arg.Is("org"),
                    Arg.Is("user"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationOutsideCollaboratorsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "user"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("org", null));

                Assert.Throws<ArgumentException>(() => client.Delete("", "user"));
                Assert.Throws<ArgumentException>(() => client.Delete("org", ""));
            }
        }

        public class TheConvertFromMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.ConvertFromMember("org", "user");

                gitHubClient.Organization.OutsideCollaborator.Received().ConvertFromMember(
                    Arg.Is("org"),
                    Arg.Is("user"));
            }

            [Fact]
            public void EnsuresNonNullArgument()
            {
                var client = new ObservableOrganizationOutsideCollaboratorsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.ConvertFromMember(null, "user"));
                Assert.Throws<ArgumentNullException>(() => client.ConvertFromMember("org", null));

                Assert.Throws<ArgumentException>(() => client.ConvertFromMember("", "user"));
                Assert.Throws<ArgumentException>(() => client.ConvertFromMember("org", ""));
            }
        }
    }
}
