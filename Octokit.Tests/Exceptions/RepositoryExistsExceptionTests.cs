using System;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class RepositoryExistsExceptionTests
    {
        [Fact]
        public void WhenOrganizationIsNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryExistsException(
                                                        null,
                                                        "some-repo",
                                                        GitHubClient.GitHubDotComUrl,
                                                        new ApiValidationException()));
        }

        public class TheMessageProperty
        {
            [Fact]
            public void WhenOwnerIsAccountDoNotMentionOwnerNameInMessage()
            {
                var exception = new RepositoryExistsException(
                    "some-repo",
                    new ApiValidationException());

                Assert.Equal("There is already a repository named 'some-repo' for the current account.", exception.Message);
            }

            [Fact]
            public void WhenOwnerIsOrganizationMentionInMessage()
            {
                var exception = new RepositoryExistsException(
                    "some-org",
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Equal("There is already a repository named 'some-repo' in the organization 'some-org'.", exception.Message);
            }
        }

        public class TheOwnerIsOrganizationProperty
        {
            [Fact]
            public void WhenOwnerIsAccountReturnsFalse()
            {
                var exception = new RepositoryExistsException(
                    "some-repo",
                    new ApiValidationException());

                Assert.False(exception.OwnerIsOrganization);
            }

            [Fact]
            public void WhenOwnerIsOrganizationReturnsTrue()
            {
                var exception = new RepositoryExistsException(
                    "some-org",
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.True(exception.OwnerIsOrganization);
            }
        }

        public class TheExistingRepositoryWebUrlProperty
        {
            [Fact]
            public void WhenOwnerIsAccountDoNotSetUrl()
            {
                var exception = new RepositoryExistsException(
                    "some-repo",
                    new ApiValidationException());

                Assert.Null(exception.ExistingRepositoryWebUrl);
            }

            [Fact]
            public void WhenOwnerIsOrganizationSetUrl()
            {
                var exception = new RepositoryExistsException(
                    "some-org",
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Equal(new Uri("https://github.com/some-org/some-repo"), exception.ExistingRepositoryWebUrl);
            }
        }
    }
}
