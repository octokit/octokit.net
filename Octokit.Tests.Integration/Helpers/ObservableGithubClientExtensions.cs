using Octokit.Reactive;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Octokit.Tests.Integration.Helpers
{
    internal static class ObservableGithubClientExtensions
    {
        internal async static Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, string repositoryName)
        {
            var repoName = Helper.MakeNameWithTimestamp(repositoryName);
            var repo = await client.Repository.Create(new NewRepository(repoName) { AutoInit = true });

            return new RepositoryContext(repo);
        }

        internal async static Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, string organizationLogin, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(organizationLogin, newRepository);

            return new RepositoryContext(repo);
        }

        internal async static Task<RepositoryContext> CreateRepositoryContext(this IObservableGitHubClient client, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(repo);
        }

        internal async static Task<EnterpriseTeamContext> CreateEnterpriseTeamContext(this IObservableGitHubClient client, string organization, NewTeam newTeam)
        {
            var team = await client.Organization.Team.Create(organization, newTeam);

            return new EnterpriseTeamContext(team);
        }

        internal async static Task<EnterpriseUserContext> CreateEnterpriseUserContext(this IObservableGitHubClient client, NewUser newUser)
        {
            var user = await client.User.Administration.Create(newUser);

            return new EnterpriseUserContext(user);
        }
    }
}