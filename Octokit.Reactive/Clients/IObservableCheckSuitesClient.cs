using System;

namespace Octokit.Reactive
{
    public interface IObservableCheckSuitesClient
    {
        IObservable<CheckSuite> Get(string owner, string name, long checkSuiteId);
        IObservable<CheckSuite> Get(long repositoryId, long checkSuiteId);
        IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference);
        IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference);
        IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request);
        IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request);
        IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options);
        IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options);
        IObservable<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences);
        IObservable<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences);
        IObservable<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite);
        IObservable<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite);
        IObservable<bool> Request(string owner, string name, CheckSuiteTriggerRequest request);
        IObservable<bool> Request(long repositoryId, CheckSuiteTriggerRequest request);
    }
}