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
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
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
        /// Returns the <see cref="Uri"/> that returns all of the members of the organization
        /// </summary>
        /// <param name="org">The organization</param>
        /// <returns></returns>
        public static Uri Members(string org)
        {
            return "orgs/{0}/members".FormatUri(org);
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
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The milestone number</param>
        /// <returns></returns>
        public static Uri Label(string owner, string repo, string name)
        {
            return "repos/{0}/{1}/labels/{2}".FormatUri(owner, repo, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns></returns>
        public static Uri Labels(string owner, string repo)
        {
            return "repos/{0}/{1}/labels".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="name">The name of the label</param>
        /// <returns></returns>
        public static Uri IssueLabel(string owner, string repo, int number, string name)
        {
            return "repos/{0}/{1}/issues/{2}/label/{3}".FormatUri(owner, repo, number, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public static Uri IssueLabels(string owner, string repo, int number)
        {
            return "repos/{0}/{1}/issues/{2}/labels".FormatUri(owner, repo, number);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the commit statuses for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public static Uri CommitStatus(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/statuses/{2}".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the starred repositories for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
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
        /// Returns the <see cref="Uri"/> for the specified commit.
        /// </summary>
        /// <param name="id">The id of the gist</param>
        public static Uri Gist(string id)
        {
            return "gists/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for all the gists of the provided <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user name</param>
        public static Uri Gists(string user)
        {
            return "users/{0}/gists".FormatUri(user);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments for the specified gist.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        public static Uri GistComments(int gistId)
        {
            return "gists/{0}/comments".FormatUri(gistId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a spesific comment for the specified commit.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        public static Uri GistComment(int gistId, int commentId)
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
        /// Returns the <see cref="Uri"/> for the network of repositories.
        /// </summary>
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
        /// Returns the <see cref="Uri"/> for a specifc blob.
        /// </summary>
        /// <param name="owner">The owner of the blob</param>
        /// <param name="name">The name of the organization</param>
        /// <returns></returns>
        public static Uri Blob(string owner, string name)
        {
            return Blob(owner, name, "");
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specifc blob.
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
        /// returns the <see cref="Uri"/> for teams
        /// use for update or deleting a team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Uri TeamsUpdateOrDelete(int id)
        {
            return "teams/{0}".FormatUri(id);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for teams
        /// use for update or deleting a team
        /// </summary>
        /// <param name="owner">owner of repo</param>
        /// /// <param name="repo">name of repo</param>
        /// <returns></returns>
        public static Uri RepoCollaborators(string owner, string repo)
        {
            return "repos/{0}/{1}/collaborators".FormatUri(owner, repo);
        }
    }
}
