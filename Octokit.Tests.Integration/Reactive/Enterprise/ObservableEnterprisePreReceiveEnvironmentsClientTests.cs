using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableEnterprisePreReceiveEnvironmentsClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new EnterprisePreReceiveEnvironmentsClient(null));
        }
    }

    public class TheGetAllMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;
        private readonly List<PreReceiveEnvironment> _preReceiveEnvironments;

        public TheGetAllMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;

            _preReceiveEnvironments = new List<PreReceiveEnvironment>();
            for (var count = 0; count < 5; count++)
            {
                var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");
                _preReceiveEnvironments.Add(_preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment).Wait());
            }
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsPreReceiveEnvironments()
        {
            var preReceiveEnvironments = await _preReceiveEnvironmentsClient.GetAll().ToList();

            Assert.NotEmpty(preReceiveEnvironments);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsCorrectCountOfPreReceiveEnvironmentsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var preReceiveEnvironments = await _preReceiveEnvironmentsClient.GetAll(options).ToList();

            Assert.Single(preReceiveEnvironments);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsCorrectCountOfPreReceiveEnvironmentsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var preReceiveEnvironments = await _preReceiveEnvironmentsClient.GetAll(options).ToList();

            Assert.Single(preReceiveEnvironments);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _preReceiveEnvironmentsClient.GetAll(startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _preReceiveEnvironmentsClient.GetAll(skipStartOptions).ToList();

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        public void Dispose()
        {
            foreach (var preReceiveEnvironment in _preReceiveEnvironments)
            {
                EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, preReceiveEnvironment);
            }
        }
    }

    public class TheGetMethod : IDisposable
    {
        private readonly string _preReceiveEnvironmentName;
        private readonly string _preReceiveEnvironmentUrl;
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;
        private readonly PreReceiveEnvironment _preReceiveEnvironment;

        public TheGetMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;

            _preReceiveEnvironmentName = Helper.MakeNameWithTimestamp("pre-receive");
            _preReceiveEnvironmentUrl = "https://example.com/foo.zip";
            var newPreReceiveEnvironment = new NewPreReceiveEnvironment(_preReceiveEnvironmentName, _preReceiveEnvironmentUrl);
            _preReceiveEnvironment = _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment).Wait();
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsName()
        {
            var preReceiveEnvironment = await _preReceiveEnvironmentsClient.Get(_preReceiveEnvironment.Id);

            Assert.NotNull(preReceiveEnvironment);
            Assert.Equal(_preReceiveEnvironmentName, preReceiveEnvironment.Name);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsImageUrl()
        {
            var preReceiveEnvironment = await _preReceiveEnvironmentsClient.Get(_preReceiveEnvironment.Id);

            Assert.NotNull(preReceiveEnvironment);
            Assert.Equal(_preReceiveEnvironmentUrl, preReceiveEnvironment.ImageUrl);
        }

        [GitHubEnterpriseTest]
        public async Task NoEnvironmentExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveEnvironmentsClient.Get(-1));
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, _preReceiveEnvironment);
        }
    }

    public class TheCreateMethod
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;

        public TheCreateMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;
        }

        [GitHubEnterpriseTest]
        public async Task CanCreatePreReceiveEnvironment()
        {
            PreReceiveEnvironment preReceiveEnvironment = null;
            try
            {
                var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");

                preReceiveEnvironment = await _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment);

                Assert.NotNull(preReceiveEnvironment);
                Assert.Equal(newPreReceiveEnvironment.Name, preReceiveEnvironment.Name);
                Assert.Equal(newPreReceiveEnvironment.ImageUrl, preReceiveEnvironment.ImageUrl);
            }
            finally
            {
                //Cleanup
                EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, preReceiveEnvironment);
            }
        }

        [GitHubEnterpriseTest]
        public async Task CannotCreateWithSameName()
        {
            var newPreReceiveEnvironment = new NewPreReceiveEnvironment("default", "https://example.com/foo.zip");

            await Assert.ThrowsAsync<ApiValidationException>(async () => await _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment));
        }
    }

    public class TheEditMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;
        private readonly PreReceiveEnvironment _preReceiveEnvironment;

        public TheEditMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;

            var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");
            _preReceiveEnvironment = _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment).Wait();
            EnterpriseHelper.WaitForPreReceiveEnvironmentToComplete(_githubEnterprise.Connection, _preReceiveEnvironment);
        }

        [GitHubEnterpriseTest]
        public async Task CanChangeNameOfPreReceiveEnvironment()
        {
            var updatePreReceiveEnvironment = new UpdatePreReceiveEnvironment
            {
                Name = Helper.MakeNameWithTimestamp("pre-receive")
            };

            var updatedPreReceiveEnvironment = await _preReceiveEnvironmentsClient.Edit(_preReceiveEnvironment.Id, updatePreReceiveEnvironment);

            Assert.Equal(_preReceiveEnvironment.Id, updatedPreReceiveEnvironment.Id);
            Assert.Equal(updatePreReceiveEnvironment.Name, updatedPreReceiveEnvironment.Name);
            Assert.Equal(_preReceiveEnvironment.ImageUrl, updatedPreReceiveEnvironment.ImageUrl);
        }

        [GitHubEnterpriseTest]
        public async Task CanChangeImageUrlOfPreReceiveEnvironment()
        {
            var updatePreReceiveEnvironment = new UpdatePreReceiveEnvironment
            {
                ImageUrl = "https://example.com/bar.zip"
            };

            var updatedPreReceiveEnvironment = await _preReceiveEnvironmentsClient.Edit(_preReceiveEnvironment.Id, updatePreReceiveEnvironment);

            Assert.Equal(_preReceiveEnvironment.Id, updatedPreReceiveEnvironment.Id);
            Assert.Equal(updatePreReceiveEnvironment.ImageUrl, updatedPreReceiveEnvironment.ImageUrl);
            Assert.Equal(_preReceiveEnvironment.Name, updatedPreReceiveEnvironment.Name);
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, _preReceiveEnvironment);
        }
    }

    public class TheDeleteMethod
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;

        public TheDeleteMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;
        }

        [GitHubEnterpriseTest]
        public async Task CanDeletePreReceiveEnvironment()
        {
            var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");
            var preReceiveEnvironment = await _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment);
            EnterpriseHelper.WaitForPreReceiveEnvironmentToComplete(_githubEnterprise.Connection, preReceiveEnvironment);

            await _preReceiveEnvironmentsClient.Delete(preReceiveEnvironment.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveEnvironmentsClient.Get(preReceiveEnvironment.Id));
        }

        [GitHubEnterpriseTest]
        public async Task CannotDeleteWhenNoEnvironmentExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveEnvironmentsClient.Delete(-1));
        }
    }

    public class TheDownloadStatusMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;
        private readonly PreReceiveEnvironment _preReceiveEnvironment;

        public TheDownloadStatusMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;

            var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");
            _preReceiveEnvironment = _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment).Wait();
        }

        [GitHubEnterpriseTest]
        public async Task CanGetDownloadStatus()
        {
            var downloadStatus = await _preReceiveEnvironmentsClient.DownloadStatus(_preReceiveEnvironment.Id);

            Assert.NotNull(downloadStatus);
        }

        [GitHubEnterpriseTest]
        public async Task CannotGetDownloadStatusWhenNoEnvironmentExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveEnvironmentsClient.DownloadStatus(-1));
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, _preReceiveEnvironment);
        }
    }

    public class TheTriggerDownloadMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveEnvironmentsClient _preReceiveEnvironmentsClient;
        private readonly PreReceiveEnvironment _preReceiveEnvironment;

        public TheTriggerDownloadMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveEnvironmentsClient = _githubEnterprise.Enterprise.PreReceiveEnvironment;

            var newPreReceiveEnvironment = new NewPreReceiveEnvironment(Helper.MakeNameWithTimestamp("pre-receive"), "https://example.com/foo.zip");
            _preReceiveEnvironment = _preReceiveEnvironmentsClient.Create(newPreReceiveEnvironment).Wait();
            EnterpriseHelper.WaitForPreReceiveEnvironmentToComplete(_githubEnterprise.Connection, _preReceiveEnvironment);
        }

        [GitHubEnterpriseTest]
        public async Task CanTriggerDownload()
        {
            var downloadStatus = await _preReceiveEnvironmentsClient.DownloadStatus(_preReceiveEnvironment.Id);

            Assert.NotNull(downloadStatus);
        }

        [GitHubEnterpriseTest]
        public async Task CannotTriggerDownloadWhenNoEnvironmentExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveEnvironmentsClient.DownloadStatus(-1));
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveEnvironment(_githubEnterprise.Connection, _preReceiveEnvironment);
        }
    }
}
