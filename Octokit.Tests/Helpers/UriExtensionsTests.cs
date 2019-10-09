using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public class UriExtensionsTests
    {
        public class TheReplaceRelativeUriMethod
        {
            [Theory]
            [InlineData("https://api.github.com", "my/new/path", "https://api.github.com/my/new/path")]
            [InlineData("https://api.github.com", "/my/new/path", "https://api.github.com/my/new/path")]
            [InlineData("https://example.com/api/v3", "my/new/path", "https://example.com/my/new/path")]
            [InlineData("https://example.com/api/v3", "/my/new/path", "https://example.com/my/new/path")]
            public void ReplacesRelativeUrisCorrectly(string baseUri, string relativeUri, string expectedUri)
            {
                var uri = new Uri(baseUri);

                var newUri = uri.ReplaceRelativeUri(relativeUri.FormatUri());

                Assert.Equal(newUri.ToString(), expectedUri);
            }
        }

        public class TheApplyParametersMethod
        {
            [Fact]
            public void AppendsParametersAsQueryString()
            {
                var uri = new Uri("https://example.com");

                var uriWithParameters = uri.ApplyParameters(new Dictionary<string, string>
                {
                    {"foo", "foo val"},
                    {"bar", "barval"}
                });

                Assert.Equal(new Uri("https://example.com?foo=foo%20val&bar=barval"), uriWithParameters);
            }

            [Fact]
            public void AppendsParametersAsQueryStringWithRelativeUri()
            {
                var uri = new Uri("issues", UriKind.Relative);

                var uriWithParameters = uri.ApplyParameters(new Dictionary<string, string>
                {
                    {"foo", "fooval"},
                    {"bar", "barval"}
                });

                Assert.Equal(new Uri("issues?foo=fooval&bar=barval", UriKind.Relative), uriWithParameters);
            }

            [Fact]
            public void ThrowsExceptionWhenNullValueProvided()
            {
                var uri = new Uri("https://example.com");

                var parameters = new Dictionary<string, string>
                {
                    {"foo", null }
                };

                Assert.Throws<ArgumentNullException>(() => uri.ApplyParameters(parameters));
            }

            [Fact]
            public void ThrowsExceptionWhenNullValueProvidedWithRelativeUri()
            {
                var uri = new Uri("api/example", UriKind.Relative);

                var parameters = new Dictionary<string, string>
                {
                    {"foo", null }
                };

                Assert.Throws<ArgumentNullException>(() => uri.ApplyParameters(parameters));
            }

            [Fact]
            public void DoesNotChangeUrlWhenParametersEmpty()
            {
                var uri = new Uri("https://example.com");

                var uriWithEmptyParameters = uri.ApplyParameters(new Dictionary<string, string>());
                var uriWithNullParameters = uri.ApplyParameters(null);

                Assert.Equal(uri, uriWithEmptyParameters);
                Assert.Equal(uri, uriWithNullParameters);
            }

            [Fact]
            public void DoesNotChangeUrlWhenParametersEmptyWithRelativeUri()
            {
                var uri = new Uri("api/example", UriKind.Relative);

                var uriWithEmptyParameters = uri.ApplyParameters(new Dictionary<string, string>());
                var uriWithNullParameters = uri.ApplyParameters(null);

                Assert.Equal(uri, uriWithEmptyParameters);
                Assert.Equal(uri, uriWithNullParameters);
            }

            [Fact]
            public void CombinesExistingParametersWithNewParameters()
            {
                var uri = new Uri("https://api.github.com/repositories/1/milestones?state=closed&sort=due_date&direction=asc&page=2");

                var parameters = new Dictionary<string, string> { { "state", "open" }, { "sort", "other" }, { "per_page", "5" } };

                var actual = uri.ApplyParameters(parameters);

                Assert.Equal(
                    new Uri("https://api.github.com/repositories/1/milestones?state=open&sort=other&per_page=5&direction=asc&page=2"),
                    actual);
            }

            [Fact]
            public void CombinesExistingParametersWithNewParametersToRelativeUri()
            {
                var uri = new Uri("repositories/1/milestones?state=closed&sort=due_date&direction=asc&page=2", UriKind.Relative);

                var parameters = new Dictionary<string, string> { { "state", "open" }, { "sort", "other" }, { "per_page", "5" } };

                var actual = uri.ApplyParameters(parameters);

                Assert.Equal(
                    new Uri("repositories/1/milestones?state=open&sort=other&per_page=5&direction=asc&page=2", UriKind.Relative),
                    actual);
            }

            [Fact]
            public void DoesNotChangePassedInDictionary()
            {
                var uri = new Uri("https://api.github.com/repositories/1/milestones?state=closed&sort=due_date&direction=asc&page=2");

                var parameters = new Dictionary<string, string> { { "state", "open" }, { "sort", "other" } };

                uri.ApplyParameters(parameters);

                Assert.Equal(2, parameters.Count);
            }

            [Fact]
            public void DoesNotChangePassedInDictionaryForRelativeUri()
            {
                var uri = new Uri("/repositories/1/milestones?state=closed&sort=due_date&direction=asc&page=2", UriKind.Relative);

                var parameters = new Dictionary<string, string> { { "state", "open" }, { "sort", "other" } };

                uri.ApplyParameters(parameters);

                Assert.Equal(2, parameters.Count);
            }

            [Fact]
            public void EnsuresArgumentNotNull()
            {
                Uri uri = null;
                Assert.Throws<ArgumentNullException>(() => uri.ApplyParameters(new Dictionary<string, string>()));
            }
        }
    }
}
