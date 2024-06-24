using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class GitHubAppFromManifestTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            // from https://docs.github.com/rest/apps/apps#create-a-github-app-from-a-manifest
            const string json = @"{
  ""id"": 1,
  ""slug"": ""octoapp"",
  ""node_id"": ""MDxOkludGVncmF0aW9uMQ=="",
  ""owner"": {
    ""login"": ""github"",
    ""id"": 1,
    ""node_id"": ""MDEyOk9yZ2FuaXphdGlvbjE="",
    ""url"": ""https://api.github.com/orgs/github"",
    ""repos_url"": ""https://api.github.com/orgs/github/repos"",
    ""events_url"": ""https://api.github.com/orgs/github/events"",
    ""avatar_url"": ""https://github.com/images/error/octocat_happy.gif"",
    ""gravatar_id"": """",
    ""html_url"": ""https://github.com/octocat"",
    ""followers_url"": ""https://api.github.com/users/octocat/followers"",
    ""following_url"": ""https://api.github.com/users/octocat/following{/other_user}"",
    ""gists_url"": ""https://api.github.com/users/octocat/gists{/gist_id}"",
    ""starred_url"": ""https://api.github.com/users/octocat/starred{/owner}{/repo}"",
    ""subscriptions_url"": ""https://api.github.com/users/octocat/subscriptions"",
    ""organizations_url"": ""https://api.github.com/users/octocat/orgs"",
    ""received_events_url"": ""https://api.github.com/users/octocat/received_events"",
    ""type"": ""User"",
    ""site_admin"": true
  },
  ""name"": ""Octocat App"",
  ""description"": ""A short description of the app"",
  ""external_url"": ""https://example.com"",
  ""html_url"": ""https://github.com/apps/octoapp"",
  ""created_at"": ""2017-07-08T16:18:44-04:00"",
  ""updated_at"": ""2017-07-08T16:18:44-04:00"",
  ""permissions"": {
    ""metadata"": ""read"",
    ""contents"": ""read"",
    ""issues"": ""write"",
    ""single_file"": ""write""
  },
  ""events"": [
    ""push"",
    ""pull_request""
  ],
  ""client_id"": ""Iv1.8a61f9b3a7aba766"",
  ""client_secret"": ""1726be1638095a19edd134c77bde3aa2ece1e5d8"",
  ""webhook_secret"": ""e340154128314309424b7c8e90325147d99fdafa"",
  ""pem"": ""-----BEGIN RSA PRIVATE KEY-----\nMIIEowIBAAKCAQEAuEPzOUE+kiEH1WLiMeBytTEF856j0hOVcSUSUkZxKvqczkWM\n9vo1gDyC7ZXhdH9fKh32aapba3RSsp4ke+giSmYTk2mGR538ShSDxh0OgpJmjiKP\nX0Bj4j5sFqfXuCtl9SkH4iueivv4R53ktqM+n6hk98l6hRwC39GVIblAh2lEM4L/\n6WvYwuQXPMM5OG2Ryh2tDZ1WS5RKfgq+9ksNJ5Q9UtqtqHkO+E63N5OK9sbzpUUm\noNaOl3udTlZD3A8iqwMPVxH4SxgATBPAc+bmjk6BMJ0qIzDcVGTrqrzUiywCTLma\nszdk8GjzXtPDmuBgNn+o6s02qVGpyydgEuqmTQIDAQABAoIBACL6AvkjQVVLn8kJ\ndBYznJJ4M8ECo+YEgaFwgAHODT0zRQCCgzd+Vxl4YwHmKV2Lr+y2s0drZt8GvYva\nKOK8NYYZyi15IlwFyRXmvvykF1UBpSXluYFDH7KaVroWMgRreHcIys5LqVSIb6Bo\ngDmK0yBLPp8qR29s2b7ScZRtLaqGJiX+j55rNzrZwxHkxFHyG9OG+u9IsBElcKCP\nkYCVE8ZdYexfnKOZbgn2kZB9qu0T/Mdvki8yk3I2bI6xYO24oQmhnT36qnqWoCBX\nNuCNsBQgpYZeZET8mEAUmo9d+ABmIHIvSs005agK8xRaP4+6jYgy6WwoejJRF5yd\nNBuF7aECgYEA50nZ4FiZYV0vcJDxFYeY3kYOvVuKn8OyW+2rg7JIQTremIjv8FkE\nZnwuF9ZRxgqLxUIfKKfzp/5l5LrycNoj2YKfHKnRejxRWXqG+ZETfxxlmlRns0QG\nJ4+BYL0CoanDSeA4fuyn4Bv7cy/03TDhfg/Uq0Aeg+hhcPE/vx3ebPsCgYEAy/Pv\neDLssOSdeyIxf0Brtocg6aPXIVaLdus+bXmLg77rJIFytAZmTTW8SkkSczWtucI3\nFI1I6sei/8FdPzAl62/JDdlf7Wd9K7JIotY4TzT7Tm7QU7xpfLLYIP1bOFjN81rk\n77oOD4LsXcosB/U6s1blPJMZ6AlO2EKs10UuR1cCgYBipzuJ2ADEaOz9RLWwi0AH\nPza2Sj+c2epQD9ZivD7Zo/Sid3ZwvGeGF13JyR7kLEdmAkgsHUdu1rI7mAolXMaB\n1pdrsHureeLxGbRM6za3tzMXWv1Il7FQWoPC8ZwXvMOR1VQDv4nzq7vbbA8z8c+c\n57+8tALQHOTDOgQIzwK61QKBgERGVc0EJy4Uag+VY8J4m1ZQKBluqo7TfP6DQ7O8\nM5MX73maB/7yAX8pVO39RjrhJlYACRZNMbK+v/ckEQYdJSSKmGCVe0JrGYDuPtic\nI9+IGfSorf7KHPoMmMN6bPYQ7Gjh7a++tgRFTMEc8956Hnt4xGahy9NcglNtBpVN\n6G8jAoGBAMCh028pdzJa/xeBHLLaVB2sc0Fe7993WlsPmnVE779dAz7qMscOtXJK\nfgtriltLSSD6rTA9hUAsL/X62rY0wdXuNdijjBb/qvrx7CAV6i37NK1CjABNjsfG\nZM372Ac6zc1EqSrid2IjET1YqyIW2KGLI1R2xbQc98UGlt48OdWu\n-----END RSA PRIVATE KEY-----\n""
}";

            var app = new SimpleJsonSerializer().Deserialize<GitHubAppFromManifest>(json);

            Assert.Equal(1, app.Id);
            Assert.Equal("octoapp", app.Slug);
            Assert.Equal("MDxOkludGVncmF0aW9uMQ==", app.NodeId);
            Assert.Equal("Octocat App", app.Name);
            Assert.Equal("A short description of the app", app.Description);
            Assert.Equal("https://example.com", app.ExternalUrl);
            Assert.Equal("https://github.com/apps/octoapp", app.HtmlUrl);
            Assert.Equal(InstallationReadWritePermissionLevel.Write, app.Permissions.SingleFile);
            Assert.Contains("push", app.Events);

            Assert.Equal("Iv1.8a61f9b3a7aba766", app.ClientId);
            Assert.Equal("1726be1638095a19edd134c77bde3aa2ece1e5d8", app.ClientSecret);
            Assert.Equal("e340154128314309424b7c8e90325147d99fdafa", app.WebhookSecret);
            Assert.Equal("-----BEGIN RSA PRIVATE KEY-----\nMIIEowIBAAKCAQEAuEPzOUE+kiEH1WLiMeBytTEF856j0hOVcSUSUkZxKvqczkWM\n9vo1gDyC7ZXhdH9fKh32aapba3RSsp4ke+giSmYTk2mGR538ShSDxh0OgpJmjiKP\nX0Bj4j5sFqfXuCtl9SkH4iueivv4R53ktqM+n6hk98l6hRwC39GVIblAh2lEM4L/\n6WvYwuQXPMM5OG2Ryh2tDZ1WS5RKfgq+9ksNJ5Q9UtqtqHkO+E63N5OK9sbzpUUm\noNaOl3udTlZD3A8iqwMPVxH4SxgATBPAc+bmjk6BMJ0qIzDcVGTrqrzUiywCTLma\nszdk8GjzXtPDmuBgNn+o6s02qVGpyydgEuqmTQIDAQABAoIBACL6AvkjQVVLn8kJ\ndBYznJJ4M8ECo+YEgaFwgAHODT0zRQCCgzd+Vxl4YwHmKV2Lr+y2s0drZt8GvYva\nKOK8NYYZyi15IlwFyRXmvvykF1UBpSXluYFDH7KaVroWMgRreHcIys5LqVSIb6Bo\ngDmK0yBLPp8qR29s2b7ScZRtLaqGJiX+j55rNzrZwxHkxFHyG9OG+u9IsBElcKCP\nkYCVE8ZdYexfnKOZbgn2kZB9qu0T/Mdvki8yk3I2bI6xYO24oQmhnT36qnqWoCBX\nNuCNsBQgpYZeZET8mEAUmo9d+ABmIHIvSs005agK8xRaP4+6jYgy6WwoejJRF5yd\nNBuF7aECgYEA50nZ4FiZYV0vcJDxFYeY3kYOvVuKn8OyW+2rg7JIQTremIjv8FkE\nZnwuF9ZRxgqLxUIfKKfzp/5l5LrycNoj2YKfHKnRejxRWXqG+ZETfxxlmlRns0QG\nJ4+BYL0CoanDSeA4fuyn4Bv7cy/03TDhfg/Uq0Aeg+hhcPE/vx3ebPsCgYEAy/Pv\neDLssOSdeyIxf0Brtocg6aPXIVaLdus+bXmLg77rJIFytAZmTTW8SkkSczWtucI3\nFI1I6sei/8FdPzAl62/JDdlf7Wd9K7JIotY4TzT7Tm7QU7xpfLLYIP1bOFjN81rk\n77oOD4LsXcosB/U6s1blPJMZ6AlO2EKs10UuR1cCgYBipzuJ2ADEaOz9RLWwi0AH\nPza2Sj+c2epQD9ZivD7Zo/Sid3ZwvGeGF13JyR7kLEdmAkgsHUdu1rI7mAolXMaB\n1pdrsHureeLxGbRM6za3tzMXWv1Il7FQWoPC8ZwXvMOR1VQDv4nzq7vbbA8z8c+c\n57+8tALQHOTDOgQIzwK61QKBgERGVc0EJy4Uag+VY8J4m1ZQKBluqo7TfP6DQ7O8\nM5MX73maB/7yAX8pVO39RjrhJlYACRZNMbK+v/ckEQYdJSSKmGCVe0JrGYDuPtic\nI9+IGfSorf7KHPoMmMN6bPYQ7Gjh7a++tgRFTMEc8956Hnt4xGahy9NcglNtBpVN\n6G8jAoGBAMCh028pdzJa/xeBHLLaVB2sc0Fe7993WlsPmnVE779dAz7qMscOtXJK\nfgtriltLSSD6rTA9hUAsL/X62rY0wdXuNdijjBb/qvrx7CAV6i37NK1CjABNjsfG\nZM372Ac6zc1EqSrid2IjET1YqyIW2KGLI1R2xbQc98UGlt48OdWu\n-----END RSA PRIVATE KEY-----\n", app.Pem);

            Assert.Equal(1, app.Owner.Id);
            Assert.Equal("github", app.Owner.Login);
            Assert.Equal("https://api.github.com/orgs/github", app.Owner.Url);
            Assert.Equal(AccountType.User, app.Owner.Type);
        }
    }
}
