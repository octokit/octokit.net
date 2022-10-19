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

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var expected = new Uri("repositories/1/branches", UriKind.Relative);

                client.GetAll(1);

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Args.EmptyDictionary);
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

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
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

                gitHubClient.Connection.Received(1).Get<List<Branch>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
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
                var update = new BranchProtectionSettingsUpdate(new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

                client.UpdateBranchProtection("owner", "repo", "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateBranchProtection("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionSettingsUpdate(new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

                client.UpdateBranchProtection(1, "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateBranchProtection(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new BranchProtectionSettingsUpdate(new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

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

        public class TheGetRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetRequiredStatusChecks("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetRequiredStatusChecks("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetRequiredStatusChecks(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetRequiredStatusChecks(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetRequiredStatusChecks(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetRequiredStatusChecks("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetRequiredStatusChecks("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetRequiredStatusChecks(1, null));

                Assert.Throws<ArgumentException>(() => client.GetRequiredStatusChecks("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetRequiredStatusChecks("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetRequiredStatusChecks("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetRequiredStatusChecks(1, ""));
            }
        }

        public class TheUpdateRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                client.UpdateRequiredStatusChecks("owner", "repo", "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateRequiredStatusChecks("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                client.UpdateRequiredStatusChecks(1, "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateRequiredStatusChecks(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(1, null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecks("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecks("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecks("owner", "repo", "", update));

                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecks(1, "", update));
            }
        }

        public class TheDeleteRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteRequiredStatusChecks("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteRequiredStatusChecks("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteRequiredStatusChecks(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteRequiredStatusChecks(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecks(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecks("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecks("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecks(1, null));

                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecks("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecks("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecks("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecks(1, ""));
            }
        }

        public class TheGetAllRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllRequiredStatusChecksContexts("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllRequiredStatusChecksContexts("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllRequiredStatusChecksContexts(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllRequiredStatusChecksContexts(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts(1, ""));
            }
        }

        public class TheUpdateRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new List<string>() { "test" };

                client.UpdateRequiredStatusChecksContexts("owner", "repo", "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateRequiredStatusChecksContexts("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new List<string>() { "test" };

                client.UpdateRequiredStatusChecksContexts(1, "branch", update);

                gitHubClient.Repository.Branch.Received()
                    .UpdateRequiredStatusChecksContexts(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new List<string>() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(1, null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", "", update));

                Assert.Throws<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts(1, "", update));
            }
        }

        public class TheAddRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newContexts = new List<string>() { "test" };

                client.AddRequiredStatusChecksContexts("owner", "repo", "branch", newContexts);

                gitHubClient.Repository.Branch.Received()
                    .AddRequiredStatusChecksContexts("owner", "repo", "branch", newContexts);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newContexts = new List<string>() { "test" };

                client.AddRequiredStatusChecksContexts(1, "branch", newContexts);

                gitHubClient.Repository.Branch.Received()
                    .AddRequiredStatusChecksContexts(1, "branch", newContexts);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var newContexts = new List<string>() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(null, "repo", "branch", newContexts));
                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", null, "branch", newContexts));
                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", null, newContexts));
                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(1, null, newContexts));
                Assert.Throws<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.AddRequiredStatusChecksContexts("", "repo", "branch", newContexts));
                Assert.Throws<ArgumentException>(() => client.AddRequiredStatusChecksContexts("owner", "", "branch", newContexts));
                Assert.Throws<ArgumentException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", "", newContexts));

                Assert.Throws<ArgumentException>(() => client.AddRequiredStatusChecksContexts(1, "", newContexts));
            }
        }

        public class TheDeleteRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var contextsToRemove = new List<string>() { "test" };

                client.DeleteRequiredStatusChecksContexts("owner", "repo", "branch", contextsToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteRequiredStatusChecksContexts("owner", "repo", "branch", contextsToRemove);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var contextsToRemove = new List<string>() { "test" };

                client.DeleteRequiredStatusChecksContexts(1, "branch", contextsToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteRequiredStatusChecksContexts(1, "branch", contextsToRemove);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var contextsToRemove = new List<string>() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(null, "repo", "branch", contextsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", null, "branch", contextsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", null, contextsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(1, null, contextsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("", "repo", "branch", contextsToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("owner", "", "branch", contextsToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", "", contextsToRemove));

                Assert.Throws<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts(1, "", contextsToRemove));
            }
        }

        public class TheGetReviewEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetReviewEnforcement("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received().GetReviewEnforcement("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetReviewEnforcement(1, "branch");

                gitHubClient.Repository.Branch.Received().GetReviewEnforcement(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetReviewEnforcement(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetReviewEnforcement("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetReviewEnforcement("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetReviewEnforcement(1, null));

                Assert.Throws<ArgumentException>(() => client.GetReviewEnforcement("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetReviewEnforcement("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetReviewEnforcement("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetReviewEnforcement(1, ""));
            }
        }

        public class TheUpdateReviewEnforcement
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                client.UpdateReviewEnforcement("owner", "repo", "branch", update);

                gitHubClient.Repository.Branch.Received().UpdateReviewEnforcement("owner", "repo", "branch", update);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                client.UpdateReviewEnforcement(1, "branch", update);

                gitHubClient.Repository.Branch.Received().UpdateReviewEnforcement(1, "branch", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement(1, null, update));
                Assert.Throws<ArgumentNullException>(() => client.UpdateReviewEnforcement(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateReviewEnforcement("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateReviewEnforcement("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.UpdateReviewEnforcement("owner", "repo", "", update));

                Assert.Throws<ArgumentException>(() => client.UpdateReviewEnforcement(1, "", update));
            }
        }

        public class TheRemoveReviewEnforcement
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.RemoveReviewEnforcement("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received().RemoveReviewEnforcement("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.RemoveReviewEnforcement(1, "branch");

                gitHubClient.Repository.Branch.Received().RemoveReviewEnforcement(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RemoveReviewEnforcement(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveReviewEnforcement("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveReviewEnforcement("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.RemoveReviewEnforcement(1, null));

                Assert.Throws<ArgumentException>(() => client.RemoveReviewEnforcement("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.RemoveReviewEnforcement("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.RemoveReviewEnforcement("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.RemoveReviewEnforcement(1, ""));
            }
        }

        public class TheGetAdminEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAdminEnforcement("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received().GetAdminEnforcement("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAdminEnforcement(1, "branch");

                gitHubClient.Repository.Branch.Received().GetAdminEnforcement(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAdminEnforcement(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAdminEnforcement("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAdminEnforcement("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAdminEnforcement(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAdminEnforcement("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAdminEnforcement("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAdminEnforcement("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetAdminEnforcement(1, ""));
            }
        }

        public class TheAddAdminEnforcement
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.AddAdminEnforcement("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received().AddAdminEnforcement("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.AddAdminEnforcement(1, "branch");

                gitHubClient.Repository.Branch.Received().AddAdminEnforcement(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.AddAdminEnforcement(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.AddAdminEnforcement("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.AddAdminEnforcement("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.AddAdminEnforcement(1, null));

                Assert.Throws<ArgumentException>(() => client.AddAdminEnforcement("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.AddAdminEnforcement("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.AddAdminEnforcement("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.AddAdminEnforcement(1, ""));
            }
        }

        public class TheRemoveAdminEnforcement
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.RemoveAdminEnforcement("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received().RemoveAdminEnforcement("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.RemoveAdminEnforcement(1, "branch");

                gitHubClient.Repository.Branch.Received().RemoveAdminEnforcement(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RemoveAdminEnforcement(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveAdminEnforcement("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveAdminEnforcement("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.RemoveAdminEnforcement(1, null));

                Assert.Throws<ArgumentException>(() => client.RemoveAdminEnforcement("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.RemoveAdminEnforcement("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.RemoveAdminEnforcement("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.RemoveAdminEnforcement(1, ""));
            }
        }

        public class TheGetProtectedBranchRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetProtectedBranchRestrictions("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetProtectedBranchRestrictions("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetProtectedBranchRestrictions(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetProtectedBranchRestrictions(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetProtectedBranchRestrictions(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetProtectedBranchRestrictions("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetProtectedBranchRestrictions("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetProtectedBranchRestrictions(1, null));

                Assert.Throws<ArgumentException>(() => client.GetProtectedBranchRestrictions("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetProtectedBranchRestrictions("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetProtectedBranchRestrictions("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetProtectedBranchRestrictions(1, ""));
            }
        }

        public class TheDeleteProtectedBranchRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteProtectedBranchRestrictions("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchRestrictions("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.DeleteProtectedBranchRestrictions(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchRestrictions(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions(1, null));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchRestrictions(1, ""));
            }
        }

        public class TheGetAllProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllProtectedBranchTeamRestrictions("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllProtectedBranchTeamRestrictions("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllProtectedBranchTeamRestrictions(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllProtectedBranchTeamRestrictions(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions(1, ""));
            }
        }

        public class TheSetProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);

                gitHubClient.Repository.Branch.Received()
                    .UpdateProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.UpdateProtectedBranchTeamRestrictions(1, "branch", newTeams);

                gitHubClient.Repository.Branch.Received()
                    .UpdateProtectedBranchTeamRestrictions(1, "branch", newTeams);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(null, "repo", "branch", newTeams));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", null, "branch", newTeams));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", null, newTeams));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(1, null, newTeams));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("", "repo", "branch", newTeams));
                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "", "branch", newTeams));
                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "", newTeams));

                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions(1, "", newTeams));
            }
        }

        public class TheAddProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.AddProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);

                gitHubClient.Repository.Branch.Received()
                    .AddProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.AddProtectedBranchTeamRestrictions(1, "branch", newTeams);

                gitHubClient.Repository.Branch.Received()
                    .AddProtectedBranchTeamRestrictions(1, "branch", newTeams);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(null, "repo", "branch", newTeams));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", null, "branch", newTeams));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", null, newTeams));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(1, null, newTeams));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("", "repo", "branch", newTeams));
                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("owner", "", "branch", newTeams));
                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", "", newTeams));

                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions(1, "", newTeams));
            }
        }

        public class TheDeleteProtectedBranchTeamRestrictions
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "branch", teamsToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchTeamRestrictions("owner", "repo", "branch", teamsToRemove);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                client.DeleteProtectedBranchTeamRestrictions(1, "branch", teamsToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchTeamRestrictions(1, "branch", teamsToRemove);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(null, "repo", "branch", teamsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", null, "branch", teamsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", null, teamsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(1, null, teamsToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("", "repo", "branch", teamsToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "", "branch", teamsToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "", teamsToRemove));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions(1, "", teamsToRemove));
            }
        }

        public class TheGetAllProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllProtectedBranchUserRestrictions("owner", "repo", "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllProtectedBranchUserRestrictions("owner", "repo", "branch");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);

                client.GetAllProtectedBranchUserRestrictions(1, "branch");

                gitHubClient.Repository.Branch.Received()
                    .GetAllProtectedBranchUserRestrictions(1, "branch");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions(1, ""));
            }
        }

        public class TheSetProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.UpdateProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);

                gitHubClient.Repository.Branch.Received()
                    .UpdateProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.UpdateProtectedBranchUserRestrictions(1, "branch", newUsers);

                gitHubClient.Repository.Branch.Received()
                    .UpdateProtectedBranchUserRestrictions(1, "branch", newUsers);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var newUsers = new BranchProtectionUserCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(null, "repo", "branch", newUsers));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", null, "branch", newUsers));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", null, newUsers));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(1, null, newUsers));
                Assert.Throws<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("", "repo", "branch", newUsers));
                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "", "branch", newUsers));
                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", "", newUsers));

                Assert.Throws<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions(1, "", newUsers));
            }
        }

        public class TheAddProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.AddProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);

                gitHubClient.Repository.Branch.Received()
                    .AddProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.AddProtectedBranchUserRestrictions(1, "branch", newUsers);

                gitHubClient.Repository.Branch.Received()
                    .AddProtectedBranchUserRestrictions(1, "branch", newUsers);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var newUsers = new BranchProtectionUserCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(null, "repo", "branch", newUsers));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", null, "branch", newUsers));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", null, newUsers));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(1, null, newUsers));
                Assert.Throws<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("", "repo", "branch", newUsers));
                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("owner", "", "branch", newUsers));
                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", "", newUsers));

                Assert.Throws<ArgumentException>(() => client.AddProtectedBranchUserRestrictions(1, "", newUsers));
            }
        }

        public class TheDeleteProtectedBranchUserRestrictions
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                client.DeleteProtectedBranchUserRestrictions("owner", "repo", "branch", usersToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchUserRestrictions("owner", "repo", "branch", usersToRemove);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryBranchesClient(gitHubClient);
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                client.DeleteProtectedBranchUserRestrictions(1, "branch", usersToRemove);

                gitHubClient.Repository.Branch.Received()
                    .DeleteProtectedBranchUserRestrictions(1, "branch", usersToRemove);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryBranchesClient(Substitute.For<IGitHubClient>());
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(null, "repo", "branch", usersToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", null, "branch", usersToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", null, usersToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(1, null, usersToRemove));
                Assert.Throws<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(1, "branch", null));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("", "repo", "branch", usersToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "", "branch", usersToRemove));
                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", "", usersToRemove));

                Assert.Throws<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions(1, "", usersToRemove));
            }
        }
    }
}
