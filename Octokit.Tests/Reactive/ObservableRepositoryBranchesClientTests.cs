using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryBranchesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableRepositoryBranchesClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/branches", UriKind.Relative);

                client.GetAll("owner", "repo");

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var expected = new Uri("repositories/1/branches", UriKind.Relative);

                client.GetAll(1);

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var expected = new Uri("repos/owner/name/branches", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("owner", "name", options);

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var expected = new Uri("repositories/1/branches", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll(1, options);

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"), null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(github);

                client.Get("owner", "repo", "branch");

                github.Repository.Branch.Received(1).Get("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(github);

                client.Get(1, "branch");

                github.Repository.Branch.Received(1).Get(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(github);
                var update = new BranchUpdate();

                client.Edit("owner", "repo", "branch", update);

                github.Repository.Branch.Received(1).Edit("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(github);
                var update = new BranchUpdate();

                client.Edit(1, "branch", update);

                github.Repository.Branch.Received(1).Edit(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new BranchUpdate();

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, null, update));
                Assert.Throws<ArgumentNullException>(() => client.Edit(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.Edit("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "repo", "", update));

                Assert.Throws<ArgumentException>(() => client.Edit(1, "", update));
            }
        }

        public class TheGetBranchProtectectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                
                client.GetBranchProtection("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetBranchProtection("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetBranchProtection(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetBranchProtection(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetBranchProtection(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranchProtection("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranchProtection("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetBranchProtection(1, null));

                Assert.Throws<ArgumentException>(() => client.GetBranchProtection("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranchProtection("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranchProtection("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetBranchProtection(1, ""));
            }
        }

        public class TheUpdateBranchProtectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, true, new[] { "test" }));

                client.UpdateBranchProtection("owner", "repo", "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateBranchProtection("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, true, new[] { "test" }));

                client.UpdateBranchProtection(1, "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateBranchProtection(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, true, new[] { "test" }));

                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection(1, null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateBranchProtection(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateBranchProtection("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateBranchProtection("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateBranchProtection("owner", "repo", "", update));

                Assert.Throws<ArgumentException>(() => client.UpdateBranchProtection(1, "", update));
            }
        }

        public class TheDeleteBranchProtectectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteBranchProtection("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteBranchProtection("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteBranchProtection(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteBranchProtection(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteBranchProtection(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteBranchProtection("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteBranchProtection("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteBranchProtection(1, null));

                Assert.Throws<ArgumentException>(() => client.DeleteBranchProtection("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteBranchProtection("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteBranchProtection("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.DeleteBranchProtection(1, ""));
            }
        }
    }
}
