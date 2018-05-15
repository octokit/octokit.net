using System;

namespace Octokit.Reactive
{
    public interface IObservableCheckRunsClient
    {
        IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun);
        IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun);
        IObservable<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate);
        IObservable<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate);
        IObservable<CheckRun> GetAllForReference(string owner, string name, string reference);
        IObservable<CheckRun> GetAllForReference(long repositoryId, string reference);
        IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId);
        IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId);
        IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest);
        IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest);
        IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest);
        IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest);
        IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options);
        IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options);
        IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);
        IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);
        IObservable<CheckRun> Get(string owner, string name, long checkRunId);
        IObservable<CheckRun> Get(long repositoryId, long checkRunId);
        IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId);
        IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId);
        IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options);
        IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options);
    }
}