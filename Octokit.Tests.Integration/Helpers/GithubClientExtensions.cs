using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal static class GithubClientExtensions
    {
        internal async static Task<RepositoryContext> CreateRepositoryContext(this IGitHubClient client, string repositoryName)
        {
            var repoName = Helper.MakeNameWithTimestamp(repositoryName);
            var repo = await client.Repository.Create(new NewRepository(repoName) { AutoInit = true });

            return new RepositoryContext(repo);
        }

        internal async static Task<RepositoryContext> CreateRepositoryContext(this IGitHubClient client, string organizationLogin, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(organizationLogin, newRepository);

            return new RepositoryContext(repo);
        }

        internal async static Task<RepositoryContext> CreateRepositoryContext(this IGitHubClient client, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(repo);
        }

        internal async static Task<EnterpriseTeamContext> CreateEnterpriseTeamContext(this IGitHubClient client, string organization, NewTeam newTeam)
        {
            var team = await client.Organization.Team.Create(organization, newTeam);

            return new EnterpriseTeamContext(team);
        }

        internal async static Task<EnterpriseUserContext> CreateEnterpriseUserContext(this IGitHubClient client, NewUser newUser)
        {
            var user = await client.User.Administration.Create(newUser);

            return new EnterpriseUserContext(user);
        }

        internal async static Task<PublicKeyContext> CreatePublicKeyContext(this IGitHubClient client)
        {
            // Create a key
            string keyTitle = "title";
            string keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ==";

            var key = await client.User.Keys.Create(new NewPublicKey(keyTitle, keyData));

            return new PublicKeyContext(key);
        }
    }
}