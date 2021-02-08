using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWorkflowRunsClient : IObservableWorkflowRunsClient
    {
        readonly IConnection _connection;
        readonly IWorkflowRunsClient _client;

        public ObservableWorkflowRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Run;
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId)
            => GetAllForRepository(repositoryId, new WorkflowRunsRequest(), ApiOptions.None);

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRunsForRepository(repositoryId), request.ToParametersDictionary(), options);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRunsForRepository(owner, name), request.ToParametersDictionary(), options);
        }
    }
}
