using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Http
{
    public class ApiInfoTests
    {
        public class TheMethods
        {
            [Fact]
            public void CanClone()
            {
                var original = new ApiInfo(
                                new Dictionary<string, Uri>
                                {
                                    {
                                        "next",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5")
                                    },
                                    {
                                        "last",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5")
                                    },
                                    {
                                        "first",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5")
                                    },
                                    {
                                        "prev",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5")
                                    }
                                },
                                new List<string>
                                {
                                    "user",
                                },
                                new List<string>
                                {
                                    "user",
                                    "public_repo",
                                    "repo",
                                    "gist"
                                },
                                "5634b0b187fd2e91e3126a75006cc4fa",
                                new RateLimit(100, 75, 1372700873)
                            );

                var clone = original.Clone();

                // Note the use of Assert.NotSame tests for value types - this should continue to test should the underlying 
                // model are changed to Object types
                Assert.NotSame(original, clone);

                Assert.Equal(original.Etag, clone.Etag);
                Assert.NotSame(original.Etag, clone.Etag);

                Assert.Equal(original.AcceptedOauthScopes.Count, clone.AcceptedOauthScopes.Count);
                Assert.NotSame(original.AcceptedOauthScopes, clone.AcceptedOauthScopes);
                for (int i = 0; i < original.AcceptedOauthScopes.Count; i++)
                {
                    Assert.Equal(original.AcceptedOauthScopes[i], clone.AcceptedOauthScopes[i]);
                    Assert.NotSame(original.AcceptedOauthScopes[i], clone.AcceptedOauthScopes[i]);
                }

                Assert.Equal(original.Links.Count, clone.Links.Count);
                Assert.NotSame(original.Links, clone.Links);
                for (int i = 0; i < original.Links.Count; i++)
                {
                    Assert.Equal(original.Links.Keys.ToArray()[i], clone.Links.Keys.ToArray()[i]);
                    Assert.NotSame(original.Links.Keys.ToArray()[i], clone.Links.Keys.ToArray()[i]);
                    Assert.Equal(original.Links.Values.ToArray()[i].ToString(), clone.Links.Values.ToArray()[i].ToString());
                    Assert.NotSame(original.Links.Values.ToArray()[i], clone.Links.Values.ToArray()[i]);
                }

                Assert.Equal(original.OauthScopes.Count, clone.OauthScopes.Count);
                Assert.NotSame(original.OauthScopes, clone.OauthScopes);
                for (int i = 0; i < original.OauthScopes.Count; i++)
                {
                    Assert.Equal(original.OauthScopes[i], clone.OauthScopes[i]);
                    Assert.NotSame(original.OauthScopes[i], clone.OauthScopes[i]);
                }

                Assert.NotSame(original.RateLimit, clone.RateLimit);
                Assert.Equal(original.RateLimit.Limit, clone.RateLimit.Limit);
                Assert.NotSame(original.RateLimit.Limit, clone.RateLimit.Limit);
                Assert.Equal(original.RateLimit.Remaining, clone.RateLimit.Remaining);
                Assert.NotSame(original.RateLimit.Remaining, clone.RateLimit.Remaining);
                Assert.Equal(original.RateLimit.ResetAsUtcEpochSeconds, clone.RateLimit.ResetAsUtcEpochSeconds);
                Assert.NotSame(original.RateLimit.ResetAsUtcEpochSeconds, clone.RateLimit.ResetAsUtcEpochSeconds);
                Assert.Equal(original.RateLimit.Reset, clone.RateLimit.Reset);
                Assert.NotSame(original.RateLimit.Reset, clone.RateLimit.Reset);
            }
        }
    }
}
