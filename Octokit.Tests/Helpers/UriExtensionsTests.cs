using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public class UriExtensionsTests
    {
        public class TheApplyParametersMethod
        {
            [Fact]
            public void AppendsParametersAsQueryString()
            {
                var uri = new Uri("https://example.com");

                var uriWithParameters = uri.ApplyParameters(new Dictionary<string, string>
                {
                    {"foo", "fooval"},
                    {"bar", "barval"}
                });

                Assert.Equal(new Uri("https://example.com?foo=fooval&bar=barval"), uriWithParameters);
            }

            [Fact]
            public void OverwritesExistingParameters()
            {
                var uri = new Uri("https://example.com?crap=crapola");

                var uriWithParameters = uri.ApplyParameters(new Dictionary<string, string>
                {
                    {"foo", "fooval"},
                    {"bar", "barval"}
                });

                Assert.Equal(new Uri("https://example.com?foo=fooval&bar=barval"), uriWithParameters);
            }

            [Fact]
            public void DoesNotChangeUrlWhenParametersIsNullOrEmpty()
            {
                var uri = new Uri("https://example.com");

                var uriWithNullParameters = uri.ApplyParameters(null);
                var uriWithEmptyParameters = uri.ApplyParameters(new Dictionary<string, string>());

                Assert.Same(uri, uriWithNullParameters);
                Assert.Equal(uri, uriWithEmptyParameters);
            }

            [Fact]
            public void EnsuresUriNotNull()
            {
                Uri uri = null;
                Assert.Throws<ArgumentNullException>(() => uri.ApplyParameters(new Dictionary<string, string>()));
            }
        }
    }
}
