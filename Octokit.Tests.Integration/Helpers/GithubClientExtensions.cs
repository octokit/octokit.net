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
    }
}