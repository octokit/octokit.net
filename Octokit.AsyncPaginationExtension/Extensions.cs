using System;
using System.Collections.Generic;
using Octokit.Models.Request.Enterprise;

namespace Octokit.AsyncPaginationExtension
{
  /// <summary>
  /// Provides all extensions for pagination.
  /// </summary>
  /// <remarks>
  /// The <code>pageSize</code> parameter at the end of all methods allows for specifying the amount of elements to be fetched per page.
  /// Only useful to optimize the amount of API calls made.
  /// </remarks>
  public static class Extensions
  {
    private const int DEFAULT_PAGE_SIZE = 30;

    /// <inheritdoc cref="IApiConnection.GetAll(Uri, ApiOptions)"/>
    public static IPaginatedList<T> GetAllAsync<T>(this IApiConnection t, Uri uri, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<T>(options => t.GetAll<T>(uri, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IApiConnection.GetAll(Uri, IDictionary{string, string}, ApiOptions)"/>
    public static IPaginatedList<T> GetAllAsync<T>(this IApiConnection t, Uri uri, IDictionary<string, string> parameters, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<T>(options => t.GetAll<T>(uri, parameters, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IApiConnection.GetAll(Uri, IDictionary{string, string}, string, ApiOptions)"/>
    public static IPaginatedList<T> GetAllAsync<T>(this IApiConnection t, Uri uri, IDictionary<string, string> parameters, string accepts, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<T>(options => t.GetAll<T>(uri, parameters, accepts, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForRepositoryAsync(this IIssueCommentsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForRepositoryAsync(this IIssueCommentsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForRepository(string, string, IssueCommentRequest, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForRepositoryAsync(this IIssueCommentsClient t, string owner, string name, IssueCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForRepository(long, IssueCommentRequest, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForRepositoryAsync(this IIssueCommentsClient t, long repositoryId, IssueCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForIssue(string, string, int, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForIssueAsync(this IIssueCommentsClient t, string owner, string name, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForIssue(owner, name, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForIssue(long, int, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForIssueAsync(this IIssueCommentsClient t, long repositoryId, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForIssue(repositoryId, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForIssue(string, string, int, IssueCommentRequest, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForIssueAsync(this IIssueCommentsClient t, string owner, string name, int issueNumber, IssueCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForIssue(owner, name, issueNumber, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentsClient.GetAllForIssue(long, int, IssueCommentRequest, ApiOptions)"/>
    public static IPaginatedList<IssueComment> GetAllForIssueAsync(this IIssueCommentsClient t, long repositoryId, int issueNumber, IssueCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueComment>(options => t.GetAllForIssue(repositoryId, issueNumber, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryPagesClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<PagesBuild> GetAllAsync(this IRepositoryPagesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PagesBuild>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryPagesClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<PagesBuild> GetAllAsync(this IRepositoryPagesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PagesBuild>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IWatchedClient.GetAllWatchers(string, string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllWatchersAsync(this IWatchedClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllWatchers(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IWatchedClient.GetAllWatchers(long, ApiOptions)"/>
    public static IPaginatedList<User> GetAllWatchersAsync(this IWatchedClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllWatchers(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IWatchedClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForCurrentAsync(this IWatchedClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IWatchedClient.GetAllForUser(string, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForUserAsync(this IWatchedClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForUser(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForCurrentAsync(this INotificationsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForCurrent(NotificationsRequest, ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForCurrentAsync(this INotificationsClient t, NotificationsRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(options => t.GetAllForCurrent(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForRepositoryAsync(this INotificationsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForRepositoryAsync(this INotificationsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForRepository(string, string, NotificationsRequest, ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForRepositoryAsync(this INotificationsClient t, string owner, string name, NotificationsRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="INotificationsClient.GetAllForRepository(long, NotificationsRequest, ApiOptions)"/>
    public static IPaginatedList<Notification> GetAllForRepositoryAsync(this INotificationsClient t, long repositoryId, NotificationsRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Notification>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueTimelineClient.GetAllForIssue(string, string, int, ApiOptions)"/>
    public static IPaginatedList<TimelineEventInfo> GetAllForIssueAsync(this IIssueTimelineClient t, string owner, string repo, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<TimelineEventInfo>(options => t.GetAllForIssue(owner, repo, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueTimelineClient.GetAllForIssue(long, int, ApiOptions)"/>
    public static IPaginatedList<TimelineEventInfo> GetAllForIssueAsync(this IIssueTimelineClient t, long repositoryId, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<TimelineEventInfo>(options => t.GetAllForIssue(repositoryId, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IMiscellaneousClient.GetAllLicenses(ApiOptions)"/>
    public static IPaginatedList<LicenseMetadata> GetAllLicensesAsync(this IMiscellaneousClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<LicenseMetadata>(t.GetAllLicenses, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueReactionsClient.GetAll(string, string, int, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IIssueReactionsClient t, string owner, string name, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(owner, name, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueReactionsClient.GetAll(long, int, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IIssueReactionsClient t, long repositoryId, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(repositoryId, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryDeployKeysClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<DeployKey> GetAllAsync(this IRepositoryDeployKeysClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<DeployKey>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryDeployKeysClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<DeployKey> GetAllAsync(this IRepositoryDeployKeysClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<DeployKey>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllAsync(this IEventsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllForRepositoryAsync(this IEventsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllForRepositoryAsync(this IEventsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllIssuesForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllIssuesForRepositoryAsync(this IEventsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllIssuesForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllIssuesForRepository(long, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllIssuesForRepositoryAsync(this IEventsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllIssuesForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllForRepositoryNetwork(string, string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllForRepositoryNetworkAsync(this IEventsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllForRepositoryNetwork(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllForOrganization(string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllForOrganizationAsync(this IEventsClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllForOrganization(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllUserReceived(string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllUserReceivedAsync(this IEventsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllUserReceived(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllUserReceivedPublic(string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllUserReceivedPublicAsync(this IEventsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllUserReceivedPublic(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllUserPerformed(string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllUserPerformedAsync(this IEventsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllUserPerformed(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllUserPerformedPublic(string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllUserPerformedPublicAsync(this IEventsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllUserPerformedPublic(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEventsClient.GetAllForAnOrganization(string, string, ApiOptions)"/>
    public static IPaginatedList<Activity> GetAllForAnOrganizationAsync(this IEventsClient t, string user, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Activity>(options => t.GetAllForAnOrganization(user, organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IActionsSelfHostedRunnersClient.ListAllRunnerApplicationsForEnterprise(string, ApiOptions)"/>
    public static IPaginatedList<RunnerApplication> ListAllRunnerApplicationsForEnterpriseAsync(this IActionsSelfHostedRunnersClient t, string enterprise, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RunnerApplication>(options => t.ListAllRunnerApplicationsForEnterprise(enterprise, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IActionsSelfHostedRunnersClient.ListAllRunnerApplicationsForOrganization(string, ApiOptions)"/>
    public static IPaginatedList<RunnerApplication> ListAllRunnerApplicationsForOrganizationAsync(this IActionsSelfHostedRunnersClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RunnerApplication>(options => t.ListAllRunnerApplicationsForOrganization(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IActionsSelfHostedRunnersClient.ListAllRunnerApplicationsForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<RunnerApplication> ListAllRunnerApplicationsForRepositoryAsync(this IActionsSelfHostedRunnersClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RunnerApplication>(options => t.ListAllRunnerApplicationsForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IMilestonesClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Milestone> GetAllForRepositoryAsync(this IMilestonesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Milestone>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IMilestonesClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Milestone> GetAllForRepositoryAsync(this IMilestonesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Milestone>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IMilestonesClient.GetAllForRepository(string, string, MilestoneRequest, ApiOptions)"/>
    public static IPaginatedList<Milestone> GetAllForRepositoryAsync(this IMilestonesClient t, string owner, string name, MilestoneRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Milestone>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IMilestonesClient.GetAllForRepository(long, MilestoneRequest, ApiOptions)"/>
    public static IPaginatedList<Milestone> GetAllForRepositoryAsync(this IMilestonesClient t, long repositoryId, MilestoneRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Milestone>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IUserGpgKeysClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<GpgKey> GetAllForCurrentAsync(this IUserGpgKeysClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GpgKey>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IUserEmailsClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<EmailAddress> GetAllAsync(this IUserEmailsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<EmailAddress>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepoCollaboratorsClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Collaborator> GetAllAsync(this IRepoCollaboratorsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Collaborator>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepoCollaboratorsClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Collaborator> GetAllAsync(this IRepoCollaboratorsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Collaborator>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepoCollaboratorsClient.GetAll(string, string, RepositoryCollaboratorListRequest, ApiOptions)"/>
    public static IPaginatedList<Collaborator> GetAllAsync(this IRepoCollaboratorsClient t, string owner, string name, RepositoryCollaboratorListRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Collaborator>(options => t.GetAll(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepoCollaboratorsClient.GetAll(long, RepositoryCollaboratorListRequest, ApiOptions)"/>
    public static IPaginatedList<Collaborator> GetAllAsync(this IRepoCollaboratorsClient t, long repositoryId, RepositoryCollaboratorListRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Collaborator>(options => t.GetAll(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectColumnsClient.GetAll(int, ApiOptions)"/>
    public static IPaginatedList<ProjectColumn> GetAllAsync(this IProjectColumnsClient t, int projectId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<ProjectColumn>(options => t.GetAll(projectId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IAuthorizationsClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<Authorization> GetAllAsync(this IAuthorizationsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Authorization>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IAssigneesClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllForRepositoryAsync(this IAssigneesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IAssigneesClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<User> GetAllForRepositoryAsync(this IAssigneesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationCustomPropertyValuesClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<OrganizationCustomPropertyValues> GetAllAsync(this IOrganizationCustomPropertyValuesClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationCustomPropertyValues>(options => t.GetAll(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IDeploymentStatusClient.GetAll(string, string, long, ApiOptions)"/>
    public static IPaginatedList<DeploymentStatus> GetAllAsync(this IDeploymentStatusClient t, string owner, string name, long deploymentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<DeploymentStatus>(options => t.GetAll(owner, name, deploymentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IDeploymentStatusClient.GetAll(long, long, ApiOptions)"/>
    public static IPaginatedList<DeploymentStatus> GetAllAsync(this IDeploymentStatusClient t, long repositoryId, long deploymentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<DeploymentStatus>(options => t.GetAll(repositoryId, deploymentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IFollowersClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<User> GetAllForCurrentAsync(this IFollowersClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IFollowersClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IFollowersClient t, string login, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(login, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IFollowersClient.GetAllFollowingForCurrent(ApiOptions)"/>
    public static IPaginatedList<User> GetAllFollowingForCurrentAsync(this IFollowersClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(t.GetAllFollowingForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IFollowersClient.GetAllFollowing(string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllFollowingAsync(this IFollowersClient t, string login, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllFollowing(login, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<Team> GetAllAsync(this ITeamsClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Team>(options => t.GetAll(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Team> GetAllForCurrentAsync(this ITeamsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Team>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllChildTeams(long, ApiOptions)"/>
    public static IPaginatedList<Team> GetAllChildTeamsAsync(this ITeamsClient t, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Team>(options => t.GetAllChildTeams(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllMembers(long, ApiOptions)"/>
    public static IPaginatedList<User> GetAllMembersAsync(this ITeamsClient t, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllMembers(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllMembers(long, TeamMembersRequest, ApiOptions)"/>
    public static IPaginatedList<User> GetAllMembersAsync(this ITeamsClient t, long id, TeamMembersRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllMembers(id, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllRepositories(long, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllRepositoriesAsync(this ITeamsClient t, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllRepositories(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ITeamsClient.GetAllPendingInvitations(long, ApiOptions)"/>
    public static IPaginatedList<OrganizationMembershipInvitation> GetAllPendingInvitationsAsync(this ITeamsClient t, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationMembershipInvitation>(options => t.GetAllPendingInvitations(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGitHubAppsClient.GetAllInstallationsForCurrent(ApiOptions)"/>
    public static IPaginatedList<Installation> GetAllInstallationsForCurrentAsync(this IGitHubAppsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Installation>(t.GetAllInstallationsForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForOrg(string, PackageType, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForOrgAsync(this IPackagesClient t, string org, PackageType packageType, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForOrg(org, packageType, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForOrg(string, PackageType, PackageVisibility?, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForOrgAsync(this IPackagesClient t, string org, PackageType packageType, PackageVisibility? packageVisibility, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForOrg(org, packageType, packageVisibility, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForActiveUser(PackageType, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForActiveUserAsync(this IPackagesClient t, PackageType packageType, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForActiveUser(packageType, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForActiveUser(PackageType, PackageVisibility?, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForActiveUserAsync(this IPackagesClient t, PackageType packageType, PackageVisibility? packageVisibility, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForActiveUser(packageType, packageVisibility, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForUser(string, PackageType, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForUserAsync(this IPackagesClient t, string username, PackageType packageType, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForUser(username, packageType, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPackagesClient.GetAllForUser(string, PackageType, PackageVisibility?, ApiOptions)"/>
    public static IPaginatedList<Package> GetAllForUserAsync(this IPackagesClient t, string username, PackageType packageType, PackageVisibility? packageVisibility, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Package>(options => t.GetAllForUser(username, packageType, packageVisibility, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ILicensesClient.GetAllLicenses(ApiOptions)"/>
    public static IPaginatedList<LicenseMetadata> GetAllLicensesAsync(this ILicensesClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<LicenseMetadata>(t.GetAllLicenses, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationMembersClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAll(string, OrganizationMembersFilter, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationMembersClient t, string org, OrganizationMembersFilter filter, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, filter, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAll(string, OrganizationMembersRole, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationMembersClient t, string org, OrganizationMembersRole role, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, role, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAll(string, OrganizationMembersFilter, OrganizationMembersRole, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationMembersClient t, string org, OrganizationMembersFilter filter, OrganizationMembersRole role, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, filter, role, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAllPublic(string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllPublicAsync(this IOrganizationMembersClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllPublic(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAllPendingInvitations(string, ApiOptions)"/>
    public static IPaginatedList<OrganizationMembershipInvitation> GetAllPendingInvitationsAsync(this IOrganizationMembersClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationMembershipInvitation>(options => t.GetAllPendingInvitations(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAllFailedInvitations(string, ApiOptions)"/>
    public static IPaginatedList<OrganizationMembershipInvitation> GetAllFailedInvitationsAsync(this IOrganizationMembersClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationMembershipInvitation>(options => t.GetAllFailedInvitations(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationMembersClient.GetAllOrganizationMembershipsForCurrent(ApiOptions)"/>
    public static IPaginatedList<OrganizationMembership> GetAllOrganizationMembershipsForCurrentAsync(this IOrganizationMembersClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationMembership>(t.GetAllOrganizationMembershipsForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationsClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Organization> GetAllForCurrentAsync(this IOrganizationsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Organization>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationsClient.GetAllForUser(string, ApiOptions)"/>
    public static IPaginatedList<Organization> GetAllForUserAsync(this IOrganizationsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Organization>(options => t.GetAllForUser(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationsClient.GetAllAuthorizations(string, ApiOptions)"/>
    public static IPaginatedList<OrganizationCredential> GetAllAuthorizationsAsync(this IOrganizationsClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationCredential>(options => t.GetAllAuthorizations(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationsClient.GetAllAuthorizations(string, string, ApiOptions)"/>
    public static IPaginatedList<OrganizationCredential> GetAllAuthorizationsAsync(this IOrganizationsClient t, string org, string login, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationCredential>(options => t.GetAllAuthorizations(org, login, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICheckRunsClient.GetAllAnnotations(string, string, long, ApiOptions)"/>
    public static IPaginatedList<CheckRunAnnotation> GetAllAnnotationsAsync(this ICheckRunsClient t, string owner, string name, long checkRunId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CheckRunAnnotation>(options => t.GetAllAnnotations(owner, name, checkRunId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICheckRunsClient.GetAllAnnotations(long, long, ApiOptions)"/>
    public static IPaginatedList<CheckRunAnnotation> GetAllAnnotationsAsync(this ICheckRunsClient t, long repositoryId, long checkRunId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CheckRunAnnotation>(options => t.GetAllAnnotations(repositoryId, checkRunId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForIssue(string, string, int, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForIssueAsync(this IIssuesLabelsClient t, string owner, string name, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForIssue(owner, name, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForIssue(long, int, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForIssueAsync(this IIssuesLabelsClient t, long repositoryId, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForIssue(repositoryId, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForRepositoryAsync(this IIssuesLabelsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForRepositoryAsync(this IIssuesLabelsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForMilestone(string, string, int, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForMilestoneAsync(this IIssuesLabelsClient t, string owner, string name, int milestoneNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForMilestone(owner, name, milestoneNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesLabelsClient.GetAllForMilestone(long, int, ApiOptions)"/>
    public static IPaginatedList<Label> GetAllForMilestoneAsync(this IIssuesLabelsClient t, long repositoryId, int milestoneNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Label>(options => t.GetAllForMilestone(repositoryId, milestoneNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationOutsideCollaboratorsClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationOutsideCollaboratorsClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationOutsideCollaboratorsClient.GetAll(string, OrganizationMembersFilter, ApiOptions)"/>
    public static IPaginatedList<User> GetAllAsync(this IOrganizationOutsideCollaboratorsClient t, string org, OrganizationMembersFilter filter, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAll(org, filter, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICommitCommentReactionsClient.GetAll(string, string, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this ICommitCommentReactionsClient t, string owner, string name, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(owner, name, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICommitCommentReactionsClient.GetAll(long, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this ICommitCommentReactionsClient t, long repositoryId, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(repositoryId, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryBranchesClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Branch> GetAllAsync(this IRepositoryBranchesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Branch>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryBranchesClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Branch> GetAllAsync(this IRepositoryBranchesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Branch>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllStargazers(string, string, ApiOptions)"/>
    public static IPaginatedList<User> GetAllStargazersAsync(this IStarredClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllStargazers(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllStargazers(long, ApiOptions)"/>
    public static IPaginatedList<User> GetAllStargazersAsync(this IStarredClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<User>(options => t.GetAllStargazers(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllStargazersWithTimestamps(string, string, ApiOptions)"/>
    public static IPaginatedList<UserStar> GetAllStargazersWithTimestampsAsync(this IStarredClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<UserStar>(options => t.GetAllStargazersWithTimestamps(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllStargazersWithTimestamps(long, ApiOptions)"/>
    public static IPaginatedList<UserStar> GetAllStargazersWithTimestampsAsync(this IStarredClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<UserStar>(options => t.GetAllStargazersWithTimestamps(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForCurrentAsync(this IStarredClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForCurrentWithTimestamps(ApiOptions)"/>
    public static IPaginatedList<RepositoryStar> GetAllForCurrentWithTimestampsAsync(this IStarredClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryStar>(t.GetAllForCurrentWithTimestamps, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForCurrent(StarredRequest, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForCurrentAsync(this IStarredClient t, StarredRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForCurrent(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForCurrentWithTimestamps(StarredRequest, ApiOptions)"/>
    public static IPaginatedList<RepositoryStar> GetAllForCurrentWithTimestampsAsync(this IStarredClient t, StarredRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryStar>(options => t.GetAllForCurrentWithTimestamps(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForUser(string, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForUserAsync(this IStarredClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForUser(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForUserWithTimestamps(string, ApiOptions)"/>
    public static IPaginatedList<RepositoryStar> GetAllForUserWithTimestampsAsync(this IStarredClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryStar>(options => t.GetAllForUserWithTimestamps(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForUser(string, StarredRequest, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForUserAsync(this IStarredClient t, string user, StarredRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForUser(user, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IStarredClient.GetAllForUserWithTimestamps(string, StarredRequest, ApiOptions)"/>
    public static IPaginatedList<RepositoryStar> GetAllForUserWithTimestampsAsync(this IStarredClient t, string user, StarredRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryStar>(options => t.GetAllForUserWithTimestamps(user, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICommitStatusClient.GetAll(string, string, string, ApiOptions)"/>
    public static IPaginatedList<CommitStatus> GetAllAsync(this ICommitStatusClient t, string owner, string name, string reference, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitStatus>(options => t.GetAll(owner, name, reference, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICommitStatusClient.GetAll(long, string, ApiOptions)"/>
    public static IPaginatedList<CommitStatus> GetAllAsync(this ICommitStatusClient t, long repositoryId, string reference, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitStatus>(options => t.GetAll(repositoryId, reference, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IAutolinksClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Autolink> GetAllAsync(this IAutolinksClient t, string owner, string repo, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Autolink>(options => t.GetAll(owner, repo, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForCurrentAsync(this IIssuesClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForCurrent(IssueRequest, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForCurrentAsync(this IIssuesClient t, IssueRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForCurrent(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForOwnedAndMemberRepositories(ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForOwnedAndMemberRepositoriesAsync(this IIssuesClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(t.GetAllForOwnedAndMemberRepositories, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForOwnedAndMemberRepositories(IssueRequest, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForOwnedAndMemberRepositoriesAsync(this IIssuesClient t, IssueRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForOwnedAndMemberRepositories(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForOrganization(string, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForOrganizationAsync(this IIssuesClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForOrganization(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForOrganization(string, IssueRequest, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForOrganizationAsync(this IIssuesClient t, string organization, IssueRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForOrganization(organization, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForRepositoryAsync(this IIssuesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForRepositoryAsync(this IIssuesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForRepository(string, string, RepositoryIssueRequest, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForRepositoryAsync(this IIssuesClient t, string owner, string name, RepositoryIssueRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesClient.GetAllForRepository(long, RepositoryIssueRequest, ApiOptions)"/>
    public static IPaginatedList<Issue> GetAllForRepositoryAsync(this IIssuesClient t, long repositoryId, RepositoryIssueRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Issue>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentReactionsClient.GetAll(string, string, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IPullRequestReviewCommentReactionsClient t, string owner, string name, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(owner, name, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentReactionsClient.GetAll(long, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IPullRequestReviewCommentReactionsClient t, long repositoryId, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(repositoryId, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesEventsClient.GetAllForIssue(string, string, int, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllForIssueAsync(this IIssuesEventsClient t, string owner, string name, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllForIssue(owner, name, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesEventsClient.GetAllForIssue(long, int, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllForIssueAsync(this IIssuesEventsClient t, long repositoryId, int issueNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllForIssue(repositoryId, issueNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesEventsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllForRepositoryAsync(this IIssuesEventsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssuesEventsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<IssueEvent> GetAllForRepositoryAsync(this IIssuesEventsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<IssueEvent>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IOrganizationHooksClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<OrganizationHook> GetAllAsync(this IOrganizationHooksClient t, string org, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<OrganizationHook>(options => t.GetAll(org, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryInvitationsClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<RepositoryInvitation> GetAllForCurrentAsync(this IRepositoryInvitationsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryInvitation>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryInvitationsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<RepositoryInvitation> GetAllForRepositoryAsync(this IRepositoryInvitationsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryInvitation>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IDeploymentsClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Deployment> GetAllAsync(this IDeploymentsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Deployment>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IDeploymentsClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Deployment> GetAllAsync(this IDeploymentsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Deployment>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryHooksClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<RepositoryHook> GetAllAsync(this IRepositoryHooksClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryHook>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryHooksClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<RepositoryHook> GetAllAsync(this IRepositoryHooksClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryHook>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistCommentsClient.GetAllForGist(string, ApiOptions)"/>
    public static IPaginatedList<GistComment> GetAllForGistAsync(this IGistCommentsClient t, string gistId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GistComment>(options => t.GetAllForGist(gistId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentReactionsClient.GetAll(string, string, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IIssueCommentReactionsClient t, string owner, string name, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(owner, name, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IIssueCommentReactionsClient.GetAll(long, long, ApiOptions)"/>
    public static IPaginatedList<Reaction> GetAllAsync(this IIssueCommentReactionsClient t, long repositoryId, long commentId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reaction>(options => t.GetAll(repositoryId, commentId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReferencesClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Reference> GetAllAsync(this IReferencesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reference>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReferencesClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Reference> GetAllAsync(this IReferencesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reference>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReferencesClient.GetAllForSubNamespace(string, string, string, ApiOptions)"/>
    public static IPaginatedList<Reference> GetAllForSubNamespaceAsync(this IReferencesClient t, string owner, string name, string subNamespace, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reference>(options => t.GetAllForSubNamespace(owner, name, subNamespace, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReferencesClient.GetAllForSubNamespace(long, string, ApiOptions)"/>
    public static IPaginatedList<Reference> GetAllForSubNamespaceAsync(this IReferencesClient t, long repositoryId, string subNamespace, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Reference>(options => t.GetAllForSubNamespace(repositoryId, subNamespace, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForCurrentAsync(this IRepositoriesClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllForCurrent(RepositoryRequest, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForCurrentAsync(this IRepositoriesClient t, RepositoryRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForCurrent(request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllForUser(string, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForUserAsync(this IRepositoriesClient t, string login, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForUser(login, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllForOrg(string, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllForOrgAsync(this IRepositoriesClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAllForOrg(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllContributors(string, string, ApiOptions)"/>
    public static IPaginatedList<RepositoryContributor> GetAllContributorsAsync(this IRepositoriesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryContributor>(options => t.GetAllContributors(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllContributors(long, ApiOptions)"/>
    public static IPaginatedList<RepositoryContributor> GetAllContributorsAsync(this IRepositoriesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryContributor>(options => t.GetAllContributors(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllContributors(string, string, bool, ApiOptions)"/>
    public static IPaginatedList<RepositoryContributor> GetAllContributorsAsync(this IRepositoriesClient t, string owner, string name, bool includeAnonymous, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryContributor>(options => t.GetAllContributors(owner, name, includeAnonymous, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllContributors(long, bool, ApiOptions)"/>
    public static IPaginatedList<RepositoryContributor> GetAllContributorsAsync(this IRepositoriesClient t, long repositoryId, bool includeAnonymous, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryContributor>(options => t.GetAllContributors(repositoryId, includeAnonymous, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllTeams(string, string, ApiOptions)"/>
    public static IPaginatedList<Team> GetAllTeamsAsync(this IRepositoriesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Team>(options => t.GetAllTeams(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllTeams(long, ApiOptions)"/>
    public static IPaginatedList<Team> GetAllTeamsAsync(this IRepositoriesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Team>(options => t.GetAllTeams(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllTags(string, string, ApiOptions)"/>
    public static IPaginatedList<RepositoryTag> GetAllTagsAsync(this IRepositoriesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryTag>(options => t.GetAllTags(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoriesClient.GetAllTags(long, ApiOptions)"/>
    public static IPaginatedList<RepositoryTag> GetAllTagsAsync(this IRepositoriesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<RepositoryTag>(options => t.GetAllTags(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryForksClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllAsync(this IRepositoryForksClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryForksClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllAsync(this IRepositoryForksClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryForksClient.GetAll(string, string, RepositoryForksListRequest, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllAsync(this IRepositoryForksClient t, string owner, string name, RepositoryForksListRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAll(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryForksClient.GetAll(long, RepositoryForksListRequest, ApiOptions)"/>
    public static IPaginatedList<Repository> GetAllAsync(this IRepositoryForksClient t, long repositoryId, RepositoryForksListRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Repository>(options => t.GetAll(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IUserKeysClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<PublicKey> GetAllAsync(this IUserKeysClient t, string userName, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PublicKey>(options => t.GetAll(userName, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IUserKeysClient.GetAllForCurrent(ApiOptions)"/>
    public static IPaginatedList<PublicKey> GetAllForCurrentAsync(this IUserKeysClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PublicKey>(t.GetAllForCurrent, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAll(string, string, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllAsync(this IPullRequestReviewCommentsClient t, string owner, string name, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAll(owner, name, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAll(long, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllAsync(this IPullRequestReviewCommentsClient t, long repositoryId, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAll(repositoryId, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllForRepositoryAsync(this IPullRequestReviewCommentsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllForRepositoryAsync(this IPullRequestReviewCommentsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAllForRepository(string, string, PullRequestReviewCommentRequest, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllForRepositoryAsync(this IPullRequestReviewCommentsClient t, string owner, string name, PullRequestReviewCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewCommentsClient.GetAllForRepository(long, PullRequestReviewCommentRequest, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllForRepositoryAsync(this IPullRequestReviewCommentsClient t, long repositoryId, PullRequestReviewCommentRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReleasesClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<Release> GetAllAsync(this IReleasesClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Release>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReleasesClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<Release> GetAllAsync(this IReleasesClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Release>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReleasesClient.GetAllAssets(string, string, long, ApiOptions)"/>
    public static IPaginatedList<ReleaseAsset> GetAllAssetsAsync(this IReleasesClient t, string owner, string name, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<ReleaseAsset>(options => t.GetAllAssets(owner, name, id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IReleasesClient.GetAllAssets(long, long, ApiOptions)"/>
    public static IPaginatedList<ReleaseAsset> GetAllAssetsAsync(this IReleasesClient t, long repositoryId, long id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<ReleaseAsset>(options => t.GetAllAssets(repositoryId, id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<PullRequest> GetAllForRepositoryAsync(this IPullRequestsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequest>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<PullRequest> GetAllForRepositoryAsync(this IPullRequestsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequest>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.GetAllForRepository(string, string, PullRequestRequest, ApiOptions)"/>
    public static IPaginatedList<PullRequest> GetAllForRepositoryAsync(this IPullRequestsClient t, string owner, string name, PullRequestRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequest>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.GetAllForRepository(long, PullRequestRequest, ApiOptions)"/>
    public static IPaginatedList<PullRequest> GetAllForRepositoryAsync(this IPullRequestsClient t, long repositoryId, PullRequestRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequest>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.Files(string, string, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestFile> FilesAsync(this IPullRequestsClient t, string owner, string name, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestFile>(options => t.Files(owner, name, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestsClient.Files(long, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestFile> FilesAsync(this IPullRequestsClient t, long repositoryId, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestFile>(options => t.Files(repositoryId, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllAsync(this IGistsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAll(DateTimeOffset, ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllAsync(this IGistsClient t, DateTimeOffset since, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(options => t.GetAll(since, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllPublic(ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllPublicAsync(this IGistsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(t.GetAllPublic, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllPublic(DateTimeOffset, ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllPublicAsync(this IGistsClient t, DateTimeOffset since, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(options => t.GetAllPublic(since, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllStarred(ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllStarredAsync(this IGistsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(t.GetAllStarred, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllStarred(DateTimeOffset, ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllStarredAsync(this IGistsClient t, DateTimeOffset since, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(options => t.GetAllStarred(since, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllForUser(string, ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllForUserAsync(this IGistsClient t, string user, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(options => t.GetAllForUser(user, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllForUser(string, DateTimeOffset, ApiOptions)"/>
    public static IPaginatedList<Gist> GetAllForUserAsync(this IGistsClient t, string user, DateTimeOffset since, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Gist>(options => t.GetAllForUser(user, since, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllCommits(string, ApiOptions)"/>
    public static IPaginatedList<GistHistory> GetAllCommitsAsync(this IGistsClient t, string id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GistHistory>(options => t.GetAllCommits(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IGistsClient.GetAllForks(string, ApiOptions)"/>
    public static IPaginatedList<GistFork> GetAllForksAsync(this IGistsClient t, string id, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GistFork>(options => t.GetAllForks(id, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForRepositoryAsync(this IProjectsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForRepository(string, string, ProjectRequest, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForRepositoryAsync(this IProjectsClient t, string owner, string name, ProjectRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForRepository(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForRepositoryAsync(this IProjectsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForRepository(long, ProjectRequest, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForRepositoryAsync(this IProjectsClient t, long repositoryId, ProjectRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForRepository(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForOrganization(string, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForOrganizationAsync(this IProjectsClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForOrganization(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectsClient.GetAllForOrganization(string, ProjectRequest, ApiOptions)"/>
    public static IPaginatedList<Project> GetAllForOrganizationAsync(this IProjectsClient t, string organization, ProjectRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Project>(options => t.GetAllForOrganization(organization, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewsClient.GetAll(string, string, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestReview> GetAllAsync(this IPullRequestReviewsClient t, string owner, string name, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReview>(options => t.GetAll(owner, name, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewsClient.GetAll(long, int, ApiOptions)"/>
    public static IPaginatedList<PullRequestReview> GetAllAsync(this IPullRequestReviewsClient t, long repositoryId, int pullRequestNumber, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReview>(options => t.GetAll(repositoryId, pullRequestNumber, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewsClient.GetAllComments(string, string, int, long, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllCommentsAsync(this IPullRequestReviewsClient t, string owner, string name, int pullRequestNumber, long reviewId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllComments(owner, name, pullRequestNumber, reviewId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IPullRequestReviewsClient.GetAllComments(long, int, long, ApiOptions)"/>
    public static IPaginatedList<PullRequestReviewComment> GetAllCommentsAsync(this IPullRequestReviewsClient t, long repositoryId, int pullRequestNumber, long reviewId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PullRequestReviewComment>(options => t.GetAllComments(repositoryId, pullRequestNumber, reviewId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommentsClient.GetAllForRepository(string, string, ApiOptions)"/>
    public static IPaginatedList<CommitComment> GetAllForRepositoryAsync(this IRepositoryCommentsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitComment>(options => t.GetAllForRepository(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommentsClient.GetAllForRepository(long, ApiOptions)"/>
    public static IPaginatedList<CommitComment> GetAllForRepositoryAsync(this IRepositoryCommentsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitComment>(options => t.GetAllForRepository(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommentsClient.GetAllForCommit(string, string, string, ApiOptions)"/>
    public static IPaginatedList<CommitComment> GetAllForCommitAsync(this IRepositoryCommentsClient t, string owner, string name, string sha, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitComment>(options => t.GetAllForCommit(owner, name, sha, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommentsClient.GetAllForCommit(long, string, ApiOptions)"/>
    public static IPaginatedList<CommitComment> GetAllForCommitAsync(this IRepositoryCommentsClient t, long repositoryId, string sha, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitComment>(options => t.GetAllForCommit(repositoryId, sha, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.BranchesWhereHead(long, string, ApiOptions)"/>
    public static IPaginatedList<Branch> BranchesWhereHeadAsync(this IRepositoryCommitsClient t, long repositoryId, string sha1, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Branch>(options => t.BranchesWhereHead(repositoryId, sha1, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.BranchesWhereHead(string, string, string, ApiOptions)"/>
    public static IPaginatedList<Branch> BranchesWhereHeadAsync(this IRepositoryCommitsClient t, string owner, string name, string sha1, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<Branch>(options => t.BranchesWhereHead(owner, name, sha1, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.GetAll(string, string, ApiOptions)"/>
    public static IPaginatedList<GitHubCommit> GetAllAsync(this IRepositoryCommitsClient t, string owner, string name, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GitHubCommit>(options => t.GetAll(owner, name, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.GetAll(long, ApiOptions)"/>
    public static IPaginatedList<GitHubCommit> GetAllAsync(this IRepositoryCommitsClient t, long repositoryId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GitHubCommit>(options => t.GetAll(repositoryId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.GetAll(string, string, CommitRequest, ApiOptions)"/>
    public static IPaginatedList<GitHubCommit> GetAllAsync(this IRepositoryCommitsClient t, string owner, string name, CommitRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GitHubCommit>(options => t.GetAll(owner, name, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.GetAll(long, CommitRequest, ApiOptions)"/>
    public static IPaginatedList<GitHubCommit> GetAllAsync(this IRepositoryCommitsClient t, long repositoryId, CommitRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<GitHubCommit>(options => t.GetAll(repositoryId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.PullRequests(long, string, ApiOptions)"/>
    public static IPaginatedList<CommitPullRequest> PullRequestsAsync(this IRepositoryCommitsClient t, long repositoryId, string sha1, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitPullRequest>(options => t.PullRequests(repositoryId, sha1, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IRepositoryCommitsClient.PullRequests(string, string, string, ApiOptions)"/>
    public static IPaginatedList<CommitPullRequest> PullRequestsAsync(this IRepositoryCommitsClient t, string owner, string name, string sha1, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CommitPullRequest>(options => t.PullRequests(owner, name, sha1, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectCardsClient.GetAll(int, ApiOptions)"/>
    public static IPaginatedList<ProjectCard> GetAllAsync(this IProjectCardsClient t, int columnId, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<ProjectCard>(options => t.GetAll(columnId, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IProjectCardsClient.GetAll(int, ProjectCardRequest, ApiOptions)"/>
    public static IPaginatedList<ProjectCard> GetAllAsync(this IProjectCardsClient t, int columnId, ProjectCardRequest request, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<ProjectCard>(options => t.GetAll(columnId, request, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="ICopilotLicenseClient.GetAll(string, ApiOptions)"/>
    public static IPaginatedList<CopilotSeats> GetAllAsync(this ICopilotLicenseClient t, string organization, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<CopilotSeats>(options => t.GetAll(organization, options), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterprisePreReceiveEnvironmentsClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<PreReceiveEnvironment> GetAllAsync(this IEnterprisePreReceiveEnvironmentsClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PreReceiveEnvironment>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterprisePreReceiveHooksClient.GetAll(ApiOptions)"/>
    public static IPaginatedList<PreReceiveHook> GetAllAsync(this IEnterprisePreReceiveHooksClient t, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<PreReceiveHook>(t.GetAll, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterpriseAuditLogClient.GetAll(string, AuditLogApiOptions, ApiOptions)"/>
    public static IPaginatedList<AuditLogEvent> GetAllAsync(this IEnterpriseAuditLogClient t, string enterprise, AuditLogApiOptions auditLog, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<AuditLogEvent>(options => t.GetAll(enterprise, auditLog), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterpriseAuditLogClient.GetAll(string, AuditLogRequest, AuditLogApiOptions, ApiOptions)"/>
    public static IPaginatedList<AuditLogEvent> GetAllAsync(this IEnterpriseAuditLogClient t, string enterprise, AuditLogRequest request, AuditLogApiOptions auditLog, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<AuditLogEvent>(options => t.GetAll(enterprise, request, auditLog), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterpriseAuditLogClient.GetAllJson(string, AuditLogApiOptions, ApiOptions)"/>
    public static IPaginatedList<object> GetAllJsonAsync(this IEnterpriseAuditLogClient t, string enterprise, AuditLogApiOptions auditLog, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<object>(options => t.GetAllJson(enterprise, auditLog), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

    /// <inheritdoc cref="IEnterpriseAuditLogClient.GetAllJson(string, AuditLogRequest, AuditLogApiOptions, ApiOptions" />
    public static IPaginatedList<object> GetAllJsonAsync(this IEnterpriseAuditLogClient t, string enterprise, AuditLogRequest request, AuditLogApiOptions auditLog, int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<object>(options => t.GetAllJson(enterprise, request, auditLog), pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be positive.");

  }
}
