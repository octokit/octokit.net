using System;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class RepositoryExistsExceptionTests
    {
        public class TheMessageProperty
        {
            [Fact]
            public void WhenOwnerIsNullDoNotMentionInMessage()
            {
                var exception = new RepositoryExistsException(
                    null,
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Equal("There is already a repository named 'some-repo' for the current account.", exception.Message);
            }

            [Fact]
            public void WhenOwnerIsNotNullMentionInMessage()
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
            public void WhenOwnerIsNullReturnsFalse()
            {
                var exception = new RepositoryExistsException(
                    null,
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.False(exception.OwnerIsOrganization);
            }

            [Fact]
            public void WhenOwnerIsNotNullReturnsTrue()
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
            public void WhenOwnerIsNullDoNotSetUrl()
            {
                var exception = new RepositoryExistsException(
                    null,
                    "some-repo",
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Null(exception.ExistingRepositoryWebUrl);
            }

            [Fact]
            public void WhenOwnerIsNotNullSetUrl()
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
