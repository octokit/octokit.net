using Xunit;
using System;

namespace Octokit.Tests.Models
{
    public class RepositoryTransferTest
    {
        public static readonly string emptyName = "";
        public static readonly string nonemptyName = "name";
        public static readonly long[] emptyTeamId = new long[] { };
        public static readonly long[] nonemptyTeamId = new long[] { 1, 2, 3 };

        public class TheSingleArgumentConstructor
        {
            [Fact]
            public void ChecksForEmptyName()
            {
                Assert.Throws<ArgumentException>(() => { new RepositoryTransfer(emptyName); });
            }

            [Fact]
            public void ChecksForNullName()
            {
                Assert.Throws<ArgumentNullException>(() => { new RepositoryTransfer(null); });
            }

            [Fact]
            public void StoresGivenName()
            {
                string testName = nonemptyName;
                RepositoryTransfer repositoryTransfer = new RepositoryTransfer(testName);
                Assert.Equal(repositoryTransfer.NewOwner, testName);
            }

            [Fact]
            public void SetsTeamIdToNull()
            {
                RepositoryTransfer repositoryTransfer = new RepositoryTransfer(nonemptyName);
                Assert.Null(repositoryTransfer.TeamIds);
            }
        }

        public class TheFullConstructor
        {
            [Fact]
            public void ChecksForEmptyName()
            {
                Assert.Throws<ArgumentException>(() => { new RepositoryTransfer(emptyName, nonemptyTeamId); });
            }

            [Fact]
            public void ChecksForNullName()
            {
                Assert.Throws<ArgumentNullException>(() => { new RepositoryTransfer(null, nonemptyTeamId); });
            }

            [Fact]
            public void ChecksForEmptyTeamId()
            {
                Assert.Throws<ArgumentException>(() => { new RepositoryTransfer(nonemptyName, emptyTeamId); });
            }

            [Fact]
            public void ChecksForNullTeamId()
            {
                Assert.Throws<ArgumentNullException>(() => { new RepositoryTransfer(nonemptyName, null); });
            }

            [Fact]
            public void StoresGivenName()
            {
                string testName = nonemptyName;
                RepositoryTransfer repositoryTransfer = new RepositoryTransfer(testName, nonemptyTeamId);
                Assert.Equal(repositoryTransfer.NewOwner, testName);
            }

            [Fact]
            public void StoresGivenTeamId()
            {
                long[] testTeamId = nonemptyTeamId;
                RepositoryTransfer repositoryTransfer = new RepositoryTransfer(nonemptyName, testTeamId);
                Assert.Equal(repositoryTransfer.TeamIds, testTeamId);
            }
        }
    }
}
