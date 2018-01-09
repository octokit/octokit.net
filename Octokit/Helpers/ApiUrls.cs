using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// Class for retrieving GitHub API URLs
    /// </summary>
    public static partial class ApiUrls
    {
        static readonly Uri _currentUserRepositoriesUrl = new Uri("user/repos", UriKind.Relative);
        static readonly Uri _currentUserOrganizationsUrl = new Uri("user/orgs", UriKind.Relative);
        static readonly Uri _currentUserSshKeys = new Uri("user/keys", UriKind.Relative);
        static readonly Uri _currentUserGpgKeys = new Uri("user/gpg_keys", UriKind.Relative);
        static readonly Uri _currentUserStars = new Uri("user/starred", UriKind.Relative);
        static readonly Uri _currentUserWatched = new Uri("user/subscriptions", UriKind.Relative);
        static readonly Uri _currentUserEmailsEndpoint = new Uri("user/emails", UriKind.Relative);
        static readonly Uri _currentUserNotificationsEndpoint = new Uri("notifications", UriKind.Relative);
        static readonly Uri _currentUserAllIssues = new Uri("issues", UriKind.Relative);
        static readonly Uri _currentUserOwnedAndMemberIssues = new Uri("user/issues", UriKind.Relative);
        
        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all public repositories in
        /// response to a GET request.
        /// </summary>
        public static Uri AllPublicRepositories()
        {
            return "repositories".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all public repositories in
        /// response to a GET request.
        /// </summary>
        /// <param name="since">The integer Id of the last Repository that you’ve seen.</param>
        public static Uri AllPublicRepositories(long since)
        {
            return "repositories?since={0}".FormatUri(since);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the repositories for the currently logged in user in
        /// response to a GET request. A POST to this URL creates a new repository.
        /// </summary>
        /// <returns></returns>
        public static Uri Repositories()
        {
            return _currentUserRepositoriesUrl;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the repositories for the specified login.
        /// </summary>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        public static Uri Repositories(string login)
        {
            return "users/{0}/repos".FormatUri(login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the repositories for the specified organization in
        /// response to a GET request. A POST to this URL creates a new repository for the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationRepositories(string organization)
        {
            return "orgs/{0}/repos".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Please use ApiUrls.UserOrganizations() instead. This method will be removed in a future version")]
        public static Uri Organizations()
        {
            return _currentUserOrganizationsUrl;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations for the specified login.
        /// </summary>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        [Obsolete("Please use ApiUrls.UserOrganizations() instead. This method will be removed in a future version")]
        public static Uri Organizations(string login)
        {
            return "users/{0}/orgs".FormatUri(login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri UserOrganizations()
        {
            return "user/orgs".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations for the specified login.
        /// </summary>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        public static Uri UserOrganizations(string login)
        {
            return "users/{0}/orgs".FormatUri(login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations.
        /// </summary>
        /// <returns></returns>
        public static Uri AllOrganizations()
        {
            return "organizations".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations.
        /// </summary>
        /// /// <param name="since">The integer Id of the last Organization that you’ve seen.</param>
        /// <returns></returns>
        public static Uri AllOrganizations(long since)
        {
            return "organizations?since={0}".FormatUri(since);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the organization for the specified organization name
        /// </summary>
        /// <param name="organizationName">The name of the organization</param>
        /// <returns>The <see cref="Uri"/> that returns the organization for the specified organization name</returns>
        public static Uri Organization(string organizationName)
        {
            return "orgs/{0}".FormatUri(organizationName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the SSH keys for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri SshKeys()
        {
            return _currentUserSshKeys;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the SSH keys for the specified user.
        /// </summary>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        public static Uri SshKeys(string login)
        {
            return "users/{0}/keys".FormatUri(login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to retrieve keys for the current user.
        /// </summary>
        public static Uri Keys()
        {
            return "user/keys".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to retrieve keys for a given user.
        /// </summary>
        /// <param name="userName">The user to search on</param>
        public static Uri Keys(string userName)
        {
            return "users/{0}/keys".FormatUri(userName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to retrieve a given key.
        /// </summary>
        /// <param name="id">The Key Id to retrieve</param>
        public static Uri Keys(int id)
        {
            return "user/keys/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the email addresses for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri Emails()
        {
            return _currentUserEmailsEndpoint;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the releases for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Releases(string owner, string name)
        {
            return "repos/{0}/{1}/releases".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <returns></returns>
        public static Uri Releases(string owner, string name, int id)
        {
            return "repos/{0}/{1}/releases/{2}".FormatUri(owner, name, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the latest release for the specified repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri LatestRelease(string owner, string name)
        {
            return "repos/{0}/{1}/releases/latest".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the assets for the specified release for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <returns></returns>
        public static Uri ReleaseAssets(string owner, string name, int id)
        {
            return "repos/{0}/{1}/releases/{2}/assets".FormatUri(owner, name, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the assets specified by the asset id.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the release asset</param>
        /// <returns></returns>
        public static Uri Asset(string owner, string name, int id)
        {
            return "repos/{0}/{1}/releases/assets/{2}".FormatUri(owner, name, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the notifications for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri Notifications()
        {
            return _currentUserNotificationsEndpoint;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the notifications for the currently logged in user
        /// specific to the repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Notifications(string owner, string name)
        {
            return "repos/{0}/{1}/notifications".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified notification.
        /// </summary>
        /// <param name="id">The Id of the notification.</param>
        /// <returns></returns>
        public static Uri Notification(int id)
        {
            return "notifications/threads/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified notification's subscription status.
        /// </summary>
        /// <param name="id">The Id of the notification.</param>
        /// <returns></returns>
        public static Uri NotificationSubscription(int id)
        {
            return "notifications/threads/{0}/subscription".FormatUri(id);
        }

        public static Uri AccessTokens(int installationId)
        {
            return "installations/{0}/access_tokens".FormatUri(installationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that creates a github app.
        /// </summary>
        public static Uri App()
        {
            return "app".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the installations of the authenticated application.
        /// </summary>
        /// <returns></returns>
        public static Uri Installations()
        {
            return "app/installations".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single installation of the authenticated application.
        /// </summary>
        /// <returns></returns>
        public static Uri Installation(int installationId)
        {
            return "app/installations/{0}".FormatUri(installationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the issues  across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories:
        /// </summary>
        public static Uri Issues()
        {
            return _currentUserAllIssues;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the issues across owned and member repositories for the
        /// authenticated user:
        /// </summary>
        public static Uri IssuesForOwnedAndMember()
        {
            return _currentUserOwnedAndMemberIssues;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the issues for the currently logged in user
        /// specific to the repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Issues(string owner, string name)
        {
            return "repos/{0}/{1}/issues".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the issues for the specified organization  for the
        /// currently logged in user.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri Issues(string organization)
        {
            return "orgs/{0}/issues".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri Issue(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue to be locked/unlocked.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueLock(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/lock".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueReactions(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/reactions".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueReactions(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/reactions".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the timeline of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueTimeline(string owner, string repo, int number)
        {
            return "repos/{0}/{1}/issues/{2}/timeline".FormatUri(owner, repo, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the timeline of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueTimeline(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/timeline".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments for all issues in a specific repo.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri IssueComments(string owner, string name)
        {
            return "repos/{0}/{1}/issues/comments".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueComments(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/comments".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        /// <returns></returns>
        public static Uri IssueComment(string owner, string name, int id)
        {
            return "repos/{0}/{1}/issues/comments/{2}".FormatUri(owner, name, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri IssueCommentReactions(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/comments/{2}/reactions".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri IssueCommentReactions(long repositoryId, int number)
        {
            return "repositories/{0}/issues/comments/{1}/reactions".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri CommitComment(string owner, string name, int number)
        {
            return "repos/{0}/{1}/comments/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <returns></returns>
        public static Uri CommitComments(string owner, string name, string sha)
        {
            return "repos/{0}/{1}/commits/{2}/comments".FormatUri(owner, name, sha);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri CommitComments(string owner, string name)
        {
            return "repos/{0}/{1}/comments".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified commit comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri CommitCommentReactions(string owner, string name, int number)
        {
            return "repos/{0}/{1}/comments/{2}/reactions".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified commit comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri CommitCommentReactions(long repositoryId, int number)
        {
            return "repositories/{0}/comments/{1}/reactions".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the assignees to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Assignees(string owner, string name)
        {
            return "repos/{0}/{1}/assignees".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a 204 if the login belongs to an assignee of the repository.
        /// Otherwire returns a 404.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        public static Uri CheckAssignee(string owner, string name, string login)
        {
            return "repos/{0}/{1}/assignees/{2}".FormatUri(owner, name, login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to add and remove assignees for an issue.        
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueAssignees(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/assignees".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the members of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <returns></returns>
        public static Uri Members(string org)
        {
            return "orgs/{0}/members".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the members of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <param name="filter">The member filter, <see cref="OrganizationMembersFilter"/></param>
        /// <returns>The correct uri</returns>
        public static Uri Members(string org, OrganizationMembersFilter filter)
        {
            return "orgs/{0}/members?filter={1}".FormatUri(org, filter.ToParameter());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the members of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <param name="role">The role filter, <see cref="OrganizationMembersRole"/></param>
        /// <returns>The correct uri</returns>
        public static Uri Members(string org, OrganizationMembersRole role)
        {
            return "orgs/{0}/members?role={1}".FormatUri(org, role.ToParameter());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the members of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <param name="filter">The member filter, <see cref="OrganizationMembersFilter"/></param>
        /// <param name="role">The role filter, <see cref="OrganizationMembersRole"/></param>
        /// <returns>The correct uri</returns>
        public static Uri Members(string org, OrganizationMembersFilter filter, OrganizationMembersRole role)
        {
            return "orgs/{0}/members?filter={1}&role={2}".FormatUri(org, filter.ToParameter(), role.ToParameter());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the public members of the organization
        /// </summary>
        /// <param name="org">Organization</param>
        /// <returns></returns>
        public static Uri PublicMembers(string org)
        {
            return "orgs/{0}/public_members".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a 204 if requester is an organization member and
        /// the user is, publicly or privately a member of the organization.
        /// Returns a 404 if the requester is an organization member and the user is not a member or
        /// the requester is not an organization member and is inquiring about themselves.
        /// Returns a 302 if the requester is not an organization member.
        /// </summary>
        /// <param name="org">The organization being inquired about</param>
        /// <param name="name">The user being inquired about</param>
        /// <returns></returns>
        public static Uri CheckMember(string org, string name)
        {
            return "orgs/{0}/members/{1}".FormatUri(org, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns member of organization
        /// </summary>
        /// <param name="org">The organization being inquired about</param>
        /// <param name="user">The user being inquired about</param>
        /// <returns>The <see cref="Uri"/> that returns member of organization</returns>
        public static Uri OrganizationMember(string org, string user)
        {
            return "orgs/{0}/members/{1}".FormatUri(org, user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a 204 if the user is a public member of the 
        /// organization.
        /// Otherwise returns a 404.
        /// </summary>
        /// <param name="org">The organization being inquired about</param>
        /// <param name="name">The user being inquired about</param>
        /// <returns></returns>
        public static Uri CheckMemberPublic(string org, string name)
        {
            return "orgs/{0}/public_members/{1}".FormatUri(org, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a 204 if the user is publicizing, or concealing
        /// their membership in an organization.
        /// </summary>
        /// <param name="org">The organization to publicize, or conceal their membership of</param>
        /// <param name="name">The user publicizing, or concealing their membership of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationMembership(string org, string name)
        {
            return "orgs/{0}/public_members/{1}".FormatUri(org, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the organizations pending invitations
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationPendingInvititations(string org)
        {
            return "orgs/{0}/invitations".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the outside collaborators of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <returns></returns>
        public static Uri OutsideCollaborators(string org)
        {
            return "orgs/{0}/outside_collaborators".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the outside collaborators of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <param name="filter">The collaborator filter, <see cref="OrganizationMembersFilter"/></param>
        /// <returns>The correct uri</returns>
        public static Uri OutsideCollaborators(string org, OrganizationMembersFilter filter)
        {
            return "orgs/{0}/outside_collaborators?filter={1}".FormatUri(org, filter.ToParameter());
        }

        public static Uri OutsideCollaborator(string org, string user)
        {
            return "orgs/{0}/outside_collaborators/{1}".FormatUri(org, user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Events(string owner, string name)
        {
            return "repos/{0}/{1}/events".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssuesEvents(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/events".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri IssuesEvents(string owner, string name)
        {
            return "repos/{0}/{1}/issues/events".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified event.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The event id</param>
        /// <returns></returns>
        public static Uri IssuesEvent(string owner, string name, int id)
        {
            return "repos/{0}/{1}/issues/events/{2}".FormatUri(owner, name, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified milestone.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        public static Uri Milestone(string owner, string name, int number)
        {
            return "repos/{0}/{1}/milestones/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the milestones for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Milestones(string owner, string name)
        {
            return "repos/{0}/{1}/milestones".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified label.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="labelName">The name of label</param>
        /// <returns></returns>
        public static Uri Label(string owner, string name, string labelName)
        {
            return "repos/{0}/{1}/labels/{2}".FormatUri(owner, name, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Labels(string owner, string name)
        {
            return "repos/{0}/{1}/labels".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the named label for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="labelName">The name of the label</param>
        /// <returns></returns>
        public static Uri IssueLabel(string owner, string name, int number, string labelName)
        {
            return "repos/{0}/{1}/issues/{2}/labels/{3}".FormatUri(owner, name, number, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueLabels(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/{2}/labels".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        public static Uri MilestoneLabels(string owner, string name, int number)
        {
            return "repos/{0}/{1}/milestones/{2}/labels".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to use when creating a commit status for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public static Uri CreateCommitStatus(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/statuses/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the repository hooks for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryHooks(string owner, string name)
        {
            return "repos/{0}/{1}/hooks".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the repository hook for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns></returns>
        public static Uri RepositoryHookById(string owner, string name, int hookId)
        {
            return "repos/{0}/{1}/hooks/{2}".FormatUri(owner, name, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that can tests a specified repository hook
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns></returns>
        public static Uri RepositoryHookTest(string owner, string name, int hookId)
        {
            return "repos/{0}/{1}/hooks/{2}/tests".FormatUri(owner, name, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that can ping a specified repository hook
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns></returns>
        public static Uri RepositoryHookPing(string owner, string name, int hookId)
        {
            return "repos/{0}/{1}/hooks/{2}/pings".FormatUri(owner, name, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the commit statuses for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public static Uri CommitStatuses(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/statuses".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a combined view of commit statuses for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public static Uri CombinedCommitStatus(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/status".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the repository forks for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryForks(string owner, string name)
        {
            return "repos/{0}/{1}/forks".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the watched repositories for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the watched repositories for the authenticated user.</returns>
        public static Uri Watchers(string owner, string name)
        {
            return "repos/{0}/{1}/subscribers".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the watched repositories for the authenticated user.
        /// </summary>
        public static Uri Watched()
        {
            return _currentUserWatched;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the watched repositories for the specified user.
        /// </summary>
        /// <param name="user">The user that has the watches</param>
        public static Uri WatchedByUser(string user)
        {
            return "users/{0}/subscriptions".FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that shows whether the repo is starred by the current user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Watched(string owner, string name)
        {
            return "repos/{0}/{1}/subscription".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the starred repositories for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the starred repositories for the authenticated user.</returns>
        public static Uri Stargazers(string owner, string name)
        {
            return "repos/{0}/{1}/stargazers".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the starred repositories for the authenticated user.
        /// </summary>
        public static Uri Starred()
        {
            return _currentUserStars;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the starred repositories for the specified user.
        /// </summary>
        /// <param name="user">The user that has the stars</param>
        public static Uri StarredByUser(string user)
        {
            return "users/{0}/starred".FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that shows whether the repo is starred by the current user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Starred(string owner, string name)
        {
            return "user/starred/{0}/{1}".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tag.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The tag reference (SHA)</param>
        /// <returns></returns>
        public static Uri Tag(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/git/tags/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a tag object.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri CreateTag(string owner, string name)
        {
            return "repos/{0}/{1}/git/tags".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the list of public events.
        /// </summary>
        /// <returns></returns>
        public static Uri Events()
        {
            return "events".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the feeds available to the authenticating user.
        /// </summary>
        /// <returns></returns>
        public static Uri Feeds()
        {
            return "feeds".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the list of public gists.
        /// </summary>
        public static Uri Gist()
        {
            return "gists".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified gist.
        /// </summary>
        /// <param name="id">The id of the gist</param>
        public static Uri Gist(string id)
        {
            return "gists/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the forks for the specified gist.
        /// </summary>
        /// <param name="id">The id of the gist</param>
        public static Uri ForkGist(string id)
        {
            return "gists/{0}/forks".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for all public gists.
        /// </summary>
        public static Uri PublicGists()
        {
            return "gists/public".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for all started public gists.
        /// </summary>
        public static Uri StarredGists()
        {
            return "gists/starred".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for all gists for a given user.
        /// </summary>
        /// <param name="user">The user to search for</param>
        public static Uri UsersGists(string user)
        {
            return "users/{0}/gists".FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to star a given gist.
        /// </summary>
        /// <param name="id">The id of the gist</param>
        public static Uri StarGist(string id)
        {
            return "gists/{0}/star".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments for the specified gist.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        public static Uri GistComments(string gistId)
        {
            return "gists/{0}/comments".FormatUri(gistId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the commits for the specified gist.
        /// </summary>
        /// <param name="id">The id of the gist</param>
        public static Uri GistCommits(string id)
        {
            return "gists/{0}/commits".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns></returns>
        public static Uri PullRequest(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the pull requests for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri PullRequests(string owner, string name)
        {
            return "repos/{0}/{1}/pulls".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the pull request merge state.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the pull request merge state.</returns>
        public static Uri MergePullRequest(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/merge".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the commits on a pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the commits on a pull request.</returns>
        public static Uri PullRequestCommits(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/commits".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the files on a pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the files on a pull request.</returns>
        public static Uri PullRequestFiles(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/files".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a spesific comment for the specified commit.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        public static Uri GistComment(string gistId, int commentId)
        {
            return "gists/{0}/comments/{1}".FormatUri(gistId, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri Commit(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/git/commits/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Reference(string owner, string name)
        {
            return "repos/{0}/{1}/git/refs".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="referenceName">The reference name</param>
        /// <returns></returns>
        public static Uri Reference(string owner, string name, string referenceName)
        {
            return "repos/{0}/{1}/git/refs/{2}".FormatUri(owner, name, referenceName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a commit object.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri CreateCommit(string owner, string name)
        {
            return "repos/{0}/{1}/git/commits".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a merge object.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri CreateMerge(string owner, string name)
        {
            return "repos/{0}/{1}/merges".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the network of repositories.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for the network of repositories.</returns>
        public static Uri NetworkEvents(string owner, string name)
        {
            return "networks/{0}/{1}/events".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationEvents(string organization)
        {
            return "orgs/{0}/events".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the received events for a user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns></returns>
        public static Uri ReceivedEvents(string user)
        {
            return ReceivedEvents(user, false);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the received events for a user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="isPublic">Whether to return public events or not</param>
        /// <returns></returns>
        public static Uri ReceivedEvents(string user, bool isPublic)
        {
            string usersReceivedEvents = "users/{0}/received_events";
            if (isPublic)
            {
                usersReceivedEvents += "/public";
            }
            return usersReceivedEvents.FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for events performed by a user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns></returns>
        public static Uri PerformedEvents(string user)
        {
            return PerformedEvents(user, false);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for events performed by a user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="isPublic">Whether to return public events or not</param>
        /// <returns></returns>
        public static Uri PerformedEvents(string user, bool isPublic)
        {
            string usersEvents = "users/{0}/events";
            if (isPublic)
            {
                usersEvents += "/public";
            }
            return usersEvents.FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for events associated with an organization.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationEvents(string user, string organization)
        {
            return "users/{0}/events/orgs/{1}".FormatUri(user, organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified pull request review.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComments(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/comments".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reviews opf a specified pull request
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviews(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComment(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/comments/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReview(string owner, string name, int number, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}".FormatUri(owner, name, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dismissing a specified pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewDismissal(long repositoryId, int number, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/dismissals".FormatUri(repositoryId, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dismissing a specified pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewDismissal(string owner, string name, int number, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/dismissals".FormatUri(owner, name, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewSubmit(long repositoryId, int number, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/events".FormatUri(repositoryId, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewSubmit(string owner, string name, int number, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/events".FormatUri(owner, name, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComments(long repositoryId, int number, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/comments".FormatUri(repositoryId, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComments(string owner, string name, int number, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/comments".FormatUri(owner, name, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specified pull request review.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReview(long repositoryId, int number, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}".FormatUri(repositoryId, number, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified pull request review comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReaction(string owner, string name, int number)
        {
            return "repos/{0}/{1}/pulls/comments/{2}/reactions".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified pull request review comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReaction(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/comments/{1}/reactions".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the pull request review comments on a specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewCommentsRepository(string owner, string name)
        {
            return "repos/{0}/{1}/pulls/comments".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific blob.
        /// </summary>
        /// <param name="owner">The owner of the blob</param>
        /// <param name="name">The name of the organization</param>
        /// <returns></returns>
        public static Uri Blobs(string owner, string name)
        {
            return Blob(owner, name, "");
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific blob.
        /// </summary>
        /// <param name="owner">The owner of the blob</param>
        /// <param name="name">The name of the organization</param>
        /// <param name="reference">The SHA of the blob</param>
        /// <returns></returns>
        public static Uri Blob(string owner, string name, string reference)
        {
            string blob = "repos/{0}/{1}/git/blobs";
            if (!string.IsNullOrEmpty(reference))
            {
                blob += "/{2}";
            }
            return blob.FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Tree(string owner, string name)
        {
            return "repos/{0}/{1}/git/trees".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The tree reference (SHA)</param>
        /// <returns></returns>
        public static Uri Tree(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/git/trees/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The tree reference (SHA)</param>
        /// <returns></returns>
        public static Uri TreeRecursive(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/git/trees/{2}?recursive=1".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for org teams 
        /// use for both Get and Create methods
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static Uri OrganizationTeams(string organization)
        {
            return "orgs/{0}/teams".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to discover teams 
        /// for the current user
        /// </summary>
        /// <returns></returns>
        public static Uri UserTeams()
        {
            return "user/teams".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for child teams
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Uri TeamChildTeams(int id)
        {
            return "teams/{0}/teams".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for teams
        /// use for getting, updating, or deleting a <see cref="Team"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Team"/>.</param>
        /// <returns></returns>
        public static Uri Teams(int id)
        {
            return "teams/{0}".FormatUri(id);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for team member
        /// </summary>
        /// <param name="id">The team id</param>
        /// <param name="login">The user login.</param>
        public static Uri TeamMember(int id, string login)
        {
            return "teams/{0}/memberships/{1}".FormatUri(id, login);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for team members list
        /// </summary>
        /// <param name="id">The team id</param>
        public static Uri TeamMembers(int id)
        {
            return "teams/{0}/members".FormatUri(id);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for the repositories
        /// </summary>
        /// <param name="id">The team id</param>
        public static Uri TeamRepositories(int id)
        {
            return "teams/{0}/repos".FormatUri(id);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for a team repository
        /// </summary>
        /// <param name="id">The team id</param>
        /// <param name="organization">The organization id</param>
        /// <param name="repoName">The repository name</param>
        public static Uri TeamRepository(int id, string organization, string repoName)
        {
            return "teams/{0}/repos/{1}/{2}".FormatUri(id, organization, repoName);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for the teams pending invitations
        /// </summary>
        /// <param name="id">The team id</param>
        /// <returns></returns>
        public static Uri TeamPendingInvitations(int id)
        {
            return "teams/{0}/invitations".FormatUri(id);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for teams
        /// use for update or deleting a team
        /// </summary>
        /// <param name="owner">owner of repo</param>
        /// <param name="name">name of repo</param>
        /// <returns></returns>
        public static Uri RepoCollaborators(string owner, string name)
        {
            return "repos/{0}/{1}/collaborators".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to check user is collaborator
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="user">The name of user</param>
        /// <returns>The <see cref="Uri"/> to check user is collaborator</returns>
        public static Uri RepoCollaborator(string owner, string repo, string user)
        {
            return "repos/{0}/{1}/collaborators/{2}".FormatUri(owner, repo, user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to check user is collaborator
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">The name of the user</param>
        /// <returns>The <see cref="Uri"/> to check user is collaborator</returns>
        public static Uri RepoCollaborator(long repositoryId, string user)
        {
            return "repositories/{0}/collaborators/{1}".FormatUri(repositoryId, user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to review the collaborators permission
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <param name="user">The name of the user</param>
        /// <returns>The <see cref="Uri"/> to review the collaborators permission</returns>
        public static Uri RepoCollaboratorPermission(string owner, string repo, string user)
        {
            return "repos/{0}/{1}/collaborators/{2}/permission".FormatUri(owner, repo, user);
        }

        public static Uri RepoCollaboratorPermission(long repositoryId, string user)
        {
            return "repositories/{0}/collaborators/{1}/permission".FormatUri(repositoryId, user);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for branches
        /// </summary>
        /// <param name="owner">owner of repo</param>
        /// <param name="name">name of repo</param>
        /// <returns></returns>
        public static Uri RepoBranches(string owner, string name)
        {
            return "repos/{0}/{1}/branches".FormatUri(owner, name);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for searching repositories
        /// </summary>
        /// <returns></returns>
        public static Uri SearchRepositories()
        {
            return "search/repositories".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for searching users
        /// </summary>
        /// <returns></returns>
        public static Uri SearchUsers()
        {
            return "search/users".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for searching issues
        /// </summary>
        /// <returns></returns>
        public static Uri SearchIssues()
        {
            return "search/issues".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for searching code
        /// </summary>
        /// <returns></returns>
        public static Uri SearchCode()
        {
            return "search/code".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository contributors.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryContributors(string owner, string name)
        {
            return "repos/{0}/{1}/contributors".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository languages.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryLanguages(string owner, string name)
        {
            return "repos/{0}/{1}/languages".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository teams.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryTeams(string owner, string name)
        {
            return "repos/{0}/{1}/teams".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository tags.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryTags(string owner, string name)
        {
            return "repos/{0}/{1}/tags".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository commits.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri RepositoryCommit(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/commits/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository commits.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryCommits(string owner, string name)
        {
            return "repos/{0}/{1}/commits".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for comparing two commits.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The base commit</param>
        /// <param name="head">The head commit</param>
        /// <returns></returns>
        public static Uri RepoCompare(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            var encodedBase = @base.UriEncode();
            var encodedHead = head.UriEncode();
            return "repos/{0}/{1}/compare/{2}...{3}".FormatUri(owner, name, encodedBase, encodedHead);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoBranch(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository branches protection.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoBranchProtection(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository branches protection.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoBranchProtection(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required status checks for a protected branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRequiredStatusChecks(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/required_status_checks".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required status checks for a protected branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRequiredStatusChecks(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/required_status_checks".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required status checks for a protected branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRequiredStatusChecksContexts(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/required_status_checks/contexts".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required status checks for a protected branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRequiredStatusChecksContexts(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/required_status_checks/contexts".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required_pull_request_reviews for a protected branch
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        public static Uri RepoProtectedBranchReviewEnforcement(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/required_pull_request_reviews".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for required_pull_request_reviews for a protected branch
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        public static Uri RepoProtectedBranchReviewEnforcement(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/required_pull_request_reviews".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for admin enforcement for a protected branch
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        public static Uri RepoProtectedBranchAdminEnforcement(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/enforce_admins".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for admin enforcement for a protected branch
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        public static Uri RepoProtectedBranchAdminEnforcement(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/enforce_admins".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for restrictions for a protected branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictions(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/restrictions".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for restrictions for a protected branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictions(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/restrictions".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for team restrictions for a protected branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictionsTeams(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/restrictions/teams".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for team restrictions for a protected branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictionsTeams(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/restrictions/teams".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for user restrictions for a protected branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictionsUsers(string owner, string name, string branchName)
        {
            return "repos/{0}/{1}/branches/{2}/protection/restrictions/users".FormatUri(owner, name, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for user restrictions for a protected branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        public static Uri RepoRestrictionsUsers(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}/protection/restrictions/users".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri Repository(string owner, string name)
        {
            return "repos/{0}/{1}".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a deploy key for a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The id of the deploy key of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryDeployKey(string owner, string name, int number)
        {
            return "repos/{0}/{1}/keys/{2}".FormatUri(owner, name, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for deploy keys for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryDeployKeys(string owner, string name)
        {
            return "repos/{0}/{1}/keys".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the Deployments API for the given repository.
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the Deployments API for the given repository.</returns>
        public static Uri Deployments(string owner, string name)
        {
            return "repos/{0}/{1}/deployments".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the Deployment Statuses API for the given deployment.
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <param name="deploymentId">Id of the deployment</param>
        /// <returns></returns>
        public static Uri DeploymentStatuses(string owner, string name, int deploymentId)
        {
            return "repos/{0}/{1}/deployments/{2}/statuses".FormatUri(owner, name, deploymentId);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for retrieving the 
        /// current users followers
        /// </summary>
        /// <returns>The <see cref="Uri"/> for retrieving the current users followers</returns>
        public static Uri Followers()
        {
            return "user/followers".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for retrieving
        /// the followers for the specified user
        /// </summary>
        /// <param name="login">name of the user</param>
        /// <returns>The <see cref="Uri"/> for retrieving the specified users followers</returns>
        public static Uri Followers(string login)
        {
            return "users/{0}/followers".FormatUri(login);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for retrieving the users the current user follows
        /// </summary>
        /// <returns>The <see cref="Uri"/> for retrieving the users the current user follows</returns>
        public static Uri Following()
        {
            return "user/following".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for retrieving the users the specified user follows
        /// </summary>
        /// <param name="login">name of the user</param>
        /// <returns>The <see cref="Uri"/> for retrieving the users the specified user follows</returns>
        public static Uri Following(string login)
        {
            return "users/{0}/following".FormatUri(login);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for checking is the current user is following
        /// another user
        /// </summary>
        /// <param name="following">name of the user followed</param>
        /// <returns>The <see cref="Uri"/> for checking if the current user follows the specified user.</returns>
        public static Uri IsFollowing(string following)
        {
            return "user/following/{0}".FormatUri(following);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for checking if a user is following another user
        /// </summary>
        /// <param name="login">name of the user following</param>
        /// <param name="following">name of the user followed</param>
        /// <returns>The <see cref="Uri"/> for checking if the specified user follows another user</returns>
        public static Uri IsFollowing(string login, string following)
        {
            return "users/{0}/following/{1}".FormatUri(login, following);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the user for the given login
        /// </summary>
        /// <param name="login">Name of the user</param>
        /// <returns>The <see cref="System.Uri"/> for the user for the given login</returns>
        public static Uri User(string login)
        {
            return "users/{0}".FormatUri(login);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for initiating the OAuth Web login Flow
        /// </summary>
        /// <returns></returns>
        public static Uri OauthAuthorize()
        {
            return "login/oauth/authorize".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> to request an OAuth access token.
        /// </summary>
        /// <returns></returns>
        public static Uri OauthAccessToken()
        {
            return "login/oauth/access_token".FormatUri();
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the README of the specified repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the README of the specified repository</returns>
        public static Uri RepositoryReadme(string owner, string name)
        {
            return "repos/{0}/{1}/readme".FormatUri(owner, name);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the contents of the specified repository's root
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository's root</returns>
        public static Uri RepositoryContent(string owner, string name)
        {
            return "repos/{0}/{1}/contents".FormatUri(owner, name);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the contents of the specified repository and path
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path of the contents to get</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(string owner, string name, string path)
        {
            return "repos/{0}/{1}/contents/{2}".FormatUri(owner, name, path);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns>The <see cref="Uri"/> for getting an archive of a given repository's contents, in a specific format</returns>
        public static Uri RepositoryArchiveLink(string owner, string name, ArchiveFormat archiveFormat, string reference)
        {
            return "repos/{0}/{1}/{2}/{3}".FormatUri(owner, name, archiveFormat.ToParameter(), reference);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the contents of the specified repository and path
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path of the contents to get</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(string owner, string name, string path, string reference)
        {
            return "repos/{0}/{1}/contents/{2}?ref={3}".FormatUri(owner, name, path, reference);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the page metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the page metadata for a given repository</returns>
        public static Uri RepositoryPage(string owner, string name)
        {
            return "repos/{0}/{1}/pages".FormatUri(owner, name);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting all build metadata for a given repository</returns>
        public static Uri RepositoryPageBuilds(string owner, string name)
        {
            return "repos/{0}/{1}/pages/builds".FormatUri(owner, name);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for getting the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the build metadata for the last build for a given repository</returns>
        public static Uri RepositoryPageBuildsLatest(string owner, string name)
        {
            return "repos/{0}/{1}/pages/builds/latest".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the contributors for the given repository
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the contributors for the given repository</returns>
        public static Uri StatsContributors(string owner, string name)
        {
            return "repos/{0}/{1}/stats/contributors".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the commit activity for the given repository
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the commit activity for the given repository</returns>
        public static Uri StatsCommitActivity(string owner, string name)
        {
            return "repos/{0}/{1}/stats/commit_activity".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the code frequency for the given repository
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the code frequency for the given repository</returns>
        public static Uri StatsCodeFrequency(string owner, string name)
        {
            return "repos/{0}/{1}/stats/code_frequency".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the participation for the given repository
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the participation for the given repository</returns>
        public static Uri StatsParticipation(string owner, string name)
        {
            return "repos/{0}/{1}/stats/participation".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the punch card for the given repository
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for the punch card for the given repository</returns>
        public static Uri StatsPunchCard(string owner, string name)
        {
            return "repos/{0}/{1}/stats/punch_card".FormatUri(owner, name);
        }

        private static Uri EnterpriseAdminStats(string type)
        {
            return "enterprise/stats/{0}".FormatUri(type);
        }

        public static Uri EnterpriseAdminStatsIssues()
        {
            return EnterpriseAdminStats("issues");
        }

        public static Uri EnterpriseAdminStatsHooks()
        {
            return EnterpriseAdminStats("hooks");
        }

        public static Uri EnterpriseAdminStatsMilestones()
        {
            return EnterpriseAdminStats("milestones");
        }

        public static Uri EnterpriseAdminStatsOrgs()
        {
            return EnterpriseAdminStats("orgs");
        }

        public static Uri EnterpriseAdminStatsComments()
        {
            return EnterpriseAdminStats("comments");
        }

        public static Uri EnterpriseAdminStatsPages()
        {
            return EnterpriseAdminStats("pages");
        }

        public static Uri EnterpriseAdminStatsUsers()
        {
            return EnterpriseAdminStats("users");
        }

        public static Uri EnterpriseAdminStatsGists()
        {
            return EnterpriseAdminStats("gists");
        }

        public static Uri EnterpriseAdminStatsPulls()
        {
            return EnterpriseAdminStats("pulls");
        }

        public static Uri EnterpriseAdminStatsRepos()
        {
            return EnterpriseAdminStats("repos");
        }

        public static Uri EnterpriseAdminStatsAll()
        {
            return EnterpriseAdminStats("all");
        }

        public static Uri EnterpriseLdapTeamMapping(int teamId)
        {
            return "admin/ldap/teams/{0}/mapping".FormatUri(teamId);
        }

        public static Uri EnterpriseLdapTeamSync(int teamId)
        {
            return "admin/ldap/teams/{0}/sync".FormatUri(teamId);
        }

        public static Uri EnterpriseLdapUserMapping(string userName)
        {
            return "admin/ldap/users/{0}/mapping".FormatUri(userName);
        }

        public static Uri EnterpriseLdapUserSync(string userName)
        {
            return "admin/ldap/users/{0}/sync".FormatUri(userName);
        }

        public static Uri EnterpriseLicense()
        {
            return "enterprise/settings/license".FormatUri();
        }

        public static Uri EnterpriseMigrationById(string org, int id)
        {
            return "orgs/{0}/migrations/{1}".FormatUri(org, id);
        }

        public static Uri EnterpriseMigrations(string org)
        {
            return "orgs/{0}/migrations".FormatUri(org);
        }

        public static Uri EnterpriseMigrationArchive(string org, int id)
        {
            return "orgs/{0}/migrations/{1}/archive".FormatUri(org, id);
        }

        public static Uri EnterpriseMigrationUnlockRepository(string org, int id, string repo)
        {
            return "orgs/{0}/migrations/{1}/repos/{2}/lock".FormatUri(org, id, repo);
        }

        public static Uri EnterpriseOrganization()
        {
            return "admin/organizations".FormatUri();
        }

        public static Uri EnterpriseSearchIndexing()
        {
            return "staff/indexing_jobs".FormatUri();
        }

        public static Uri UserAdministration()
        {
            return "admin/users".FormatUri();
        }

        public static Uri UserAdministration(string login)
        {
            return "admin/users/{0}".FormatUri(login);
        }

        public static Uri UserAdministrationAuthorization(string login)
        {
            return "admin/users/{0}/authorizations".FormatUri(login);
        }

        public static Uri UserAdministrationPublicKeys()
        {
            return "admin/keys".FormatUri();
        }

        public static Uri UserAdministrationPublicKeys(int keyId)
        {
            return "admin/keys/{0}".FormatUri(keyId);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for altering administration status of a user.
        /// </summary>
        /// <param name="login">The login for the intended user.</param>
        /// <returns></returns>
        public static Uri UserAdministrationSiteAdmin(string login)
        {
            return "users/{0}/site_admin".FormatUri(login);
        }

        /// <summary>
        /// Creates the relative <see cref="Uri"/> for altering suspension status of a user.
        /// </summary>
        /// <param name="login">The login for the intended user.</param>
        /// <returns></returns>
        public static Uri UserAdministrationSuspension(string login)
        {
            return "users/{0}/suspended".FormatUri(login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the assets specified by the asset id.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release asset</param>
        /// <returns>The <see cref="Uri"/> that returns the assets specified by the asset id.</returns>
        public static Uri Asset(long repositoryId, int id)
        {
            return "repositories/{0}/releases/assets/{1}".FormatUri(repositoryId, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the assignees to which issues may be assigned.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the assignees to which issues may be assigned.</returns>
        public static Uri Assignees(long repositoryId)
        {
            return "repositories/{0}/assignees".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific blob.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for a specific blob.</returns>
        public static Uri Blobs(long repositoryId)
        {
            return Blob(repositoryId, "");
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific blob.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA of the blob</param>
        /// <returns>The <see cref="Uri"/> for a specific blob.</returns>
        public static Uri Blob(long repositoryId, string reference)
        {
            string blob = "repositories/{0}/git/blobs";
            if (!string.IsNullOrEmpty(reference))
            {
                blob += "/{1}";
            }
            return blob.FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a 204 if the login belongs to an assignee of the repository. Otherwire returns a 404.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="login">The login for the user</param>
        /// <returns>The <see cref="Uri"/> that returns a 204 if the login belongs to an assignee of the repository. Otherwire returns a 404.</returns>
        public static Uri CheckAssignee(long repositoryId, string login)
        {
            return "repositories/{0}/assignees/{1}".FormatUri(repositoryId, login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a combined view of commit statuses for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns>The <see cref="Uri"/> that returns a combined view of commit statuses for the specified reference.</returns>
        public static Uri CombinedCommitStatus(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/status".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns>The <see cref="Uri"/> for the specified commit.</returns>
        public static Uri Commit(long repositoryId, string reference)
        {
            return "repositories/{0}/git/commits/{1}".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns>The <see cref="Uri"/> for the specified comment.</returns>
        public static Uri CommitComment(long repositoryId, int number)
        {
            return "repositories/{0}/comments/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <returns>The <see cref="Uri"/> for the comments of a specified commit.</returns>
        public static Uri CommitComments(long repositoryId, string sha)
        {
            return "repositories/{0}/commits/{1}/comments".FormatUri(repositoryId, sha);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the comments of a specified commit.</returns>
        public static Uri CommitComments(long repositoryId)
        {
            return "repositories/{0}/comments".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the commit statuses for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns>The <see cref="Uri"/> that lists the commit statuses for the specified reference.</returns>
        public static Uri CommitStatuses(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/statuses".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a commit object.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for creating a commit object.</returns>
        public static Uri CreateCommit(long repositoryId)
        {
            return "repositories/{0}/git/commits".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to use when creating a commit status for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns>The <see cref="Uri"/> to use when creating a commit status for the specified reference.</returns>
        public static Uri CreateCommitStatus(long repositoryId, string reference)
        {
            return "repositories/{0}/statuses/{1}".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a merge object.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for creating a merge object.</returns>
        public static Uri CreateMerge(long repositoryId)
        {
            return "repositories/{0}/merges".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a tag object.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for creating a tag object.</returns>
        public static Uri CreateTag(long repositoryId)
        {
            return "repositories/{0}/git/tags".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Deployments API for the given repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the Deployments API for the given repository.</returns>
        public static Uri Deployments(long repositoryId)
        {
            return "repositories/{0}/deployments".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Deployment Statuses API for the given deployment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="deploymentId">Id of the deployment</param>
        /// <returns>The <see cref="Uri"/> for the Deployment Statuses API for the given deployment.</returns>
        public static Uri DeploymentStatuses(long repositoryId, int deploymentId)
        {
            return "repositories/{0}/deployments/{1}/statuses".FormatUri(repositoryId, deploymentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.</returns>
        public static Uri Events(long repositoryId)
        {
            return "repositories/{0}/events".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the GPG Keys for the authenticated user.
        /// </summary>
        /// <returns>The <see cref="Uri"/> that returns all the GPG Keys for the authenticated user.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        public static Uri GpgKeys()
        {
            return _currentUserGpgKeys;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the GPG Key for the authenticated user for the specified Id.
        /// </summary>
        /// <param name="id">The <see cref="Uri"/> that returns the GPG Key for the authenticated user for the specified Id.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        public static Uri GpgKeys(int id)
        {
            return "user/gpg_keys/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the specified issue.</returns>
        public static Uri Issue(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The comment id</param>
        /// <returns>The <see cref="Uri"/> for the specified comment.</returns>
        public static Uri IssueComment(long repositoryId, int id)
        {
            return "repositories/{0}/issues/comments/{1}".FormatUri(repositoryId, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments for all issues in a specific repo.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the comments for all issues in a specific repo.</returns>
        public static Uri IssueComments(long repositoryId)
        {
            return "repositories/{0}/issues/comments".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the comments of a specified issue.</returns>
        public static Uri IssueComments(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/comments".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the named label for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="labelName">The name of the label</param>
        /// <returns>The <see cref="Uri"/> that returns the named label for the specified issue.</returns>
        public static Uri IssueLabel(long repositoryId, int number, string labelName)
        {
            return "repositories/{0}/issues/{1}/labels/{2}".FormatUri(repositoryId, number, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The <see cref="Uri"/> that returns all of the labels for the specified issue.</returns>
        public static Uri IssueLabels(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/labels".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue to be locked/unlocked.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the specified issue to be locked/unlocked.</returns>
        public static Uri IssueLock(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/lock".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the issues for the currently logged in user specific to the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the issues for the currently logged in user specific to the repository.</returns>
        public static Uri Issues(long repositoryId)
        {
            return "repositories/{0}/issues".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified event.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The event id</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified event.</returns>
        public static Uri IssuesEvent(long repositoryId, int id)
        {
            return "repositories/{0}/issues/events/{1}".FormatUri(repositoryId, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.</returns>
        public static Uri IssuesEvents(long repositoryId, int number)
        {
            return "repositories/{0}/issues/{1}/events".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified repository.</returns>
        public static Uri IssuesEvents(long repositoryId)
        {
            return "repositories/{0}/issues/events".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified label.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of label</param>
        /// <returns>The <see cref="Uri"/> that returns the specified label.</returns>
        public static Uri Label(long repositoryId, string labelName)
        {
            return "repositories/{0}/labels/{1}".FormatUri(repositoryId, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the labels for the specified repository.</returns>
        public static Uri Labels(long repositoryId)
        {
            return "repositories/{0}/labels".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the latest release for the specified repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns the latest release for the specified repository</returns>
        public static Uri LatestRelease(long repositoryId)
        {
            return "repositories/{0}/releases/latest".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the pull request merge state.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the pull request merge state.</returns>
        public static Uri MergePullRequest(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/merge".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified milestone.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns>The <see cref="Uri"/> that returns the specified milestone.</returns>
        public static Uri Milestone(long repositoryId, int number)
        {
            return "repositories/{0}/milestones/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns>The <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.</returns>
        public static Uri MilestoneLabels(long repositoryId, int number)
        {
            return "repositories/{0}/milestones/{1}/labels".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the milestones for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the milestones for the specified repository.</returns>
        public static Uri Milestones(long repositoryId)
        {
            return "repositories/{0}/milestones".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the notifications for the currently logged in user specific to the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the notifications for the currently logged in user specific to the repository.</returns>
        public static Uri Notifications(long repositoryId)
        {
            return "repositories/{0}/notifications".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified pull request.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the specified pull request.</returns>
        public static Uri PullRequest(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the commits on a pull request.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the commits on a pull request.</returns>
        public static Uri PullRequestCommits(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/commits".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the files on a pull request.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the files on a pull request.</returns>
        public static Uri PullRequestFiles(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/files".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComment(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/comments/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified pull request review.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComments(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/comments".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reviews of a specified pull request
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviews(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/reviews".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the pull request review comments on a specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewCommentsRepository(long repositoryId)
        {
            return "repositories/{0}/pulls/comments".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the pull requests for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the pull requests for a repository.</returns>
        public static Uri PullRequests(long repositoryId)
        {
            return "repositories/{0}/pulls".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the specified reference.</returns>
        public static Uri Reference(long repositoryId)
        {
            return "repositories/{0}/git/refs".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="referenceName">The reference name</param>
        /// <returns>The <see cref="Uri"/> for the specified reference.</returns>
        public static Uri Reference(long repositoryId, string referenceName)
        {
            return "repositories/{0}/git/refs/{1}".FormatUri(repositoryId, referenceName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the assets for the specified release for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <returns>The <see cref="Uri"/> that returns all the assets for the specified release for the specified repository.</returns>
        public static Uri ReleaseAssets(long repositoryId, int id)
        {
            return "repositories/{0}/releases/{1}/assets".FormatUri(repositoryId, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the releases for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the releases for the specified repository.</returns>
        public static Uri Releases(long repositoryId)
        {
            return "repositories/{0}/releases".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <returns>The <see cref="Uri"/> that returns a single release for the specified repository</returns>
        public static Uri Releases(long repositoryId, int id)
        {
            return "repositories/{0}/releases/{1}".FormatUri(repositoryId, id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns>The <see cref="Uri"/> for a repository branch.</returns>
        public static Uri RepoBranch(long repositoryId, string branchName)
        {
            return "repositories/{0}/branches/{1}".FormatUri(repositoryId, branchName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the branches for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the branches for the specified repository.</returns>
        public static Uri RepoBranches(long repositoryId)
        {
            return "repositories/{0}/branches".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the collaborators for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that returns all of the collaborators for the specified repository.</returns>
        public static Uri RepoCollaborators(long repositoryId)
        {
            return "repositories/{0}/collaborators".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for comparing two commits.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The base commit</param>
        /// <param name="head">The head commit</param>
        /// <returns>The <see cref="Uri"/> for comparing two commits.</returns>
        public static Uri RepoCompare(long repositoryId, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");
            var encodedBase = @base.UriEncode();
            var encodedHead = head.UriEncode();
            return "repositories/{0}/compare/{1}...{2}".FormatUri(repositoryId, encodedBase, encodedHead);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for a repository.</returns>
        public static Uri Repository(long repositoryId)
        {
            return "repositories/{0}".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns>The <see cref="Uri"/> for getting an archive of a given repository's contents, in a specific format</returns>
        public static Uri RepositoryArchiveLink(long repositoryId, ArchiveFormat archiveFormat, string reference)
        {
            return "repositories/{0}/{1}/{2}".FormatUri(repositoryId, archiveFormat.ToParameter(), reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository commits.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns>The <see cref="Uri"/> for repository commits.</returns>
        public static Uri RepositoryCommit(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository commits.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository commits.</returns>
        public static Uri RepositoryCommits(long repositoryId)
        {
            return "repositories/{0}/commits".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the contents of the specified repository's root
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository's root</returns>
        public static Uri RepositoryContent(long repositoryId)
        {
            return "repositories/{0}/contents".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the contents of the specified repository and path
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path of the contents to get</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(long repositoryId, string path)
        {
            return "repositories/{0}/contents/{1}".FormatUri(repositoryId, path);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the contents of the specified repository and path
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path of the contents to get</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(long repositoryId, string path, string reference)
        {
            return "repositories/{0}/contents/{1}?ref={2}".FormatUri(repositoryId, path, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository contributors.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository contributors.</returns>
        public static Uri RepositoryContributors(long repositoryId)
        {
            return "repositories/{0}/contributors".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a deploy key for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The id of the deploy key of the repository</param>
        /// <returns>The <see cref="Uri"/> for a deploy key for a repository</returns>
        public static Uri RepositoryDeployKey(long repositoryId, int number)
        {
            return "repositories/{0}/keys/{1}".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for deploy keys for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for deploy keys for a repository.</returns>
        public static Uri RepositoryDeployKeys(long repositoryId)
        {
            return "repositories/{0}/keys".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the repository forks for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the repository forks for the specified reference.</returns>
        public static Uri RepositoryForks(long repositoryId)
        {
            return "repositories/{0}/forks".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the repository hook for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns>The <see cref="Uri"/> that gets the repository hook for the specified reference.</returns>
        public static Uri RepositoryHookById(long repositoryId, int hookId)
        {
            return "repositories/{0}/hooks/{1}".FormatUri(repositoryId, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that can ping a specified repository hook
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns>The <see cref="Uri"/> that can ping a specified repository hook</returns>
        public static Uri RepositoryHookPing(long repositoryId, int hookId)
        {
            return "repositories/{0}/hooks/{1}/pings".FormatUri(repositoryId, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the repository hooks for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the repository hooks for the specified reference.</returns>
        public static Uri RepositoryHooks(long repositoryId)
        {
            return "repositories/{0}/hooks".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that can tests a specified repository hook
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The identifier of the repository hook</param>
        /// <returns>The <see cref="Uri"/> that can tests a specified repository hook</returns>
        public static Uri RepositoryHookTest(long repositoryId, int hookId)
        {
            return "repositories/{0}/hooks/{1}/tests".FormatUri(repositoryId, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository languages.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository languages.</returns>
        public static Uri RepositoryLanguages(long repositoryId)
        {
            return "repositories/{0}/languages".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the page metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the page metadata for a given repository</returns>
        public static Uri RepositoryPage(long repositoryId)
        {
            return "repositories/{0}/pages".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting all build metadata for a given repository</returns>
        public static Uri RepositoryPageBuilds(long repositoryId)
        {
            return "repositories/{0}/pages/builds".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the build metadata for the last build for a given repository</returns>
        public static Uri RepositoryPageBuildsLatest(long repositoryId)
        {
            return "repositories/{0}/pages/builds/latest".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting the README of the specified repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for getting the README of the specified repository</returns>
        public static Uri RepositoryReadme(long repositoryId)
        {
            return "repositories/{0}/readme".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository tags.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository tags.</returns>
        public static Uri RepositoryTags(long repositoryId)
        {
            return "repositories/{0}/tags".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository teams.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository teams.</returns>
        public static Uri RepositoryTeams(long repositoryId)
        {
            return "repositories/{0}/teams".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the starred repositories for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the starred repositories for the authenticated user.</returns>
        public static Uri Stargazers(long repositoryId)
        {
            return "repositories/{0}/stargazers".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the code frequency for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the code frequency for the given repository</returns>
        public static Uri StatsCodeFrequency(long repositoryId)
        {
            return "repositories/{0}/stats/code_frequency".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the commit activity for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the commit activity for the given repository</returns>
        public static Uri StatsCommitActivity(long repositoryId)
        {
            return "repositories/{0}/stats/commit_activity".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the contributors for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the contributors for the given repository</returns>
        public static Uri StatsContributors(long repositoryId)
        {
            return "repositories/{0}/stats/contributors".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the participation for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the participation for the given repository</returns>
        public static Uri StatsParticipation(long repositoryId)
        {
            return "repositories/{0}/stats/participation".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the punch card for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the punch card for the given repository</returns>
        public static Uri StatsPunchCard(long repositoryId)
        {
            return "repositories/{0}/stats/punch_card".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tag.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The tag reference (SHA)</param>
        /// <returns>The <see cref="Uri"/> for the specified tag.</returns>
        public static Uri Tag(long repositoryId, string reference)
        {
            return "repositories/{0}/git/tags/{1}".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the specified tree.</returns>
        public static Uri Tree(long repositoryId)
        {
            return "repositories/{0}/git/trees".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The tree reference (SHA)</param>
        /// <returns>The <see cref="Uri"/> for the specified tree.</returns>
        public static Uri Tree(long repositoryId, string reference)
        {
            return "repositories/{0}/git/trees/{1}".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified tree.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The tree reference (SHA)</param>
        /// <returns>The <see cref="Uri"/> for the specified tree.</returns>
        public static Uri TreeRecursive(long repositoryId, string reference)
        {
            return "repositories/{0}/git/trees/{1}?recursive=1".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that shows whether the repo is starred by the current user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that shows whether the repo is starred by the current user.</returns>
        public static Uri Watched(long repositoryId)
        {
            return "repositories/{0}/subscription".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the watched repositories for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that lists the watched repositories for the authenticated user.</returns>
        public static Uri Watchers(long repositoryId)
        {
            return "repositories/{0}/subscribers".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for deleting a reaction.
        /// </summary>
        /// <param name="number">The reaction number</param>
        /// <returns>The <see cref="Uri"/> that lists the watched repositories for the authenticated user.</returns>
        public static Uri Reactions(int number)
        {
            return "reactions/{0}".FormatUri(number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository invitations.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository invitations.</returns>
        public static Uri RepositoryInvitations(long repositoryId)
        {
            return "repositories/{0}/invitations".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a single repository invitation.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <returns>The <see cref="Uri"/> for repository invitations.</returns>
        public static Uri RepositoryInvitations(long repositoryId, int invitationId)
        {
            return "repositories/{0}/invitations/{1}".FormatUri(repositoryId, invitationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for invitations for the current user.
        /// </summary>
        /// <returns>The <see cref="Uri"/> for invitations for the current user.</returns>
        public static Uri UserInvitations()
        {
            return "user/repository_invitations".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a single invitation of the current user.
        /// </summary>
        /// <param name="invitationId">The id of the invitation</param>
        /// <returns>The <see cref="Uri"/> for invitations for the current user.</returns>
        public static Uri UserInvitations(int invitationId)
        {
            return "user/repository_invitations/{0}".FormatUri(invitationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice referrers.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic referrers.</returns>
        public static Uri RepositoryTrafficReferrers(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/popular/referrers".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice referrers.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic referrers.</returns>
        public static Uri RepositoryTrafficReferrers(long repositoryId)
        {
            return "repositories/{0}/traffic/popular/referrers".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice paths.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic paths.</returns>
        public static Uri RepositoryTrafficPaths(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/popular/paths".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice paths.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic paths.</returns>
        public static Uri RepositoryTrafficPaths(long repositoryId)
        {
            return "repositories/{0}/traffic/popular/paths".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice views.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic views.</returns>
        public static Uri RepositoryTrafficViews(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/views".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice views.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic views.</returns>
        public static Uri RepositoryTrafficViews(long repositoryId)
        {
            return "repositories/{0}/traffic/views".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice clones.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic clones.</returns>
        public static Uri RepositoryTrafficClones(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/clones".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffice clones.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic clones.</returns>
        public static Uri RepositoryTrafficClones(long repositoryId)
        {
            return "repositories/{0}/traffic/clones".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for pull request review requests.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> for pull request review requests.</returns>
        public static Uri PullRequestReviewRequests(string owner, string repo, int number)
        {
            return "repos/{0}/{1}/pulls/{2}/requested_reviewers".FormatUri(owner, repo, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for pull request review requests.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The <see cref="Uri"/> for pull request review requests.</returns>
        public static Uri PullRequestReviewRequests(long repositoryId, int number)
        {
            return "repositories/{0}/pulls/{1}/requested_reviewers".FormatUri(repositoryId, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified project projects.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> for projects.</returns>
        public static Uri RepositoryProjects(string owner, string repo)
        {
            return "repos/{0}/{1}/projects".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified project projects.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for projects.</returns>
        public static Uri RepositoryProjects(long repositoryId)
        {
            return "repositories/{0}/projects".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified organization projects.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns>The <see cref="Uri"/> for projects.</returns>
        public static Uri OrganizationProjects(string organization)
        {
            return "orgs/{0}/projects".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a project.
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <returns>The <see cref="Uri"/> for repository projects.</returns>
        public static Uri Project(int id)
        {
            return "projects/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for project columns.
        /// </summary>
        /// <param name="id">The id of the columns</param>
        /// <returns>The <see cref="Uri"/> for project columns.</returns>
        public static Uri ProjectColumn(int id)
        {
            return "projects/columns/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific project column.
        /// </summary>
        /// <param name="projectId">The id of the project</param>
        /// <returns>The <see cref="Uri"/> for a specific project column.</returns>
        public static Uri ProjectColumns(int projectId)
        {
            return "projects/{0}/columns".FormatUri(projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to move a project column.
        /// </summary>
        /// <param name="id">The id of the column to move</param>
        /// <returns>The <see cref="Uri"/> to move a project column.</returns>
        public static Uri ProjectColumnMove(int id)
        {
            return "projects/columns/{0}/moves".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for project cards.
        /// </summary>
        /// <param name="id">The id of the card</param>
        /// <returns>The <see cref="Uri"/> for project cards.</returns>
        public static Uri ProjectCard(int id)
        {
            return "projects/columns/cards/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for project cards.
        /// </summary>
        /// <param name="columnId">The id of the column</param>
        /// <returns>The <see cref="Uri"/> for project cards.</returns>
        public static Uri ProjectCards(int columnId)
        {
            return "projects/columns/{0}/cards".FormatUri(columnId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to move a project card.
        /// </summary>
        /// <param name="id">The id of the card to move</param>
        /// <returns>The <see cref="Uri"/> to move a project card.</returns>
        public static Uri ProjectCardMove(int id)
        {
            return "projects/columns/cards/{0}/moves".FormatUri(id);
        }
    }
}
