using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    public interface ISecretsClient
    {
        Task<PublicKey> GetPublicKey(string owner, string name);
        Task<PublicKey> GetPublicKey(long repositoryId);

        Task<SecretsResponse> GetAll(string owner, string name);
        Task<SecretsResponse> GetAll(long repositoryId);

        Task<SecretsResponse> GetAll(string owner, string name, ApiOptions options);
        Task<SecretsResponse> GetAll(long repositoryId, ApiOptions options);

        Task<Secret> Get(string owner, string name, string secretName);
        Task<Secret> Get(long repositoryId, string secretName);

        Task Create(string owner, string name, string secretName, SecretRequest secretRequest);
        Task Create(long repositoryId, string secretName, SecretRequest secretRequest);
        Task Update(string owner, string name, string secretName, SecretRequest secretRequest);
        Task Update(long repositoryId, string secretName, SecretRequest secretRequest);

        Task Delete(string owner, string name, string secretName);
        Task Delete(long repositoryId, string secretName);

    }
}
