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
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("org", options);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators"),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationOutsideCollaboratorsClient(Substitute.For<IGitHubClient>());


                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, null));

                Assert.Throws<ArgumentException>(() => client.GetAll(""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, ApiOptions.None));
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.GetAll("org", OrganizationMembersFilter.All);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("org", OrganizationMembersFilter.All, options);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationOutsideCollaboratorsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
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
