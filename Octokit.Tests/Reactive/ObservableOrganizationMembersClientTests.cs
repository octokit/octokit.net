using NSubstitute;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationMembersClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableOrganizationMembersClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.GetAll("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/members", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("org", options);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/members", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, ApiOptions.None));

                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, OrganizationMembersRole.Admin));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, OrganizationMembersRole.Admin, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Admin, null));

                Assert.Throws<ArgumentException>(() => client.GetAll(""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, OrganizationMembersRole.Admin));
                Assert.Throws<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, OrganizationMembersRole.Admin, ApiOptions.None));
            }

            [Fact]
            public void TwoFactorFilterRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IGitHubClient>();
                var orgMembersClient = new ObservableOrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                client.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/members?filter=2fa_disabled", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void MemberRoleFilterRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IGitHubClient>();
                var orgMembersClient = new ObservableOrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersRole.Member, options);

                client.Connection.Received().Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=member"), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void TwoFactorFilterPlusMemberRoleRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IGitHubClient>();
                var orgMembersClient = new ObservableOrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Member, options);

                client.Connection.Received().Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=member"), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.GetAllPublic("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/public_members", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllPublic("org", options);

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/public_members", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllPublic(null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllPublic(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllPublic("org", null));

                Assert.Throws<ArgumentException>(() => client.GetAllPublic(""));
                Assert.Throws<ArgumentException>(() => client.GetAllPublic("", ApiOptions.None));
            }
        }

        public class TheCheckMemberMethod
        {
            [Fact]
            public void ChecksMemberFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.CheckMember("org", "user");

                gitHubClient.Organization.Member.Received().CheckMember("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMember(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMember("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMember("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMember("org", "").ToTask());
            }
        }

        public class TheCheckMemberPublicMethod
        {
            [Fact]
            public void ChecksMemberPublicFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.CheckMemberPublic("org", "user");

                gitHubClient.Organization.Member.Received().CheckMemberPublic("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMemberPublic(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMemberPublic("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMemberPublic("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMemberPublic("org", "").ToTask());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Delete("org", "user");

                gitHubClient.Organization.Member.Received().Delete("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("org", "").ToTask());
            }
        }

        public class ThePublicizeMethod
        {
            [Fact]
            public void PublicizeFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Publicize("org", "user");

                gitHubClient.Organization.Member.Received().Publicize("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Publicize(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Publicize("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Publicize("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Publicize("org", "").ToTask());
            }
        }

        public class TheConcealMethod
        {
            [Fact]
            public void ConcealFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Conceal("org", "user");

                gitHubClient.Organization.Member.Received().Conceal("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Conceal(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Conceal("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Conceal("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Conceal("org", "").ToTask());
            }
        }
    }
}
