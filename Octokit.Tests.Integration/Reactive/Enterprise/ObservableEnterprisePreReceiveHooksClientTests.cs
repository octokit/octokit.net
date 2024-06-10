using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableEnterprisePreReceiveHooksClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new EnterprisePreReceiveHooksClient(null));
        }
    }

    public class TheGetAllMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveHooksClient _preReceiveHooksClient;
        private readonly List<PreReceiveHook> _preReceiveHooks;

        public TheGetAllMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveHooksClient = _githubEnterprise.Enterprise.PreReceiveHook;

            _preReceiveHooks = new List<PreReceiveHook>();
            for (var count = 0; count < 3; count++)
            {
                var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
                _preReceiveHooks.Add(_preReceiveHooksClient.Create(newPreReceiveHook).Wait());
            }
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsPreReceiveHooks()
        {
            var preReceiveHooks = await _preReceiveHooksClient.GetAll().ToList();

            Assert.NotEmpty(preReceiveHooks);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsCorrectCountOfPreReceiveHooksWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var preReceiveHooks = await _preReceiveHooksClient.GetAll(options).ToList();

            Assert.Single(preReceiveHooks);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsCorrectCountOfPreReceiveHooksWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var preReceiveHooks = await _preReceiveHooksClient.GetAll(options).ToList();

            Assert.Single(preReceiveHooks);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _preReceiveHooksClient.GetAll(startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _preReceiveHooksClient.GetAll(skipStartOptions).ToList();

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        public void Dispose()
        {
            foreach (var preReceiveHook in _preReceiveHooks)
            {
                EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, preReceiveHook);
            }
        }
    }

    public class TheGetMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveHooksClient _preReceiveHooksClient;
        private readonly NewPreReceiveHook _expectedPreReceiveHook;
        private readonly PreReceiveHook _preReceiveHook;

        public TheGetMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveHooksClient = _githubEnterprise.Enterprise.PreReceiveHook;

            _expectedPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1)
            {
                AllowDownstreamConfiguration = true,
                Enforcement = PreReceiveHookEnforcement.Testing,
            };
            _preReceiveHook = _preReceiveHooksClient.Create(_expectedPreReceiveHook).Wait();
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsName()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.Equal(_expectedPreReceiveHook.Name, preReceiveHook.Name);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsScript()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.Equal(_expectedPreReceiveHook.Script, preReceiveHook.Script);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsRepository()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.NotNull(preReceiveHook.ScriptRepository);
            Assert.Equal(_expectedPreReceiveHook.ScriptRepository.FullName, preReceiveHook.ScriptRepository.FullName);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsEnvironment()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.NotNull(preReceiveHook.Environment);
            Assert.Equal(_expectedPreReceiveHook.Environment.Id, preReceiveHook.Environment.Id);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsAllowDownstreamConfiguration()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.Equal(_expectedPreReceiveHook.AllowDownstreamConfiguration, preReceiveHook.AllowDownstreamConfiguration);
        }

        [GitHubEnterpriseTest]
        public async Task ReturnsEnforcement()
        {
            var preReceiveHook = await _preReceiveHooksClient.Get(_preReceiveHook.Id);

            Assert.NotNull(preReceiveHook);
            Assert.Equal(_expectedPreReceiveHook.Enforcement.Value, preReceiveHook.Enforcement);
        }

        [GitHubEnterpriseTest]
        public async Task NoHookExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveHooksClient.Get(-1));
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, _preReceiveHook);
        }
    }

    public class TheCreateMethod
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveHooksClient _preReceiveHooksClient;

        public TheCreateMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveHooksClient = _githubEnterprise.Enterprise.PreReceiveHook;
        }

        [GitHubEnterpriseTest]
        public async Task CanCreatePreReceiveHook()
        {
            PreReceiveHook preReceiveHook = null;
            try
            {
                var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
                preReceiveHook = await _preReceiveHooksClient.Create(newPreReceiveHook);

                Assert.NotNull(preReceiveHook);
                Assert.Equal(newPreReceiveHook.Name, preReceiveHook.Name);
                Assert.Equal(newPreReceiveHook.Script, preReceiveHook.Script);
                Assert.Equal(newPreReceiveHook.ScriptRepository.FullName, preReceiveHook.ScriptRepository.FullName);
                Assert.Equal(newPreReceiveHook.Environment.Id, preReceiveHook.Environment.Id);
            }
            finally
            {
                //Cleanup
                EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, preReceiveHook);
            }
        }

        [GitHubEnterpriseTest]
        public async Task CannotCreateWhenRepoDoesNotExist()
        {
            var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "doesntExist/repo", Helper.MakeNameWithTimestamp("script"), 1);
            await Assert.ThrowsAsync<ApiValidationException>(async () => await _preReceiveHooksClient.Create(newPreReceiveHook));
        }

        [GitHubEnterpriseTest]
        public async Task CannotCreateWhenEnvironmentDoesNotExist()
        {
            var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), -1);
            await Assert.ThrowsAsync<ApiValidationException>(async () => await _preReceiveHooksClient.Create(newPreReceiveHook));
        }

        [GitHubEnterpriseTest]
        public async Task CannotCreateWithSameName()
        {
            PreReceiveHook preReceiveHook = null;
            try
            {
                var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
                preReceiveHook = await _preReceiveHooksClient.Create(newPreReceiveHook);

                newPreReceiveHook.Script = Helper.MakeNameWithTimestamp("script");
                await Assert.ThrowsAsync<ApiValidationException>(async () => await _preReceiveHooksClient.Create(newPreReceiveHook));
            }
            finally
            {
                //Cleanup
                EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, preReceiveHook);
            }
        }

        [GitHubEnterpriseTest]
        public async Task CannotCreateWithSameScript()
        {
            PreReceiveHook preReceiveHook = null;
            try
            {
                var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
                preReceiveHook = await _preReceiveHooksClient.Create(newPreReceiveHook);

                newPreReceiveHook.Name = Helper.MakeNameWithTimestamp("hook");
                await Assert.ThrowsAsync<ApiValidationException>(async () => await _preReceiveHooksClient.Create(newPreReceiveHook));
            }
            finally
            {
                //Cleanup
                EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, preReceiveHook);
            }
        }
    }

    public class TheEditMethod : IDisposable
    {
        private readonly IObservableGitHubClient _githubEnterprise;
        private readonly IObservableEnterprisePreReceiveHooksClient _preReceiveHooksClient;
        private readonly PreReceiveHook _preReceiveHook;

        public TheEditMethod()
        {
            _githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveHooksClient = _githubEnterprise.Enterprise.PreReceiveHook;

            var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
            _preReceiveHook = _preReceiveHooksClient.Create(newPreReceiveHook).Wait();
        }

        [GitHubEnterpriseTest]
        public async Task CanChangeName()
        {
            var updatePreReceiveHook = new UpdatePreReceiveHook
            {
                Name = Helper.MakeNameWithTimestamp("hook")
            };

            var updatedPreReceiveHook = await _preReceiveHooksClient.Edit(_preReceiveHook.Id, updatePreReceiveHook);

            Assert.Equal(_preReceiveHook.Id, updatedPreReceiveHook.Id);
            Assert.Equal(updatePreReceiveHook.Name, updatedPreReceiveHook.Name);
        }

        [GitHubEnterpriseTest]
        public async Task CanChangeScript()
        {
            var updatePreReceiveHook = new UpdatePreReceiveHook
            {
                Script = Helper.MakeNameWithTimestamp("script")
            };

            var updatedPreReceiveHook = await _preReceiveHooksClient.Edit(_preReceiveHook.Id, updatePreReceiveHook);

            Assert.Equal(_preReceiveHook.Id, updatedPreReceiveHook.Id);
            Assert.Equal(updatePreReceiveHook.Script, updatedPreReceiveHook.Script);
        }

        public void Dispose()
        {
            EnterpriseHelper.DeletePreReceiveHook(_githubEnterprise.Connection, _preReceiveHook);
        }
    }

    public class TheDeleteMethod
    {
        private readonly IObservableEnterprisePreReceiveHooksClient _preReceiveHooksClient;

        public TheDeleteMethod()
        {
            var githubEnterprise = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
            _preReceiveHooksClient = githubEnterprise.Enterprise.PreReceiveHook;
        }

        [GitHubEnterpriseTest]
        public async Task CanDelete()
        {
            var newPreReceiveHook = new NewPreReceiveHook(Helper.MakeNameWithTimestamp("hook"), "octokit/octokit.net", Helper.MakeNameWithTimestamp("script"), 1);
            var preReceiveHook = await _preReceiveHooksClient.Create(newPreReceiveHook);

            await _preReceiveHooksClient.Delete(preReceiveHook.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveHooksClient.Get(preReceiveHook.Id));
        }

        [GitHubEnterpriseTest]
        public async Task CannotDeleteWhenHookDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _preReceiveHooksClient.Delete(-1));
        }
    }
}
