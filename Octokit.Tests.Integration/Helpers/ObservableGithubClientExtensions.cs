using Octokit.Reactive;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Octokit.Tests.Integration.Helpers
{
    internal static class ObservableGithubClientExtensions
    {
        internal static async Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, string repositoryName)
        {
            var repoName = Helper.MakeNameWithTimestamp(repositoryName);
            var repo = await client.Repository.Create(new NewRepository(repoName) { AutoInit = true });

            return new RepositoryContext(client.Connection, repo);
        }

        internal static async Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, string organizationLogin, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(organizationLogin, newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        internal static async Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        internal static async Task<TeamContext> CreateEnterpriseTeamContext(this IObservableGitHubClient client, string organization, NewTeam newTeam)
        {
            var team = await client.Organization.Team.Create(organization, newTeam);

            return new TeamContext(client.Connection, team);
        }

        internal static async Task<EnterpriseUserContext> CreateEnterpriseUserContext(this IObservableGitHubClient client, NewUser newUser)
        {
            var user = await client.User.Administration.Create(newUser);

            return new EnterpriseUserContext(client.Connection, user);
        }

        internal static async Task<PublicKeyContext> CreatePublicKeyContext(this IObservableGitHubClient client)
        {
            // Create a key
            string keyTitle = "title";
            string keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ==";

            var key = await client.User.GitSshKey.Create(new NewPublicKey(keyTitle, keyData));

            return new PublicKeyContext(client.Connection, key);
        }

        internal static async Task<GpgKeyContext> CreateGpgKeyContext(this IObservableGitHubClient client)
        {
            // Create a key
            string publicKey = @"
-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: BCPG C# v1.6.1.0

mQENBFdTvCUBCADOaVtPoJTQOgMIVYEpI8uT60LA/kDqw/1OKn7ftKjAtxNVSgjQ
i/ZqZp8XKjTg2u6l4c/aPjER2BGTg90xCcbmpwq/kkQuHR4DK7dOlEOoTzDDESEF
v6XXlXGCnxN8AD8YNvSO+Sy4+35ihuKUBAHDxmOl7ZAMH0STo10KuW82/DhfT3cC
JNqmID7H+CW1H6IhwutPKt8XsVq2FQg2RMx+uX1KqkuBAd7b30KJ93SJqzgq5CC3
DticaC0/WdZnxmYD01UvMAS6o/REs+SICdsyTxgBx/X8SIXuX2TD9PG/O2785JI5
/xvBd4jU6bBH/4oWoHr3e/lyNqb1+GSeTFX3ABEBAAG0AIkBHAQQAQIABgUCV1O8
JQAKCRDohALS4SeiOs/QB/9PMeFNdPkB1xfBm3qvTErqvhTcQspPibucYefG6JHL
vhm6iCOVBeCuPS4P/T8RTzb0qJaTdZZWcwZ9UjRVqF/RKwdMJTBKBHRegc5hRjLH
Koxk0zXosk+CapIR6eVhHe4IzpVVxZOvunsFOclIh+qHx9UzJhz9wSO/XBn/6Rzr
DGzE9fpK1JRKC0I3PuiDCNuZvojXeUsT/zuHYsz00wnA2Em7CmcWWng3nPUSHvBB
GUJ7YE7NvYXqT09PdhoZnf9p1wOugiuG6CLzWf8stlNV3mZptpP+sCGcz/UVffRO
VO/+BCBsaoT4g1FFOmJhbBAD3G72yslBnUJmqKP/39pi
=O7Yi
-----END PGP PUBLIC KEY BLOCK-----
";

            var key = await client.User.GpgKey.Create(new NewGpgKey(publicKey));

            return new GpgKeyContext(client.Connection, key);
        }
    }
}