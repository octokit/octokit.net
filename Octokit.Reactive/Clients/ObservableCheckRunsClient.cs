using System;

namespace Octokit.Reactive
{
	public class ObservableCheckRunsClient : IObservableCheckRunsClient
	{
		readonly ICheckRunsClient _client;

		public ObservableCheckRunsClient(IGitHubClient gitHubClient)
		{
			_client = gitHubClient.Checks.Runs;
		}

		public IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> Get(long repositoryId, long checkRunId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> Get(string owner, string name, long checkRunId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate)
		{
			throw new NotImplementedException();
		}
	}
}