using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    public static class GithubClientExtensions
    {
        public async static Task<DisposableRepository> CreateDisposableRepository(this IGitHubClient client, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(newRepository);
            return DisposableRepository.InitFromRepository(repo);
        }

        public async static Task<DisposableRepository> CreateDisposableRepository(this IGitHubClient client, string organizationLogin, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(organizationLogin, newRepository);
            return DisposableRepository.InitFromRepository(repo);
        }
    }
}
