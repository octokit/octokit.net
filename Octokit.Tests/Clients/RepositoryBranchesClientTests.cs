using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepositoryBranchesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryBranchesClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.GetAll("owner", "name");

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.GetAll(1);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("owner", "name", options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.Get("owner", "repo", "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), null);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.Get(1, "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", ""));
            }
        }

        public class TheGetBranchProtectectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetBranchProtection("owner", "repo", "branch");

                connection.Received()
                    .Get<BranchProtectionSettings>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetBranchProtection(1, "branch");

                connection.Received()
                    .Get<BranchProtectionSettings>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranchProtection(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranchProtection("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranchProtection("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranchProtection(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranchProtection("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranchProtection("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranchProtection("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranchProtection(1, ""));
            }
        }

        public class TheUpdateBranchProtectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

                client.UpdateBranchProtection("owner", "repo", "branch", update);

                connection.Received()
                    .Put<BranchProtectionSettings>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection"), Arg.Any<BranchProtectionSettingsUpdate>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

                client.UpdateBranchProtection(1, "branch", update);

                connection.Received()
                    .Put<BranchProtectionSettings>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection"), Arg.Any<BranchProtectionSettingsUpdate>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" }));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateBranchProtection(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateBranchProtection("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateBranchProtection("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateBranchProtection("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateBranchProtection(1, "", update));
            }
        }

        public class TheDeleteBranchProtectectionMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteBranchProtection("owner", "repo", "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteBranchProtection(1, "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteBranchProtection(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteBranchProtection("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteBranchProtection("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteBranchProtection(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteBranchProtection("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteBranchProtection("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteBranchProtection("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteBranchProtection(1, ""));
            }
        }

        public class TheGetRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetRequiredStatusChecks("owner", "repo", "branch");

                connection.Received()
                    .Get<BranchProtectionRequiredStatusChecks>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetRequiredStatusChecks(1, "branch");

                connection.Received()
                    .Get<BranchProtectionRequiredStatusChecks>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRequiredStatusChecks(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRequiredStatusChecks("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRequiredStatusChecks("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRequiredStatusChecks(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRequiredStatusChecks("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRequiredStatusChecks("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRequiredStatusChecks("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRequiredStatusChecks(1, ""));
            }
        }

        public class TheUpdateRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                client.UpdateRequiredStatusChecks("owner", "repo", "branch", update);

                connection.Received()
                    .Patch<BranchProtectionRequiredStatusChecks>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks"), Arg.Any<BranchProtectionRequiredStatusChecksUpdate>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                client.UpdateRequiredStatusChecks(1, "branch", update);

                connection.Received()
                    .Patch<BranchProtectionRequiredStatusChecks>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks"), Arg.Any<BranchProtectionRequiredStatusChecksUpdate>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "test" });

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecks(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecks("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecks("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecks("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecks(1, "", update));
            }
        }

        public class TheDeleteRequiredStatusChecksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteRequiredStatusChecks("owner", "repo", "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteRequiredStatusChecks(1, "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecks(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecks("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecks("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecks(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecks("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecks("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecks("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecks(1, ""));
            }
        }

        public class TheGetAllRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllRequiredStatusChecksContexts("owner", "repo", "branch");

                connection.Received()
                    .Get<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks/contexts"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllRequiredStatusChecksContexts(1, "branch");

                connection.Received()
                    .Get<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks/contexts"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllRequiredStatusChecksContexts(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllRequiredStatusChecksContexts(1, ""));
            }
        }

        public class TheUpdateRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new List<string>() { "test" };

                client.UpdateRequiredStatusChecksContexts("owner", "repo", "branch", update);

                connection.Received()
                    .Put<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new List<string>() { "test" };

                client.UpdateRequiredStatusChecksContexts(1, "branch", update);

                connection.Received()
                    .Put<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var update = new List<string>() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateRequiredStatusChecksContexts(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateRequiredStatusChecksContexts(1, "", update));
            }
        }

        public class TheAddRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newContexts = new List<string>() { "test" };

                client.AddRequiredStatusChecksContexts("owner", "repo", "branch", newContexts);

                connection.Received()
                    .Post<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newContexts = new List<string>() { "test" };

                client.AddRequiredStatusChecksContexts(1, "branch", newContexts);

                connection.Received()
                    .Post<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var newContexts = new List<string>() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(null, "repo", "branch", newContexts));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", null, "branch", newContexts));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", null, newContexts));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(1, null, newContexts));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRequiredStatusChecksContexts(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRequiredStatusChecksContexts("", "repo", "branch", newContexts));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRequiredStatusChecksContexts("owner", "", "branch", newContexts));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRequiredStatusChecksContexts("owner", "repo", "", newContexts));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRequiredStatusChecksContexts(1, "", newContexts));
            }
        }

        public class TheDeleteRequiredStatusChecksContextsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var contextsToRemove = new List<string>() { "test" };

                client.DeleteRequiredStatusChecksContexts("owner", "repo", "branch", contextsToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var contextsToRemove = new List<string>() { "test" };

                client.DeleteRequiredStatusChecksContexts(1, "branch", contextsToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_status_checks/contexts"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var contextsToRemove = new List<string>() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(null, "repo", "branch", contextsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", null, "branch", contextsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", null, contextsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(1, null, contextsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRequiredStatusChecksContexts(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("", "repo", "branch", contextsToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("owner", "", "branch", contextsToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts("owner", "repo", "", contextsToRemove));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRequiredStatusChecksContexts(1, "", contextsToRemove));
            }
        }

        public class TheGetReviewEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetReviewEnforcement("owner", "repo", "branch");

                connection.Received()
                    .Get<BranchProtectionRequiredReviews>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_pull_request_reviews"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetReviewEnforcement(1, "branch");

                connection.Received()
                    .Get<BranchProtectionRequiredReviews>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_pull_request_reviews"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewEnforcement(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewEnforcement("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewEnforcement("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewEnforcement(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewEnforcement("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewEnforcement("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewEnforcement("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewEnforcement(1, ""));
            }
        }

        public class TheUpdateReviewEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                client.UpdateReviewEnforcement("owner", "repo", "branch", update);

                connection.Received()
                    .Patch<BranchProtectionRequiredReviews>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_pull_request_reviews"), Arg.Any<BranchProtectionRequiredReviewsUpdate>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                client.UpdateReviewEnforcement(1, "branch", update);

                connection.Received()
                    .Patch<BranchProtectionRequiredReviews>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_pull_request_reviews"), Arg.Any<BranchProtectionRequiredReviewsUpdate>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                var update = new BranchProtectionRequiredReviewsUpdate(false, false, 2);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateReviewEnforcement(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateReviewEnforcement("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateReviewEnforcement("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateReviewEnforcement("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateReviewEnforcement(1, "", update));
            }
        }

        public class TheRemoveReviewEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.RemoveReviewEnforcement("owner", "repo", "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/required_pull_request_reviews"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.RemoveReviewEnforcement(1, "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/required_pull_request_reviews"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveReviewEnforcement(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveReviewEnforcement("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveReviewEnforcement("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveReviewEnforcement(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveReviewEnforcement("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveReviewEnforcement("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveReviewEnforcement("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveReviewEnforcement(1, ""));
            }
        }

        public class TheGetAdminEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAdminEnforcement("owner", "repo", "branch");

                connection.Received()
                    .Get<EnforceAdmins>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/enforce_admins"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAdminEnforcement(1, "branch");

                connection.Received()
                    .Get<EnforceAdmins>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/enforce_admins"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAdminEnforcement(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAdminEnforcement("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAdminEnforcement("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAdminEnforcement(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAdminEnforcement("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAdminEnforcement("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAdminEnforcement("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAdminEnforcement(1, ""));
            }
        }

        public class TheAddAdminEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.AddAdminEnforcement("owner", "repo", "branch");

                connection.Received()
                    .Post<EnforceAdmins>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/enforce_admins"), Arg.Any<object>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.AddAdminEnforcement(1, "branch");

                connection.Received()
                    .Post<EnforceAdmins>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/enforce_admins"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAdminEnforcement(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAdminEnforcement("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAdminEnforcement("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAdminEnforcement(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAdminEnforcement("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAdminEnforcement("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAdminEnforcement("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAdminEnforcement(1, ""));
            }
        }

        public class TheRemoveAdminEnforcementMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.RemoveAdminEnforcement("owner", "repo", "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/enforce_admins"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.RemoveAdminEnforcement(1, "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/enforce_admins"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAdminEnforcement(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAdminEnforcement("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAdminEnforcement("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAdminEnforcement(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAdminEnforcement("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAdminEnforcement("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAdminEnforcement("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAdminEnforcement(1, ""));
            }
        }

        public class TheGetProtectedBranchRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetProtectedBranchRestrictions("owner", "repo", "branch");

                connection.Received()
                    .Get<BranchProtectionPushRestrictions>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetProtectedBranchRestrictions(1, "branch");

                connection.Received()
                    .Get<BranchProtectionPushRestrictions>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetProtectedBranchRestrictions(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetProtectedBranchRestrictions("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetProtectedBranchRestrictions("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetProtectedBranchRestrictions(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetProtectedBranchRestrictions("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetProtectedBranchRestrictions("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetProtectedBranchRestrictions("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetProtectedBranchRestrictions(1, ""));
            }
        }

        public class TheDeleteProtectedBranchRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteProtectedBranchRestrictions("owner", "repo", "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.DeleteProtectedBranchRestrictions(1, "branch");

                connection.Connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchRestrictions(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchRestrictions("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchRestrictions(1, ""));
            }
        }

        public class TheGetAllProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllProtectedBranchTeamRestrictions("owner", "repo", "branch");

                connection.Received()
                    .Get<IReadOnlyList<Team>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/teams"),
                    null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllProtectedBranchTeamRestrictions(1, "branch");

                connection.Received()
                    .Get<IReadOnlyList<Team>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/teams"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchTeamRestrictions(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchTeamRestrictions(1, ""));
            }
        }

        public class TheSetProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);

                connection.Received()
                    .Put<IReadOnlyList<Team>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/teams"),
                    Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.UpdateProtectedBranchTeamRestrictions(1, "branch", newTeams);

                connection.Received()
                    .Put<IReadOnlyList<Team>>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/teams"),
                        Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(null, "repo", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", null, "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", null, newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(1, null, newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchTeamRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("", "repo", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions("owner", "repo", "", newTeams));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchTeamRestrictions(1, "", newTeams));
            }
        }

        public class TheAddProtectedBranchTeamRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.AddProtectedBranchTeamRestrictions("owner", "repo", "branch", newTeams);

                connection.Received()
                    .Post<IReadOnlyList<Team>>(
                        Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/teams"),
                        Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                client.AddProtectedBranchTeamRestrictions(1, "branch", newTeams);

                connection.Received()
                    .Post<IReadOnlyList<Team>>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/teams"),
                        Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var newTeams = new BranchProtectionTeamCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(null, "repo", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", null, "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", null, newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(1, null, newTeams));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchTeamRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("", "repo", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("owner", "", "branch", newTeams));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions("owner", "repo", "", newTeams));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchTeamRestrictions(1, "", newTeams));
            }
        }

        public class TheDeleteProtectedBranchTeamRestrictions
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "branch", teamsToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<Team>>(
                        Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/teams"),
                        Arg.Any<BranchProtectionTeamCollection>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                client.DeleteProtectedBranchTeamRestrictions(1, "branch", teamsToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<Team>>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/teams"),
                        Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var teamsToRemove = new BranchProtectionTeamCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(null, "repo", "branch", teamsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", null, "branch", teamsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", null, teamsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(1, null, teamsToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchTeamRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("", "repo", "branch", teamsToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "", "branch", teamsToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions("owner", "repo", "", teamsToRemove));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchTeamRestrictions(1, "", teamsToRemove));
            }
        }

        public class TheGetAllProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllProtectedBranchUserRestrictions("owner", "repo", "branch");

                connection.Received()
                    .Get<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/users"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                client.GetAllProtectedBranchUserRestrictions(1, "branch");

                connection.Received()
                    .Get<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/users"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllProtectedBranchUserRestrictions(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions("owner", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllProtectedBranchUserRestrictions(1, ""));
            }
        }

        public class TheSetProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.UpdateProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);

                connection.Received()
                    .Put<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.UpdateProtectedBranchUserRestrictions(1, "branch", newUsers);

                connection.Received()
                    .Put<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var newUsers = new BranchProtectionUserCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(null, "repo", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", null, "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", null, newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(1, null, newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProtectedBranchUserRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("", "repo", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions("owner", "repo", "", newUsers));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateProtectedBranchUserRestrictions(1, "", newUsers));
            }
        }

        public class TheAddProtectedBranchUserRestrictionsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.AddProtectedBranchUserRestrictions("owner", "repo", "branch", newUsers);

                connection.Received()
                    .Post<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var newUsers = new BranchProtectionUserCollection() { "test" };

                client.AddProtectedBranchUserRestrictions(1, "branch", newUsers);

                connection.Received()
                    .Post<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var newUsers = new BranchProtectionUserCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(null, "repo", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", null, "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", null, newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(1, null, newUsers));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddProtectedBranchUserRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("", "repo", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("owner", "", "branch", newUsers));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchUserRestrictions("owner", "repo", "", newUsers));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddProtectedBranchUserRestrictions(1, "", newUsers));
            }
        }

        public class TheDeleteProtectedBranchUserRestrictions
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                client.DeleteProtectedBranchUserRestrictions("owner", "repo", "branch", usersToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                client.DeleteProtectedBranchUserRestrictions(1, "branch", usersToRemove);

                connection.Received()
                    .Delete<IReadOnlyList<User>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch/protection/restrictions/users"), Arg.Any<IReadOnlyList<string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var usersToRemove = new BranchProtectionUserCollection() { "test" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(null, "repo", "branch", usersToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", null, "branch", usersToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", null, usersToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(1, null, usersToRemove));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteProtectedBranchUserRestrictions(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("", "repo", "branch", usersToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "", "branch", usersToRemove));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions("owner", "repo", "", usersToRemove));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteProtectedBranchUserRestrictions(1, "", usersToRemove));
            }
        }
    }
}
