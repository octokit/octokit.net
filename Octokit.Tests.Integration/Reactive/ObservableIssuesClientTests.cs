using Octokit.Reactive;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableIssuesClientTests : IDisposable
    {
        readonly ObservableIssuesClient client;
        readonly string repoName;
        readonly Repository createdRepository;

        public ObservableIssuesClientTests()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            client = new ObservableIssuesClient(github);
            repoName = Helper.MakeNameWithTimestamp("public-repo");
            var result = github.Repository.Create(new NewRepository { Name = repoName }).Result;
            createdRepository = result;
        }

        [IntegrationTest]
        public async Task ReturnsSpecifiedIssue()
        {
            var observable = client.Get("libgit2", "libgit2sharp", 1);
            var issue = await observable;

            Assert.Equal(1, issue.Number);
            Assert.Equal("Change License ", issue.Title);
        }

        [IntegrationTest]
        public void ReturnsAllIssuesForARepository()
        {
            var issues = client.GetForRepository("libgit2", "libgit2sharp").ToList().Wait();

            Assert.NotEmpty(issues);
        }

        [IntegrationTest]
        public async void ReturnsAllIssuesForCurrentUser()
        {
            var newIssue = new NewIssue("Integration test issue");
            var createResult = await client.Create(createdRepository.Owner.Name, repoName, newIssue);
            
            var issues = client.GetAllForCurrent().ToList().Wait();

            Assert.NotEmpty(issues);
        }

        [IntegrationTest]
        public async void ReturnsAllIssuesForOwnedAndMemberRepositories()
        {
            var newIssue = new NewIssue("Integration test issue");
            var createResult = await client.Create(createdRepository.Owner.Name, repoName, newIssue);
            var result = client.GetAllForOwnedAndMemberRepositories().ToList().Wait();

            Assert.NotEmpty(result);
        }

        [IntegrationTest]
        public async void CanCreateAndUpdateIssues()
        {
            var newIssue = new NewIssue("Integration test issue");

            var createResult = await client.Create(createdRepository.Owner.Name, repoName, newIssue);
            var updateResult = await client.Update(createdRepository.Owner.Name, repoName, createResult.Number, new IssueUpdate { Title = "Modified integration test issue" });

            Assert.Equal("Modified integration test issue", updateResult.Title);
        }

        public void Dispose()
        {
            Helper.DeleteRepo(createdRepository);
        }
    }
}
