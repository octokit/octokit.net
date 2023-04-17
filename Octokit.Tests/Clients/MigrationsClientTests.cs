using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MigrationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new EventsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                client.Get("fake", 69);

                connection.Received().Get<Migration>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations/69"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, 69));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", 69));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                client.GetAll("fake");

                connection.Received().GetAll<Migration>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAll("fake", options);

                connection.Received().GetAll<Migration>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations"),
                    null,
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("fake", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class TheStartNewMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);
                var migrationRequest = new StartMigrationRequest(new List<string> { "fake/repo" });

                client.Start("fake", migrationRequest);

                connection.Received().Post<Migration>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations"),
                    Arg.Any<StartMigrationRequest>());
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);
                var migrationRequest = new StartMigrationRequest(new List<string> { "fake/repo" });

                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Start(null, migrationRequest));
                await Assert.ThrowsAsync<ArgumentException>(
                    () => client.Start("", migrationRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Start("fake", null));
            }

            [Fact]
            public void PassesRequestBody()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);
                var migrationRequest = new StartMigrationRequest(new List<string> { "fake/repo" });

                client.Start("fake", migrationRequest);

                connection.Received().Post<Migration>(
                    Arg.Any<Uri>(),
                    Arg.Is<StartMigrationRequest>(m =>
                        m.Repositories.Equals(migrationRequest.Repositories) &&
                        m.LockRepositories == migrationRequest.LockRepositories &&
                        m.ExcludeAttachments == migrationRequest.ExcludeAttachments));
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                client.GetArchive("fake", 69);

                connection.Connection.Received().GetRaw(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations/69/archive"), null);
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(null, 69));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("", 69));
            }
        }

        public class TheDeleteArchiveMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                client.DeleteArchive("fake", 69);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations/69/archive"));
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteArchive(null, 69));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteArchive("", 69));
            }
        }

        public class TheUnlockRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                client.UnlockRepository("fake", 69, "repo");

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/migrations/69/repos/repo/lock"),
                    Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullAndNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MigrationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UnlockRepository(null, 69, "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UnlockRepository("", 69, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UnlockRepository("fake", 69, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UnlockRepository("fake", 69, ""));
            }
        }
    }
}
