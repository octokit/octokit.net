using System;

namespace Octokit
{
    /// <summary>
    /// Class for retrieving GitHub ApI URLs
    /// </summary>
    public static class ApiUrls
    {
        static readonly Uri _currentUserRepositoriesUrl = new Uri("user/repos", UriKind.Relative);
        static readonly Uri _currentUserOrganizationsUrl = new Uri("user/orgs", UriKind.Relative);
        static readonly Uri _currentUserSshKeys = new Uri("user/keys", UriKind.Relative);
        static readonly Uri _currentUserStars = new Uri("user/starred", UriKind.Relative);
        static readonly Uri _currentUserEmailsEndpoint = new Uri("user/emails", UriKind.Relative);
        static readonly Uri _currentUserAuthorizationsEndpoint = new Uri("authorizations", UriKind.Relative);
        static readonly Uri _currentUserNotificationsEndpoint = new Uri("notifications", UriKind.Relative);
        static readonly Uri _currentUserAllIssues = new Uri("issues", UriKind.Relative);
        static readonly Uri _currentUserOwnedAndMemberIssues = new Uri("user/issues", UriKind.Relative);

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
        public static Uri Organizations()
        {
            return _currentUserOrganizationsUrl;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the organizations for the specified login.
        /// </summary>
        /// <param name="login">The login for the user</param>
        /// <returns></returns>
        public static Uri Organizations(string login)
        {
            return "users/{0}/orgs".FormatUri(login);
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
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <returns></returns>
        public static Uri Releases(string owner, string name)
        {
            return "repos/{0}/{1}/releases".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the authorizations for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri Authorizations()
        {
            return _currentUserAuthorizationsEndpoint;
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
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        public static Uri IssueComment(string owner, string name, int number)
        {
            return "repos/{0}/{1}/issues/comments/{2}".FormatUri(owner, name, number);
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
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// /// <param name="number">The issue number</param>
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
        /// /// <param name="number">The milestone number</param>
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
        /// Returns the <see cref="Uri"/> that lists the commit statuses for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for.</param>
        /// <returns></returns>
        public static Uri CommitStatus(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/statuses/{2}".FormatUri(owner, name, reference);
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
        public static Uri Starred(string user)
        {
            return "users/{0}/starred".FormatUri(user);
        }

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
    }
}
