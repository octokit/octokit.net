using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration
{
    public static class GithubClientExtensions
    {
        public async static Task<DisposableRepository> CreateDisposableRepository(this IGitHubClient client, NewRepository newRepository)
        {
            return await ((TestRepositoriesClient)client.Repository).CreateDisposableRepository(newRepository);
        }
    }

    public class DisposableRepository : Repository, IDisposable
    {
        public static DisposableRepository InitFromRepository(Repository repository)
        {
            DisposableRepository result = new DisposableRepository();
            foreach (System.Reflection.PropertyInfo prop in repository.GetType().GetProperties())
            {
                var value = prop.GetValue(repository);
                prop.SetValue(result, value);
            }
            return result;
        }

        public void Dispose()
        {
            Helper.DeleteRepo(this as Repository);
        }
    }

    public class TestGithubClient : GitHubClient
    {
        public TestGithubClient()
            : base(new ProductHeaderValue("OctokitTests"))
        {
            base.Repository = new TestRepositoriesClient(new ApiConnection(Connection));
        }
    }

    public class TestRepositoriesClient : RepositoriesClient
    {
        public TestRepositoriesClient(IApiConnection apiConnection) : base (apiConnection)
        {

        }

        public async Task<DisposableRepository> CreateDisposableRepository(NewRepository newRepo)
        {
            var result = await base.Create(newRepo);
            return DisposableRepository.InitFromRepository(result);
        }
    }
}
