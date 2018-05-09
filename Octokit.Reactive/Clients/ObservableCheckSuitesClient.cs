using System;

namespace Octokit.Reactive
{
	public class ObservableCheckSuitesClient : IObservableCheckSuitesClient
	{
		readonly ICheckSuitesClient _client;

		public ObservableCheckSuitesClient(IGitHubClient gitHubClient)
		{
			_client = gitHubClient.Checks.Suites;
		}

		public IObservable<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> Get(long repositoryId, long checkSuiteId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> Get(string owner, string name, long checkSuiteId)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> Request(long repositoryId, CheckSuiteTriggerRequest request)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuite> Request(string owner, string name, CheckSuiteTriggerRequest request)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuitePreferences> UpdatePreferences(long repositoryId, AutoTriggerChecksObject preferences)
		{
			throw new NotImplementedException();
		}

		public IObservable<CheckSuitePreferences> UpdatePreferences(string owner, string name, AutoTriggerChecksObject preferences)
		{
			throw new NotImplementedException();
		}
	}
}