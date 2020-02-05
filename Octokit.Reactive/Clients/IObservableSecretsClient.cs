using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableSecretsClient
    {
        IObservable<PublicKey> GetPublicKey(string owner, string name);
        IObservable<PublicKey> GetPublicKey(long repositoryId);

        IObservable<SecretsResponse> GetAll(string owner, string name);
        IObservable<SecretsResponse> GetAll(long repositoryId);

        IObservable<SecretsResponse> GetAll(string owner, string name, ApiOptions options);

        IObservable<SecretsResponse> GetAll(long repositoryId, ApiOptions options);

        IObservable<Secret> Get(string owner, string name, string secretName);
        IObservable<Secret> Get(long repositoryId, string secretName);

        IObservable<Unit> Create(string owner, string name, string secretName, SecretRequest secretRequest);
        IObservable<Unit> Create(long repositoryId, string secretName, SecretRequest secretRequest);
        IObservable<Unit> Update(string owner, string name, string secretName, SecretRequest secretRequest);
        IObservable<Unit> Update(long repositoryId, string secretName, SecretRequest secretRequest);

        IObservable<Unit> Delete(string owner, string name, string secretName);
        IObservable<Unit> Delete(long repositoryId, string secretName);

    }
}
