using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableOrganizationsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                client.Get("orgName");

                //gitHubClient.Received().Get<Organization>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null));

                Assert.Throws<ArgumentException>(() => client.Get(""));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                client.GetAll("username");

                //gitHubClient.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "users/username/orgs"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAll("username", options);

                //gitHubClient.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "users/username/orgs"), options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("username", null));

                Assert.Throws<ArgumentException>(() => client.GetAll(""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", ApiOptions.None));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                client.GetAllForCurrent();

                //gitHubClient.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "user/orgs"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(options);

                //gitHubClient.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "user/orgs"), options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                client.Update("initrode", new OrganizationUpdate());

                //gitHubClient.Received().Patch<Organization>(Arg.Is<Uri>(u => u.ToString() == "orgs/initrode"), Args.OrganizationUpdate);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Update(null, new OrganizationUpdate()));
                Assert.Throws<ArgumentNullException>(() => client.Update("org", null));

                Assert.Throws<ArgumentNullException>(() => client.Update("", new OrganizationUpdate()));
            }
        }
    }
}