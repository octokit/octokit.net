using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class RepositoryPagesClientTests
{
    public class TheGetAllMethod
    {
        readonly IRepositoryPagesClient _repositoryPagesClient;
        const string owner = "octokit";
        const string name = "octokit.net";

        public TheGetAllMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _repositoryPagesClient = github.Repository.Page;
        }

        [IntegrationTest]
        public async Task ReturnsPages()
        {
            var pages = await _repositoryPagesClient.GetAll(owner, name);
            Assert.NotEmpty(pages);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfPagesWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var pages = await _repositoryPagesClient.GetAll(owner, name, options);
            Assert.Equal(5, pages.Count);
        }

        [IntegrationTest]
        public async Task ReturnCorrectCountOfPagesWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var pages = await _repositoryPagesClient.GetAll(owner, name, options);
            Assert.Equal(5, pages.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPage = await _repositoryPagesClient.GetAll(owner, name, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _repositoryPagesClient.GetAll(owner, name, skipStartOptions);

            Assert.NotEqual(firstPage[0].Url, secondPage[0].Url);
            Assert.NotEqual(firstPage[1].Url, secondPage[1].Url);
            Assert.NotEqual(firstPage[2].Url, secondPage[2].Url);
            Assert.NotEqual(firstPage[3].Url, secondPage[3].Url);
            Assert.NotEqual(firstPage[4].Url, secondPage[4].Url);
        }
    }
}