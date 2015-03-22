using System;

namespace Octokit.Tests.Integration.fixtures
{
    public class RepositoriesHooksFixture : IDisposable
    {
        readonly IGitHubClient _github;
        readonly Repository _repository;
        readonly RepositoryHook _hook;

        public RepositoriesHooksFixture()
        {
            _github = Helper.GetAuthenticatedClient();
            _repository = CreateRepository(_github);
            _hook = CreateHook(_github, _repository);
        }

        public string RepositoryOwner { get { return _repository.Owner.Login; } }

        public string RepositoryName { get { return _repository.Name; } }

        public RepositoryHook ExpectedHook { get { return _hook; } }

        public void Dispose()
        {
            _github.Repository.Delete(_repository.Owner.Login, _repository.Name);
        }

        static Repository CreateRepository(IGitHubClient github)
        {
            var repoName = Helper.MakeNameWithTimestamp("create-hooks-test");
            var repository = github.Repository.Create(new NewRepository(repoName) { AutoInit = true });

            return repository.Result;
        }

        static RepositoryHook CreateHook(IGitHubClient github, Repository repository)
        {
            var parameters = new NewRepositoryHook("tenxer", new { content_type = "json", url = "http://test.com/example" })
            {
                Events = new[] { "delete" },
                Active = false
            };
            var createdHook = github.Repository.Hooks.Create(Helper.Credentials.Login, repository.Name, parameters);

            return createdHook.Result;
        }
    }
}
