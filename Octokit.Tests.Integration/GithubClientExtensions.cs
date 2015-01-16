using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration
{
    public static class GithubClientExtensions
    {
        public static DisposableRepository2 CreateTestRepository(this IGitHubClient client, NewRepository newRepository)
        {
            return ((TestRepositoriesClient)client.Repository).CreateTestRepository(newRepository);
        }
    }

    public class DisposableRepository2 : Repository, IDisposable
    {
        public static DisposableRepository2 InitFromRepository(Repository repository)
        {
            DisposableRepository2 result = new DisposableRepository2();
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

        public DisposableRepository2 CreateTestRepository(NewRepository newRepo)
        {
            var repoTask = base.Create(newRepo);
            repoTask.Wait();
            var repo = repoTask.Result;
            return DisposableRepository2.InitFromRepository(repo);
        }
    }
}
