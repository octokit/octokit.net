using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class SecretsClient : ApiClient, ISecretsClient
    {
        public SecretsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task Create(string owner, string name, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return ApiConnection.Put<object>(ApiUrls.Secret(owner, name, secretName), secretRequest);
        }

        public Task Create(long repositoryId, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return ApiConnection.Put<object>(ApiUrls.Secret(repositoryId, secretName), secretRequest);
        }

        public Task Delete(string owner, string name, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Delete(ApiUrls.Secret(owner, name, secretName));
        }

        public Task Delete(long repositoryId, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Delete(ApiUrls.Secret(repositoryId, secretName));
        }

        public Task<Secret> Get(string owner, string name, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Get<Secret>(ApiUrls.Secret(owner, name, secretName));
        }

        public Task<Secret> Get(long repositoryId, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Get<Secret>(ApiUrls.Secret(repositoryId, secretName));
        }

        public Task<SecretsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        public Task<SecretsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public Task<SecretsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnSecretsResponse(ApiUrls.Secrets(owner, name), options);
        }

        public Task<SecretsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnSecretsResponse(ApiUrls.Secrets(repositoryId), options);
        }

        public Task<PublicKey> GetPublicKey(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<PublicKey>(ApiUrls.SecretsPublicKey(owner, name));
        }

        public Task<PublicKey> GetPublicKey(long repositoryId)
        {
            return ApiConnection.Get<PublicKey>(ApiUrls.SecretsPublicKey(repositoryId));
        }

        public Task Update(string owner, string name, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return ApiConnection.Put<object>(ApiUrls.Secret(owner, name, secretName), secretRequest);
        }

        public Task Update(long repositoryId, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));
            return ApiConnection.Put<object>(ApiUrls.Secret(repositoryId, secretName), secretRequest);
        }

        private async Task<SecretsResponse> RequestAndReturnSecretsResponse(Uri uri, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<SecretsResponse>(uri, options);
            return new SecretsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Secrets).ToList());
        }

    }
}
