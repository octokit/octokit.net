using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace Octokit
{
    public class RunnersClient : ApiClient, IRunnersClient
    {
        public RunnersClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<RunnerRegistrationToken> CreateRegistrationToken(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<RunnerRegistrationToken>(ApiUrls.RunnersRegistrationToken(owner, name));
        }

        public Task<RunnerRegistrationToken> CreateRegistrationToken(long repositoryId)
        {
            return ApiConnection.Post<RunnerRegistrationToken>(ApiUrls.RunnersRegistrationToken(repositoryId));
        }

        public Task<RunnerRemoveToken> CreateRemoveToken(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<RunnerRemoveToken>(ApiUrls.RunnersRemoveToken(owner, name));
        }

        public Task<RunnerRemoveToken> CreateRemoveToken(long repositoryId)
        {
            return ApiConnection.Post<RunnerRemoveToken>(ApiUrls.RunnersRemoveToken(repositoryId));
        }

        public Task<Runner> Get(string owner, string name, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Runner>(ApiUrls.Runner(owner, name, runnerId));
        }

        public Task<Runner> Get(long repositoryId, long runnerId)
        {
            return ApiConnection.Get<Runner>(ApiUrls.Runner(repositoryId, runnerId));
        }

        public Task<IReadOnlyList<Runner>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            return GetAll(owner, name, ApiOptions.None);
        }

        public Task<IReadOnlyList<Runner>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public Task<IReadOnlyList<Runner>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Runner>(ApiUrls.Runners(owner, name), null, null, options);
        }

        public Task<IReadOnlyList<Runner>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Runner>(ApiUrls.Runners(repositoryId), null, null, options);
        }

        public Task<IReadOnlyList<RunnerDownload>> GetAllDownloads(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            return ApiConnection.GetAll<RunnerDownload>(ApiUrls.RunnersDownloads(owner, name));
        }

        public Task<IReadOnlyList<RunnerDownload>> GetAllDownloads(long repositoryId)
        {
            return ApiConnection.GetAll<RunnerDownload>(ApiUrls.RunnersDownloads(repositoryId));
        }

        public Task Remove(string owner, string name, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.Runner(owner, name, runnerId));
        }

        public Task Remove(long repositoryId, long runnerId)
        {
            return ApiConnection.Delete(ApiUrls.Runner(repositoryId, runnerId));
        }
    }
}
