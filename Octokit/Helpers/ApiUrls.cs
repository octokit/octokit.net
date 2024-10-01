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
        static readonly Uri _currentUserSshKeys = new Uri("user/keys", UriKind.Relative);
        static readonly Uri _currentUserGpgKeys = new Uri("user/gpg_keys", UriKind.Relative);
        static readonly Uri _currentUserStars = new Uri("user/starred", UriKind.Relative);
        static readonly Uri _currentUserWatched = new Uri("user/subscriptions", UriKind.Relative);
        static readonly Uri _currentUserEmailsEndpoint = new Uri("user/emails", UriKind.Relative);
        static readonly Uri _currentUserNotificationsEndpoint = new Uri("notifications", UriKind.Relative);
        static readonly Uri _currentUserAllIssues = new Uri("issues", UriKind.Relative);
        static readonly Uri _currentUserOwnedAndMemberIssues = new Uri("user/issues", UriKind.Relative);
        static readonly Uri _currentUserAllCodespaces = new Uri("user/codespaces", UriKind.Relative);

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
        /// Returns the <see cref="Uri"/> that create a repository using a template.
        /// </summary>
        /// <returns></returns>
        public static Uri Repositories(string owner, string repo)
        {
            return "repos/{0}/{1}/generate".FormatUri(owner, repo);
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
        /// Returns the <see cref="Uri"/> that returns all of the custom properties for the specified organization in
        /// response to a GET request. A PATCH to this URL updates the custom properties for the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationCustomProperties(string organization)
        {
            return "orgs/{0}/properties/schema".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a custom property for the specified organization in
        /// response to a GET request. A PUT to this URL updates the custom property for the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <param name="property">The name of the property</param>
        /// <returns></returns>
        public static Uri OrganizationCustomProperty(string organization, string property)
        {
            return "orgs/{0}/properties/schema/{1}".FormatUri(organization, property);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a custom property values for repositories in the
        /// specified organization in response to a GET request. A PATCH to this URL updates the custom property
        /// values for specified repositories in the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationCustomPropertyValues(string organization)
        {
            return "orgs/{0}/properties/values".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the secrets for the specified organization in
        /// response to a GET request.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationRepositorySecrets(string organization)
        {
            return "orgs/{0}/actions/secrets".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a secret for the specified organization in
        /// response to a GET request. A POST to this URL creates a new secret for the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <param name="secret">The name of the secret</param>
        /// <returns></returns>
        public static Uri OrganizationRepositorySecret(string organization, string secret)
        {
            return "orgs/{0}/actions/secrets/{1}".FormatUri(organization, secret);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the public key for signing secrets for the specified organization in
        /// response to a GET request.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationRepositorySecretPublicKey(string organization)
        {
            return "orgs/{0}/actions/secrets/public-key".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a list of repositories for a secret for the specified organization in
        /// response to a GET request. A POST to this URL sets the full repository list for a secret in the organization.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <param name="secret">The name of the secret</param>
        /// <returns></returns>
        public static Uri OrganizationRepositorySecretRepositories(string organization, string secret)
        {
            return "orgs/{0}/actions/secrets/{1}/repositories".FormatUri(organization, secret);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that adds (PUT) or removes (DELETE) a repository from the visibility list of a secret.
        /// </summary>
        /// <param name="organization">The name of the organization</param>
        /// <param name="secret">The name of the secret</param>
        /// <param name="repoId">The id of the repo to target</param>
        /// <returns></returns>
        public static Uri OrganizationRepositorySecretRepository(string organization, string secret, long repoId)
        {
            return "orgs/{0}/actions/secrets/{1}/repositories/{2}".FormatUri(organization, secret, repoId.ToString());
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
        /// Returns the <see cref="Uri"/> that returns all of the organization memberships for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public static Uri UserOrganizationMemberships()
        {
            return "user/memberships/orgs".FormatUri();
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
        /// <param name="since">The integer Id of the last Organization that you’ve seen.</param>
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
        /// <param name="keyId">The Key Id to retrieve</param>
        public static Uri Keys(long keyId)
        {
            return "user/keys/{0}".FormatUri(keyId);
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
        /// Returns the <see cref="Uri"/> that generates release notes for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="Uri"/> that generates release notes for the specified repository.</returns>
        public static Uri ReleasesGenerateNotes(string owner, string name)
        {
            return "repos/{0}/{1}/releases/generate-notes".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="releaseId">The id of the release</param>
        /// <returns></returns>
        public static Uri Releases(string owner, string name, long releaseId)
        {
            return "repos/{0}/{1}/releases/{2}".FormatUri(owner, name, releaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="tag">The tag of the release</param>
        /// <returns></returns>
        public static Uri Releases(string owner, string name, string tag)
        {
            return "repos/{0}/{1}/releases/tags/{2}".FormatUri(owner, name, tag);
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
        /// <param name="releaseId">The id of the release</param>
        /// <returns></returns>
        public static Uri ReleaseAssets(string owner, string name, long releaseId)
        {
            return "repos/{0}/{1}/releases/{2}/assets".FormatUri(owner, name, releaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the assets specified by the asset id.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="releaseAssetId">The id of the release asset</param>
        /// <returns></returns>
        public static Uri Asset(string owner, string name, int releaseAssetId)
        {
            return "repos/{0}/{1}/releases/assets/{2}".FormatUri(owner, name, releaseAssetId);
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
        /// <param name="notificationId">The Id of the notification.</param>
        /// <returns></returns>
        public static Uri Notification(int notificationId)
        {
            return "notifications/threads/{0}".FormatUri(notificationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified notification's subscription status.
        /// </summary>
        /// <param name="notificationId">The Id of the notification.</param>
        public static Uri NotificationSubscription(int notificationId)
        {
            return "notifications/threads/{0}/subscription".FormatUri(notificationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to complete the handshake necessary when implementing the GitHub App Manifest flow.
        /// </summary>
        /// <param name="code">Temporary code in a code parameter.</param>
        public static Uri AppManifestConversions(string code)
        {
            return "app-manifests/{0}/conversions".FormatUri(code);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for creating a new installation token.
        /// </summary>
        /// <param name="installationId">The Id of the GitHub App installation.</param>
        public static Uri AccessTokens(long installationId)
        {
            return "app/installations/{0}/access_tokens".FormatUri(installationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that creates a github app.
        /// </summary>
        public static Uri App()
        {
            return "app".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that creates a github app.
        /// </summary>
        public static Uri App(string slug)
        {
            return "apps/{0}".FormatUri(slug);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the installations of the authenticated application.
        /// </summary>
        public static Uri Installations()
        {
            return "app/installations".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists repositories accessible to the user for an installation..
        /// </summary>
        /// <param name="installationId">The id of the installation</param>
        public static Uri UserInstallationRepositories(long installationId)
        {
            return "user/installations/{0}/repositories".FormatUri(installationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the repository's installation information.
        /// </summary>
        /// <returns></returns>
        public static Uri RepoInstallation(string owner, string repo)
        {
            return "repos/{0}/{1}/installation".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the repository's installation information.
        /// </summary>
        /// <returns></returns>
        public static Uri RepoInstallation(long repositoryId)
        {
            return "repositories/{0}/installation".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the organization's installation information.
        /// </summary>
        public static Uri OrganizationInstallation(string organization)
        {
            return "orgs/{0}/installation".FormatUri(organization); ;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the user's installation information.
        /// </summary>
        public static Uri UserInstallation(string username)
        {
            return "users/{0}/installation".FormatUri(username);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single installation of the authenticated application.
        /// </summary>
        public static Uri Installation(long installationId)
        {
            return "app/installations/{0}".FormatUri(installationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the installations in repositories the user has explicit permission to access
        /// </summary>
        public static Uri UserInstallations()
        {
            return "user/installations".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all the repositories
        /// </summary>
        /// <returns></returns>
        public static Uri InstallationRepositories()
        {
            return "installation/repositories".FormatUri();
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
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri Issue(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}".FormatUri(owner, name, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue to be locked/unlocked.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueLock(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/lock".FormatUri(owner, name, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueReactions(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/reactions".FormatUri(owner, name, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueReactions(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/reactions".FormatUri(repositoryId, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reactionId">The reactionid for the issue</param>
        /// <returns></returns>
        public static Uri IssueReaction(string owner, string name, int issueNumber, long reactionId)
        {
            return "repos/{0}/{1}/issues/{2}/reactions/{3}".FormatUri(owner, name, issueNumber, reactionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reactionId">The reactionid for the issue</param>
        /// <returns></returns>
        public static Uri IssueReaction(long repositoryId, int issueNumber, long reactionId)
        {
            return "repositories/{0}/issues/{1}/reactions/{2}".FormatUri(repositoryId, issueNumber, reactionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the timeline of a specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueTimeline(string owner, string repo, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/timeline".FormatUri(owner, repo, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the timeline of a specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueTimeline(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/timeline".FormatUri(repositoryId, issueNumber);
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
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueComments(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/comments".FormatUri(owner, name, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri IssueComment(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/issues/comments/{2}".FormatUri(owner, name, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri IssueCommentReactions(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/issues/comments/{2}/reactions".FormatUri(owner, name, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri IssueCommentReactions(long repositoryId, long commentId)
        {
            return "repositories/{0}/issues/comments/{1}/reactions".FormatUri(repositoryId, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reactionid for the comment</param>
        /// <returns></returns>
        public static Uri IssueCommentReaction(string owner, string name, long commentId, long reaction)
        {
            return "repos/{0}/{1}/issues/comments/{2}/reactions/{3}".FormatUri(owner, name, commentId, reaction);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified issue comment.
        /// </summary>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reactionid for the comment</param>
        /// <returns></returns>
        public static Uri IssueCommentReaction(long repositoryId, long commentId, long reaction)
        {
            return "repositories/{0}/issues/comments/{1}/reactions/{2}".FormatUri(repositoryId, commentId, reaction);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri CommitComment(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/comments/{2}".FormatUri(owner, name, commentId);
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
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri CommitCommentReactions(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/comments/{2}/reactions".FormatUri(owner, name, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified commit comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri CommitCommentReactions(long repositoryId, long commentId)
        {
            return "repositories/{0}/comments/{1}/reactions".FormatUri(repositoryId, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified commit comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reaction number</param>
        /// <returns></returns>
        public static Uri CommitCommentReaction(string owner, string name, long commentId, long reaction)
        {
            return "repos/{0}/{1}/comments/{2}/reactions/{3}".FormatUri(owner, name, commentId, reaction);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified commit comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reaction number</param>
        /// <returns></returns>
        public static Uri CommitCommentReaction(long repositoryId, long commentId, long reaction)
        {
            return "repositories/{0}/comments/{1}/reactions/{2}".FormatUri(repositoryId, commentId, reaction);
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
        /// Otherwise returns a 404.
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
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueAssignees(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/assignees".FormatUri(owner, name, issueNumber);
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
        /// Returns the <see cref="Uri"/> for the organization memberships
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <param name="name">The name of the user</param>
        /// <returns></returns>
        public static Uri OrganizationMemberships(string org, string name)
        {
            return "orgs/{0}/memberships/{1}".FormatUri(org, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the organization's invitations
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationInvitations(string org)
        {
            return "orgs/{0}/invitations".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the organizations pending invitations
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationPendingInvitations(string org)
        {
            return "orgs/{0}/invitations".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the organizations failed invitations
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationFailedInvitations(string org)
        {
            return "orgs/{0}/failed_invitations".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to cancel an organization invitation
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <param name="invitationId">The unique identifier of the invitation</param>
        /// <returns></returns>
        public static Uri CancelOrganizationInvitation(string org, long invitationId)
        {
            return "orgs/{0}/invitations/{1}".FormatUri(org, invitationId);
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
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssuesEvents(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/events".FormatUri(owner, name, issueNumber);
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
        /// <param name="eventId">The event id</param>
        /// <returns></returns>
        public static Uri IssuesEvent(string owner, string name, long eventId)
        {
            return "repos/{0}/{1}/issues/events/{2}".FormatUri(owner, name, eventId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified milestone.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The milestone number</param>
        /// <returns></returns>
        public static Uri Milestone(string owner, string name, int milestoneNumber)
        {
            return "repos/{0}/{1}/milestones/{2}".FormatUri(owner, name, milestoneNumber);
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
        /// <param name="issueNumber">The issue number</param>
        /// <param name="labelName">The name of the label</param>
        /// <returns></returns>
        public static Uri IssueLabel(string owner, string name, int issueNumber, string labelName)
        {
            return "repos/{0}/{1}/issues/{2}/labels/{3}".FormatUri(owner, name, issueNumber, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns></returns>
        public static Uri IssueLabels(string owner, string name, int issueNumber)
        {
            return "repos/{0}/{1}/issues/{2}/labels".FormatUri(owner, name, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The milestone number</param>
        /// <returns></returns>
        public static Uri MilestoneLabels(string owner, string name, int milestoneNumber)
        {
            return "repos/{0}/{1}/milestones/{2}/labels".FormatUri(owner, name, milestoneNumber);
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
        /// Returns the <see cref="Uri"/> that lists the organization hooks for the specified reference.
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns></returns>
        public static Uri OrganizationHooks(string org)
        {
            return "orgs/{0}/hooks".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the organization hook for the specified reference.
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <param name="hookId">The identifier of the organization hook</param>
        /// <returns></returns>
        public static Uri OrganizationHookById(string org, int hookId)
        {
            return "orgs/{0}/hooks/{1}".FormatUri(org, hookId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that can ping a specified organization hook
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <param name="hookId">The identifier of the organization hook</param>
        /// <returns></returns>
        public static Uri OrganizationHookPing(string org, int hookId)
        {
            return "orgs/{0}/hooks/{1}/pings".FormatUri(org, hookId);
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
        /// <param name="gistId">The id of the gist</param>
        public static Uri Gist(string gistId)
        {
            return "gists/{0}".FormatUri(gistId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the forks for the specified gist.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        public static Uri ForkGist(string gistId)
        {
            return "gists/{0}/forks".FormatUri(gistId);
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
        /// <param name="gistId">The id of the gist</param>
        public static Uri StarGist(string gistId)
        {
            return "gists/{0}/star".FormatUri(gistId);
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
        /// <param name="gistId">The id of the gist</param>
        public static Uri GistCommits(string gistId)
        {
            return "gists/{0}/commits".FormatUri(gistId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns></returns>
        public static Uri PullRequest(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}".FormatUri(owner, name, pullRequestNumber);
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the pull request merge state.</returns>
        public static Uri MergePullRequest(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/merge".FormatUri(owner, name, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the commits on a pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the commits on a pull request.</returns>
        public static Uri PullRequestCommits(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/commits".FormatUri(owner, name, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the files on a pull request.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the files on a pull request.</returns>
        public static Uri PullRequestFiles(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/files".FormatUri(owner, name, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specific comment for the specified commit.
        /// </summary>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        public static Uri GistComment(string gistId, long commentId)
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComments(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/comments".FormatUri(owner, name, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reviews opf a specified pull request
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviews(string owner, string name, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews".FormatUri(owner, name, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComment(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/pulls/comments/{2}".FormatUri(owner, name, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReview(string owner, string name, int pullRequestNumber, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}".FormatUri(owner, name, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dismissing a specified pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewDismissal(long repositoryId, int pullRequestNumber, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/dismissals".FormatUri(repositoryId, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dismissing a specified pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewDismissal(string owner, string name, int pullRequestNumber, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/dismissals".FormatUri(owner, name, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewSubmit(long repositoryId, int pullRequestNumber, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/events".FormatUri(repositoryId, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewSubmit(string owner, string name, int pullRequestNumber, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/events".FormatUri(owner, name, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComments(long repositoryId, int pullRequestNumber, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}/comments".FormatUri(repositoryId, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a pull request review
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReviewComments(string owner, string name, int pullRequestNumber, long reviewId)
        {
            return "repos/{0}/{1}/pulls/{2}/reviews/{3}/comments".FormatUri(owner, name, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a specified pull request review.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <returns>The <see cref="Uri"/></returns>
        public static Uri PullRequestReview(long repositoryId, int pullRequestNumber, long reviewId)
        {
            return "repositories/{0}/pulls/{1}/reviews/{2}".FormatUri(repositoryId, pullRequestNumber, reviewId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reactions of a specified pull request review comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReactions(string owner, string name, long commentId)
        {
            return "repos/{0}/{1}/pulls/comments/{2}/reactions".FormatUri(owner, name, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reactions of a specified pull request review comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReactions(long repositoryId, long commentId)
        {
            return "repositories/{0}/pulls/comments/{1}/reactions".FormatUri(repositoryId, commentId);
        }


        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified pull request review comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reactionid for the comment</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReaction(string owner, string name, long commentId, long reaction)
        {
            return "repos/{0}/{1}/pulls/comments/{2}/reactions/{3}".FormatUri(owner, name, commentId, reaction);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reaction of a specified pull request review comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reactionid for the comment</param>
        /// <returns></returns>
        public static Uri PullRequestReviewCommentReaction(long repositoryId, long commentId, long reaction)
        {
            return "repositories/{0}/pulls/comments/{1}/reactions/{2}".FormatUri(repositoryId, commentId, reaction);
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
            return "repos/{0}/{1}/git/trees/{2}?recursive=1".FormatUri(owner, name, reference.TrimEnd('/'));
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
        /// <param name="parentTeamId">The id of the parent team</param>
        /// <returns></returns>
        public static Uri TeamChildTeams(long parentTeamId)
        {
            return "teams/{0}/teams".FormatUri(parentTeamId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for teams
        /// use for getting, updating, or deleting a <see cref="Team"/>.
        /// </summary>
        /// <param name="teamId">The id of the <see cref="Team"/>.</param>
        /// <returns></returns>
        public static Uri Teams(long teamId)
        {
            return "teams/{0}".FormatUri(teamId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for teams
        /// use for updating, or deleteing a <see cref="Team"/>.
        /// </summary>
        /// <param name="org"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static Uri TeamsByOrganizationAndSlug(string org, string teamId)
        {
            return "orgs/{0}/teams/{1}".FormatUri(org, teamId);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for team member
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <param name="login">The user login.</param>
        public static Uri TeamMember(long teamId, string login)
        {
            return "teams/{0}/memberships/{1}".FormatUri(teamId, login);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for team members list
        /// </summary>
        /// <param name="teamId">The team id</param>
        public static Uri TeamMembers(long teamId)
        {
            return "teams/{0}/members".FormatUri(teamId);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for the repositories
        /// </summary>
        /// <param name="teamId">The team id</param>
        public static Uri TeamRepositories(long teamId)
        {
            return "teams/{0}/repos".FormatUri(teamId);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for a team repository
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <param name="organization">The organization id</param>
        /// <param name="repoName">The repository name</param>
        public static Uri TeamRepository(long teamId, string organization, string repoName)
        {
            return "teams/{0}/repos/{1}/{2}".FormatUri(teamId, organization, repoName);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for a team repository
        /// </summary>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamId">The slug of the team name.</param>
        /// <param name="owner">The account owner of the repository. The name is not case sensitive.</param>
        /// <param name="repo">The name of the repository. The name is not case sensitive.</param>
        public static Uri TeamPermissionsForARepository(string org, string teamId, string owner, string repo)
        {
            return "/orgs/{0}/teams/{1}/repos/{2}/{3}".FormatUri(org, teamId, owner, repo);
        }

        /// <summary>
        /// returns the <see cref="Uri"/> for the teams pending invitations
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <returns></returns>
        public static Uri TeamPendingInvitations(long teamId)
        {
            return "teams/{0}/invitations".FormatUri(teamId);
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
        /// Creates the relative <see cref="Uri"/> for searching labels
        /// </summary>
        /// <returns></returns>
        public static Uri SearchLabels()
        {
            return "search/labels".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository codeowners errors.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>the <see cref="Uri"/> for repository topics.</returns>
        public static Uri RepositoryCodeOwnersErrors(string owner, string name)
        {
            return "repos/{0}/{1}/codeowners/errors".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository codeowners errors.
        /// </summary>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>the <see cref="Uri"/> for repository topics.</returns>
        public static Uri RepositoryCodeOwnersErrors(long repositoryId)
        {
            return "repositories/{0}/codeowners/errors".FormatUri(repositoryId);
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
        /// Returns the <see cref="Uri"/> for repository topics.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>the <see cref="Uri"/> for repository topics.</returns>
        public static Uri RepositoryTopics(string owner, string name)
        {
            return "repos/{0}/{1}/topics".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository topics.
        /// </summary>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>the <see cref="Uri"/> for repository topics.</returns>
        public static Uri RepositoryTopics(long repositoryId)
        {
            return "repositories/{0}/topics".FormatUri(repositoryId);
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
        /// Returns the <see cref="Uri"/> for a repository transfer.
        /// </summary>
        /// <param name="owner">The current owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryTransfer(string owner, string name)
        {
            return "repos/{0}/{1}/transfer".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for a repository transfer.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryTransfer(long repositoryId)
        {
            return "repositories/{0}/transfer".FormatUri(repositoryId);
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

        ///repos/{owner}/{repo}/commits/{commit_sha}/
        /// <summary>
        /// Returns the <see cref="Uri"/> that lists all branches where the given commit SHA is the HEAD, or latest commit for the branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri RepositoryCommitsBranchesWhereHead(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/branches-where-head".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists all branches where the given commit SHA is the HEAD, or latest commit for the branch.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri RepositoryCommitsBranchesWhereHead(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/branches-where-head".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check suites for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri RepositoryCommitsPull(string owner, string name, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/pulls".FormatUri(owner, name, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check suites for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (SHA)</param>
        /// <returns></returns>
        public static Uri RepositoryCommitsPull(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/pulls".FormatUri(repositoryId, reference);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

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
        /// <param name="deployKeyId">The id of the deploy key of the repository</param>
        /// <returns></returns>
        public static Uri RepositoryDeployKey(string owner, string name, int deployKeyId)
        {
            return "repos/{0}/{1}/keys/{2}".FormatUri(owner, name, deployKeyId);
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
        /// Returns the <see cref="Uri"/> for checking vulnerability alerts for a repository.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Uri RepositoryVulnerabilityAlerts(string owner, string name)
        {
            return "repos/{0}/{1}/vulnerability-alerts".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for getting Dependency-Diffs between two revisions.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <returns>The <see cref="System.Uri"/> for getting Dependency-Diffs between two revisions for the given repository.</returns>
        public static Uri DependencyReview(string owner, string name, string @base, string head)
        {
            return "repos/{0}/{1}/dependency-graph/compare/{2}...{3}".FormatUri(owner, name, @base, head);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a Dependency Snapshot for the given repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for submitting a Dependency Snapshot for the given repository.</returns>
        public static Uri DependencySubmission(string owner, string name)
        {
            return "repos/{0}/{1}/dependency-graph/snapshots".FormatUri(owner, name);
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
        /// Returns the <see cref="Uri"/> for the Deployment Environments API for the given repository.
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <returns>The <see cref="Uri"/> for the Deployments API for the given repository.</returns>
        public static Uri DeploymentEnvironments(string owner, string name)
        {
            return "repos/{0}/{1}/environments".FormatUri(owner, name);
        }

        /// <summary>
        /// Returns the <see cref="System.Uri"/> for the Deployment Statuses API for the given deployment.
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="name">Name of the repository</param>
        /// <param name="deploymentId">Id of the deployment</param>
        /// <returns></returns>
        public static Uri DeploymentStatuses(string owner, string name, long deploymentId)
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
        /// Creates the relative <see cref="Uri"/> for initiating the OAuth device Flow
        /// </summary>
        /// <returns></returns>
        public static Uri OauthDeviceCode()
        {
            return "login/device/code".FormatUri();
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
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually main)</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(string owner, string name, string path, string reference)
        {
            return "repos/{0}/{1}/contents/{2}?ref={3}".FormatUri(owner, name, path == "/" ? "" : path.TrimEnd('/'), reference);
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

        public static Uri EnterpriseAuditLog(string enterprise)
        {
            return "enterprises/{0}/audit-log".FormatUri(enterprise);
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

        public static Uri EnterpriseLdapTeamMapping(long teamId)
        {
            return "admin/ldap/teams/{0}/mapping".FormatUri(teamId);
        }

        public static Uri EnterpriseLdapTeamSync(long teamId)
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

        public static Uri EnterpriseMigrationById(string org, long migrationId)
        {
            return "orgs/{0}/migrations/{1}".FormatUri(org, migrationId);
        }

        public static Uri EnterpriseMigrations(string org)
        {
            return "orgs/{0}/migrations".FormatUri(org);
        }

        public static Uri EnterpriseMigrationArchive(string org, long migrationId)
        {
            return "orgs/{0}/migrations/{1}/archive".FormatUri(org, migrationId);
        }

        public static Uri EnterpriseMigrationUnlockRepository(string org, long migrationId, string repo)
        {
            return "orgs/{0}/migrations/{1}/repos/{2}/lock".FormatUri(org, migrationId, repo);
        }

        public static Uri EnterpriseManagementConsoleMaintenance(string managementConsolePassword, Uri baseAddress)
        {
            if (baseAddress != null
                && baseAddress.ToString().EndsWith("/api/v3/", StringComparison.OrdinalIgnoreCase))
            {
                // note: leading slash here means the /api/v3/ prefix inherited from baseAddress is ignored
                return "/setup/api/maintenance?api_key={0}".FormatUri(managementConsolePassword);
            }

            return "setup/api/maintenance?api_key={0}".FormatUri(managementConsolePassword);
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
        /// Creates the <see cref="Uri"/> for pre-receive environments.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveEnvironments()
        {
            return "admin/pre-receive-environments".FormatUri();
        }

        /// <summary>
        /// Creates the <see cref="Uri"/> for pre-receive environments.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveEnvironments(long environmentId)
        {
            return "admin/pre-receive-environments/{0}".FormatUri(environmentId);
        }

        /// <summary>
        /// Creates the <see cref="Uri"/> for pre-receive environment download status.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveEnvironmentDownload(long environmentId)
        {
            return "admin/pre-receive-environments/{0}/downloads".FormatUri(environmentId);
        }

        /// <summary>
        /// Creates the <see cref="Uri"/> for pre-receive environment download status.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveEnvironmentDownloadStatus(long environmentId)
        {
            return "admin/pre-receive-environments/{0}/downloads/latest".FormatUri(environmentId);
        }

        /// <summary>
        /// Creates the <see cref="Uri"/> for pre-receive hooks.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveHooks()
        {
            return "admin/pre-receive-hooks".FormatUri();
        }

        /// <summary>
        /// Creates the <see cref="Uri"/> for pre-receive hooks.
        /// </summary>
        /// <returns></returns>
        public static Uri AdminPreReceiveHooks(long hookId)
        {
            return "admin/pre-receive-hooks/{0}".FormatUri(hookId);
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
        /// <param name="releaseAssetId">The id of the release asset</param>
        /// <returns>The <see cref="Uri"/> that returns the assets specified by the asset id.</returns>
        public static Uri Asset(long repositoryId, int releaseAssetId)
        {
            return "repositories/{0}/releases/assets/{1}".FormatUri(repositoryId, releaseAssetId);
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
        /// Returns the <see cref="Uri"/> that returns a 204 if the login belongs to an assignee of the repository. Otherwise returns a 404.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="login">The login for the user</param>
        /// <returns>The <see cref="Uri"/> that returns a 204 if the login belongs to an assignee of the repository. Otherwise returns a 404.</returns>
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
        /// <param name="commentId">The comment id</param>
        /// <returns>The <see cref="Uri"/> for the specified comment.</returns>
        public static Uri CommitComment(long repositoryId, long commentId)
        {
            return "repositories/{0}/comments/{1}".FormatUri(repositoryId, commentId);
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
        /// Returns the <see cref="Uri"/> for getting Dependency-Diffs between two revisions.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <returns>The <see cref="System.Uri"/> for getting Dependency-Diffs between two revisions for the given repository.</returns>
        public static Uri DependencyReview(long repositoryId, string @base, string head)
        {
            return "repositories/{0}/dependency-graph/compare/{1}...{2}".FormatUri(repositoryId, @base, head);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for submitting a Dependency Snapshot for the given repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="System.Uri"/> for submitting a Dependency Snapshot for the given repository.</returns>
        public static Uri DependencySubmission(long repositoryId)
        {
            return "repositories/{0}/dependency-graph/snapshots".FormatUri(repositoryId);
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
        /// Returns the <see cref="Uri"/> for the Deployment Environments API for the given repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> for the Deployments API for the given repository.</returns>
        public static Uri DeploymentEnvironments(long repositoryId)
        {
            return "repositories/{0}/environments".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Deployment Statuses API for the given deployment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="deploymentId">Id of the deployment</param>
        /// <returns>The <see cref="Uri"/> for the Deployment Statuses API for the given deployment.</returns>
        public static Uri DeploymentStatuses(long repositoryId, long deploymentId)
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
        /// <param name="gpgKeyId">The GPG Key Id.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        public static Uri GpgKeys(long gpgKeyId)
        {
            return "user/gpg_keys/{0}".FormatUri(gpgKeyId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the specified issue.</returns>
        public static Uri Issue(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}".FormatUri(repositoryId, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns>The <see cref="Uri"/> for the specified comment.</returns>
        public static Uri IssueComment(long repositoryId, long commentId)
        {
            return "repositories/{0}/issues/comments/{1}".FormatUri(repositoryId, commentId);
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
        /// <param name="issueNumber">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the comments of a specified issue.</returns>
        public static Uri IssueComments(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/comments".FormatUri(repositoryId, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the named label for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="labelName">The name of the label</param>
        /// <returns>The <see cref="Uri"/> that returns the named label for the specified issue.</returns>
        public static Uri IssueLabel(long repositoryId, int issueNumber, string labelName)
        {
            return "repositories/{0}/issues/{1}/labels/{2}".FormatUri(repositoryId, issueNumber, labelName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns>The <see cref="Uri"/> that returns all of the labels for the specified issue.</returns>
        public static Uri IssueLabels(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/labels".FormatUri(repositoryId, issueNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified issue to be locked/unlocked.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns>The <see cref="Uri"/> for the specified issue to be locked/unlocked.</returns>
        public static Uri IssueLock(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/lock".FormatUri(repositoryId, issueNumber);
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
        /// <param name="eventId">The event id</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event and issue info for the specified event.</returns>
        public static Uri IssuesEvent(long repositoryId, long eventId)
        {
            return "repositories/{0}/issues/events/{1}".FormatUri(repositoryId, eventId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <returns>The <see cref="Uri"/> that returns the issue/pull request event info for the specified issue.</returns>
        public static Uri IssuesEvents(long repositoryId, int issueNumber)
        {
            return "repositories/{0}/issues/{1}/events".FormatUri(repositoryId, issueNumber);
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the pull request merge state.</returns>
        public static Uri MergePullRequest(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/merge".FormatUri(repositoryId, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified milestone.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The milestone number</param>
        /// <returns>The <see cref="Uri"/> that returns the specified milestone.</returns>
        public static Uri Milestone(long repositoryId, int milestoneNumber)
        {
            return "repositories/{0}/milestones/{1}".FormatUri(repositoryId, milestoneNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The milestone number</param>
        /// <returns>The <see cref="Uri"/> that returns all of the labels for all issues in the specified milestone.</returns>
        public static Uri MilestoneLabels(long repositoryId, int milestoneNumber)
        {
            return "repositories/{0}/milestones/{1}/labels".FormatUri(repositoryId, milestoneNumber);
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the specified pull request.</returns>
        public static Uri PullRequest(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}".FormatUri(repositoryId, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the commits on a pull request.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the commits on a pull request.</returns>
        public static Uri PullRequestCommits(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/commits".FormatUri(repositoryId, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the files on a pull request.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that returns the files on a pull request.</returns>
        public static Uri PullRequestFiles(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/files".FormatUri(repositoryId, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified pull request review comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComment(long repositoryId, long commentId)
        {
            return "repositories/{0}/pulls/comments/{1}".FormatUri(repositoryId, commentId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the comments of a specified pull request review.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviewComments(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/comments".FormatUri(repositoryId, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the reviews of a specified pull request
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> that </returns>
        public static Uri PullRequestReviews(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/reviews".FormatUri(repositoryId, pullRequestNumber);
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
        /// <param name="releaseId">The id of the release</param>
        /// <returns>The <see cref="Uri"/> that returns all the assets for the specified release for the specified repository.</returns>
        public static Uri ReleaseAssets(long repositoryId, long releaseId)
        {
            return "repositories/{0}/releases/{1}/assets".FormatUri(repositoryId, releaseId);
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
        /// Returns the <see cref="Uri"/> that generates release notes for the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that generates release notes for the specified repository.</returns>
        public static Uri ReleasesGenerateNotes(long repositoryId)
        {
            return "repositories/{0}/releases/generate-notes".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="releaseId">The id of the release</param>
        /// <returns>The <see cref="Uri"/> that returns a single release for the specified repository</returns>
        public static Uri Releases(long repositoryId, long releaseId)
        {
            return "repositories/{0}/releases/{1}".FormatUri(repositoryId, releaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single release for the specified repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="tag">The tag of the release</param>
        /// <returns>The <see cref="Uri"/> that returns a single release for the specified repository</returns>
        public static Uri Releases(long repositoryId, string tag)
        {
            return "repositories/{0}/releases/tags/{1}".FormatUri(repositoryId, tag);
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
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
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
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually main)</param>
        /// <returns>The <see cref="Uri"/> for getting the contents of the specified repository and path</returns>
        public static Uri RepositoryContent(long repositoryId, string path, string reference)
        {
            return "repositories/{0}/contents/{1}?ref={2}".FormatUri(repositoryId, path.TrimEnd('/'), reference);
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
        /// <param name="deployKeyId">The id of the deploy key of the repository</param>
        /// <returns>The <see cref="Uri"/> for a deploy key for a repository</returns>
        public static Uri RepositoryDeployKey(long repositoryId, int deployKeyId)
        {
            return "repositories/{0}/keys/{1}".FormatUri(repositoryId, deployKeyId);
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
            return "repositories/{0}/git/trees/{1}?recursive=1".FormatUri(repositoryId, reference.TrimEnd('/'));
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
        /// <param name="reactionId">The reaction number</param>
        /// <returns>The <see cref="Uri"/> that lists the watched repositories for the authenticated user.</returns>
        public static Uri Reactions(int reactionId)
        {
            return "reactions/{0}".FormatUri(reactionId);
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
        public static Uri RepositoryInvitations(long repositoryId, long invitationId)
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
        public static Uri UserInvitations(long invitationId)
        {
            return "user/repository_invitations/{0}".FormatUri(invitationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic referrers.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic referrers.</returns>
        public static Uri RepositoryTrafficReferrers(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/popular/referrers".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic referrers.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic referrers.</returns>
        public static Uri RepositoryTrafficReferrers(long repositoryId)
        {
            return "repositories/{0}/traffic/popular/referrers".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic paths.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic paths.</returns>
        public static Uri RepositoryTrafficPaths(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/popular/paths".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic paths.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic paths.</returns>
        public static Uri RepositoryTrafficPaths(long repositoryId)
        {
            return "repositories/{0}/traffic/popular/paths".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic views.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic views.</returns>
        public static Uri RepositoryTrafficViews(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/views".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic views.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository traffic views.</returns>
        public static Uri RepositoryTrafficViews(long repositoryId)
        {
            return "repositories/{0}/traffic/views".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic clones.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository traffic clones.</returns>
        public static Uri RepositoryTrafficClones(string owner, string repo)
        {
            return "repos/{0}/{1}/traffic/clones".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository traffic clones.
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> for pull request review requests.</returns>
        public static Uri PullRequestReviewRequests(string owner, string repo, int pullRequestNumber)
        {
            return "repos/{0}/{1}/pulls/{2}/requested_reviewers".FormatUri(owner, repo, pullRequestNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for pull request review requests.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <returns>The <see cref="Uri"/> for pull request review requests.</returns>
        public static Uri PullRequestReviewRequests(long repositoryId, int pullRequestNumber)
        {
            return "repositories/{0}/pulls/{1}/requested_reviewers".FormatUri(repositoryId, pullRequestNumber);
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
        /// <param name="projectId">The id of the project</param>
        /// <returns>The <see cref="Uri"/> for repository projects.</returns>
        public static Uri Project(int projectId)
        {
            return "projects/{0}".FormatUri(projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for project columns.
        /// </summary>
        /// <param name="columnId">The id of the columns</param>
        /// <returns>The <see cref="Uri"/> for project columns.</returns>
        public static Uri ProjectColumn(int columnId)
        {
            return "projects/columns/{0}".FormatUri(columnId);
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
        /// <param name="columnId">The id of the column to move</param>
        /// <returns>The <see cref="Uri"/> to move a project column.</returns>
        public static Uri ProjectColumnMove(int columnId)
        {
            return "projects/columns/{0}/moves".FormatUri(columnId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for project cards.
        /// </summary>
        /// <param name="cardId">The id of the card</param>
        /// <returns>The <see cref="Uri"/> for project cards.</returns>
        public static Uri ProjectCard(long cardId)
        {
            return "projects/columns/cards/{0}".FormatUri(cardId);
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
        /// <param name="cardId">The id of the card to move</param>
        /// <returns>The <see cref="Uri"/> to move a project card.</returns>
        public static Uri ProjectCardMove(long cardId)
        {
            return "projects/columns/cards/{0}/moves".FormatUri(cardId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository's license requests.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> for repository's license requests.</returns>
        public static Uri RepositoryLicense(string owner, string repo)
        {
            return "repos/{0}/{1}/license".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for repository's license requests.
        /// </summary>
        /// <param name="repositoryId">The id of the repository</param>
        /// <returns>The <see cref="Uri"/> for repository's license requests.</returns>
        public static Uri RepositoryLicense(long repositoryId)
        {
            return "repositories/{0}/license".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified check run.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The check run Id</param>
        /// <returns>The <see cref="Uri"/> that returns the specified check run.</returns>
        public static Uri CheckRun(long repositoryId, long checkRunId)
        {
            return "repositories/{0}/check-runs/{1}".FormatUri(repositoryId, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified check run.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="checkRunId">The check run Id</param>
        /// <returns>The <see cref="Uri"/> that returns the specified check run.</returns>
        public static Uri CheckRun(string owner, string repo, long checkRunId)
        {
            return "repos/{0}/{1}/check-runs/{2}".FormatUri(owner, repo, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check runs for the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that handles the check runs for the repository.</returns>
        public static Uri CheckRuns(long repositoryId)
        {
            return "repositories/{0}/check-runs".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check runs for the repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the check runs for the repository.</returns>
        public static Uri CheckRuns(string owner, string repo)
        {
            return "repos/{0}/{1}/check-runs".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check runs for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The git reference</param>
        /// <returns>The <see cref="Uri"/> that returns the check runs for the specified reference.</returns>
        public static Uri CheckRunsForReference(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/check-runs".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check runs for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="reference">The git reference</param>
        /// <returns>The <see cref="Uri"/> that returns the check runs for the specified reference.</returns>
        public static Uri CheckRunsForReference(string owner, string repo, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/check-runs".FormatUri(owner, repo, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check runs for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <returns>The <see cref="Uri"/> that returns the check runs for the specified reference.</returns>
        public static Uri CheckRunsForCheckSuite(long repositoryId, long checkSuiteId)
        {
            return "repositories/{0}/check-suites/{1}/check-runs".FormatUri(repositoryId, checkSuiteId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check runs for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <returns>The <see cref="Uri"/> that returns the check runs for the specified reference.</returns>
        public static Uri CheckRunsForCheckSuite(string owner, string repo, long checkSuiteId)
        {
            return "repos/{0}/{1}/check-suites/{2}/check-runs".FormatUri(owner, repo, checkSuiteId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the annotations for the specified check run.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <returns>The <see cref="Uri"/> that returns the annotations for the specified check run.</returns>
        public static Uri CheckRunAnnotations(long repositoryId, long checkRunId)
        {
            return "repositories/{0}/check-runs/{1}/annotations".FormatUri(repositoryId, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the annotations for the specified check run.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <returns>The <see cref="Uri"/> that returns the annotations for the specified check run.</returns>
        public static Uri CheckRunAnnotations(string owner, string repo, long checkRunId)
        {
            return "repos/{0}/{1}/check-runs/{2}/annotations".FormatUri(owner, repo, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified check suite.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The check run Id</param>
        /// <returns>The <see cref="Uri"/> that returns the specified check suite.</returns>
        public static Uri CheckSuite(long repositoryId, long checkRunId)
        {
            return "repositories/{0}/check-suites/{1}".FormatUri(repositoryId, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the specified check suite.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="checkRunId">The check run Id</param>
        /// <returns>The <see cref="Uri"/> that returns the specified check suite.</returns>
        public static Uri CheckSuite(string owner, string repo, long checkRunId)
        {
            return "repos/{0}/{1}/check-suites/{2}".FormatUri(owner, repo, checkRunId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check suites for the specified reference.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The git reference</param>
        /// <returns>The <see cref="Uri"/> that returns the check suites for the specified reference.</returns>
        public static Uri CheckSuitesForReference(long repositoryId, string reference)
        {
            return "repositories/{0}/commits/{1}/check-suites".FormatUri(repositoryId, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the check suites for the specified reference.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="reference">The git reference</param>
        /// <returns>The <see cref="Uri"/> that returns the check suites for the specified reference.</returns>
        public static Uri CheckSuitesForReference(string owner, string repo, string reference)
        {
            return "repos/{0}/{1}/commits/{2}/check-suites".FormatUri(owner, repo, reference);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suites for the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that handles the check suites for the repository.</returns>
        public static Uri CheckSuites(long repositoryId)
        {
            return "repositories/{0}/check-suites".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suites for the repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the check suites for the repository.</returns>
        public static Uri CheckSuites(string owner, string repo)
        {
            return "repos/{0}/{1}/check-suites".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suite requests for the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <returns>The <see cref="Uri"/> that handles the check suite requests for the repository.</returns>
        public static Uri CheckSuiteRerequest(long repositoryId, long checkSuiteId)
        {
            return "repositories/{0}/check-suites/{1}/rerequest".FormatUri(repositoryId, checkSuiteId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suite requests for the repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <returns>The <see cref="Uri"/> that handles the check suite requests for the repository.</returns>
        public static Uri CheckSuiteRerequest(string owner, string repo, long checkSuiteId)
        {
            return "repos/{0}/{1}/check-suites/{2}/rerequest".FormatUri(owner, repo, checkSuiteId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suite preferences for the repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>The <see cref="Uri"/> that handles the check suite preferences for the repository.</returns>
        public static Uri CheckSuitePreferences(long repositoryId)
        {
            return "repositories/{0}/check-suites/preferences".FormatUri(repositoryId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the check suite preferences for the repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the check suite preferences for the repository.</returns>
        public static Uri CheckSuitePreferences(string owner, string repo)
        {
            return "repos/{0}/{1}/check-suites/preferences".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository custom property values for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <returns>The <see cref="Uri"/> that handles the repository secrets for the repository</returns>
        public static Uri RepositoryCustomPropertyValues(string owner, string repo)
        {
            return "repos/{0}/{1}/properties/values".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository secrets for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <param name="secret">The name of the secret</param>
        /// <returns>The <see cref="Uri"/> that handles the repository secrets for the repository</returns>
        public static Uri RepositorySecret(string owner, string repo, string secret)
        {
            return "repos/{0}/{1}/actions/secrets/{2}".FormatUri(owner, repo, secret);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository secrets for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <returns>The <see cref="Uri"/> that handles the repository secrets for the repository</returns>
        public static Uri RepositorySecrets(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/secrets".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository secrets for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <returns>The <see cref="Uri"/> that handles the repository secrets for the repository</returns>
        public static Uri RepositorySecretsPublicKey(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/secrets/public-key".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository variables for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <param name="variable">The name of the variable</param>
        /// <returns>The <see cref="Uri"/> that handles the repository variables for the repository</returns>
        public static Uri RepositoryVariable(string owner, string repo, string variable)
        {
            return "repos/{0}/{1}/actions/variables/{2}".FormatUri(owner, repo, variable);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the repository variables for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <returns>The <see cref="Uri"/> that handles the repository variables for the repository</returns>
        public static Uri RepositoryVariables(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/variables".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the organization variables for the repository
        /// </summary>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        /// <returns>The <see cref="Uri"/> that handles the organization variables for the repository</returns>
        public static Uri RepositoryOrganizationVariables(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/organization-variables".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all emojis in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> for emojis.</returns>
        public static Uri Emojis()
        {
            return "emojis".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns rendered markdown in
        /// response to a POST request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to render markdown.</returns>
        public static Uri RawMarkdown()
        {
            return "markdown/raw".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns rendered markdown in
        /// response to a POST request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to render markdown.</returns>
        public static Uri Markdown()
        {
            return "markdown".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all git ignore templates in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to git ignore templates.</returns>
        public static Uri GitIgnoreTemplates()
        {
            return "gitignore/templates".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns specified git ignore templates in
        /// response to a GET request.
        /// </summary>
        /// <param name="templateName">The name of the template to retrieve</param>
        /// <returns>The <see cref="Uri"/> to git ignore template.</returns>
        public static Uri GitIgnoreTemplates(string templateName)
        {
            return "gitignore/templates/{0}".FormatUri(templateName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all licenses in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to licenses.</returns>
        public static Uri Licenses()
        {
            return "licenses".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns specified license in
        /// response to a GET request.
        /// </summary>
        /// <param name="key">The key of the license to retrieve</param>
        /// <returns>The <see cref="Uri"/> to license.</returns>
        public static Uri Licenses(string key)
        {
            return "licenses/{0}".FormatUri(key);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns rate limit in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to rate limit.</returns>
        public static Uri RateLimit()
        {
            return "rate_limit".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns meta in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to meta.</returns>
        public static Uri Meta()
        {
            return "meta".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns meta in
        /// response to a GET request.
        /// </summary>
        /// <returns>The <see cref="Uri"/> to meta.</returns>
        public static Uri PublicKeys(PublicKeyType keysType)
        {
            return "meta/public_keys/{0}".FormatUri(keysType.ToParameter());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all organization credentials in
        /// response to a GET request.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <returns>The <see cref="Uri"/> to meta.</returns>
        public static Uri AllOrganizationCredentials(string org)
        {
            return "orgs/{0}/credential-authorizations".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all organization credentials for a given login in
        /// response to a GET request.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <returns>The <see cref="Uri"/> to meta.</returns>
        public static Uri AllOrganizationCredentials(string org, string login)
        {
            return "orgs/{0}/credential-authorizations?login={1}".FormatUri(org, login);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Packages request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Packages endpoint.</returns>
        public static Uri PackagesOrg(string org)
        {
            return "orgs/{0}/packages".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageOrg(string org, PackageType packageType, string packageName)
        {
            return "orgs/{0}/packages/{1}/{2}".FormatUri(org, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Restore request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package Restore endpoint.</returns>
        public static Uri PackageRestoreOrg(string org, PackageType packageType, string packageName)
        {
            return "orgs/{0}/packages/{1}/{2}/restore".FormatUri(org, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Versions request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionsOrg(string org, PackageType packageType, string packageName)
        {
            return "orgs/{0}/packages/{1}/{2}/versions".FormatUri(org, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            return "orgs/{0}/packages/{1}/{2}/versions/{3}".FormatUri(org, packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionRestoreOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            return "orgs/{0}/packages/{1}/{2}/versions/{3}/restore".FormatUri(org, packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Packages request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Packages endpoint.</returns>
        public static Uri PackagesActiveUser()
        {
            return "user/packages".FormatUri();
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageActiveUser(PackageType packageType, string packageName)
        {
            return "user/packages/{0}/{1}".FormatUri(packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Restore request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package Restore endpoint.</returns>
        public static Uri PackageRestoreActiveUser(PackageType packageType, string packageName)
        {
            return "user/packages/{0}/{1}/restore".FormatUri(packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Versions request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionsActiveUser(PackageType packageType, string packageName)
        {
            return "user/packages/{0}/{1}/versions".FormatUri(packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            return "user/packages/{0}/{1}/versions/{2}".FormatUri(packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionRestoreActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            return "user/packages/{0}/{1}/versions/{2}/restore".FormatUri(packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Packages request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Packages endpoint.</returns>
        public static Uri PackagesUser(string username)
        {
            return "users/{0}/packages".FormatUri(username);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageUser(string username, PackageType packageType, string packageName)
        {
            return "users/{0}/packages/{1}/{2}".FormatUri(username, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Restore request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package Restore endpoint.</returns>
        public static Uri PackageRestoreUser(string username, PackageType packageType, string packageName)
        {
            return "users/{0}/packages/{1}/{2}/restore".FormatUri(username, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Versions request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionsUser(string username, PackageType packageType, string packageName)
        {
            return "users/{0}/packages/{1}/{2}/versions".FormatUri(username, packageType.ToParameter(), packageName);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            return "users/{0}/packages/{1}/{2}/versions/{3}".FormatUri(username, packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the Package Version request
        /// </summary>
        /// <returns>The <see cref="Uri"/> Package endpoint.</returns>
        public static Uri PackageVersionRestoreUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            return "users/{0}/packages/{1}/{2}/versions/{3}/restore".FormatUri(username, packageType.ToParameter(), packageName, packageVersionId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDispatchWorkflow(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/dispatches".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDispatchWorkflow(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/dispatches".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDispatchWorkflow(long repositoryId, long workflowId)
        {
            return "repositories/{0}/actions/workflows/{1}/dispatches".FormatUri(repositoryId, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDispatchWorkflow(long repositoryId, string workflowFileName)
        {
            return "repositories/{0}/actions/workflows/{1}/dispatches".FormatUri(repositoryId, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDisableWorkflow(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/disable".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that disables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsDisableWorkflow(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/disable".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that enables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsEnableWorkflow(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/enable".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that enables an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsEnableWorkflow(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/enable".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflow(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflow(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow'usage for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflowUsage(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/timing".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow's usage for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflowUsage(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/timing".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions workflows for the repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions workflows for the repository.</returns>
        public static Uri ActionsListWorkflows(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/workflows".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that re-runs an Actions workflow job for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsRerunWorkflowJob(string owner, string repo, long jobId)
        {
            return "repos/{0}/{1}/actions/jobs/{2}/rerun".FormatUri(owner, repo, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that re-runs an Actions workflow job for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflowJob(string owner, string repo, long jobId)
        {
            return "repos/{0}/{1}/actions/jobs/{2}".FormatUri(owner, repo, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the logs an Actions workflow job for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow job for a repository.</returns>
        public static Uri ActionsGetWorkflowJobLogs(string owner, string repo, long jobId)
        {
            return "repos/{0}/{1}/actions/jobs/{2}/logs".FormatUri(owner, repo, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions jobs for a workflow run.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions workflows runs for a workflow.</returns>
        public static Uri ActionsListWorkflowJobs(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/jobs".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions jobs for a workflow run.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions workflows runs for a workflow.</returns>
        public static Uri ActionsListWorkflowJobs(string owner, string repo, long runId, int attemptNumber)
        {
            return "repos/{0}/{1}/actions/runs/{2}/attempts/{3}/jobs".FormatUri(owner, repo, runId, attemptNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets Actions workflow runs for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that gets Actions workflow runs for a repository.</returns>
        public static Uri ActionsWorkflowRuns(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/runs".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow run for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow run for a repository.</returns>
        public static Uri ActionsWorkflowRun(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow run attempt for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow run for a repository.</returns>
        public static Uri ActionsWorkflowRunAttempt(string owner, string repo, long runId, long attemptNumber)
        {
            return "repos/{0}/{1}/actions/runs/{2}/attempts/{3}".FormatUri(owner, repo, runId, attemptNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that approves an Actions workflow run for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that approves an Actions workflow run for a repository.</returns>
        public static Uri ActionsApproveWorkflowRun(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/approve".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that cancels an Actions workflow run for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that cancels an Actions workflow run for a repository.</returns>
        public static Uri ActionsCancelWorkflowRun(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/cancel".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the logs an Actions workflow run attempt for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow run for a repository.</returns>
        public static Uri ActionsGetWorkflowRunLogs(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/logs".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets the logs an Actions workflow run attempt for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow run for a repository.</returns>
        public static Uri ActionsGetWorkflowRunAttemptLogs(string owner, string repo, long runId, long attemptNumber)
        {
            return "repos/{0}/{1}/actions/runs/{2}/attempts/{3}/logs".FormatUri(owner, repo, runId, attemptNumber);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that re-runs an Actions workflow run for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsRerunWorkflowRun(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/rerun".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that re-runs failed jobs of an Actions workflow run for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow job.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsRerunWorkflowRunFailedJobs(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/rerun-failed-jobs".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets an Actions workflow's usage for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets an Actions workflow for a repository.</returns>
        public static Uri ActionsGetWorkflowRunUsage(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/timing".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets Actions workflow run approvals for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets Actions workflow run approvals for a repository.</returns>
        public static Uri ActionsWorkflowRunApprovals(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/approvals".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets Actions workflow run pending deployments for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that gets Actions workflow run pending deployments for a repository.</returns>
        public static Uri ActionsWorkflowRunPendingDeployments(string owner, string repo, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/pending_deployments".FormatUri(owner, repo, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions workflow runs for a workflow.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions workflows runs for a workflow.</returns>
        public static Uri ActionsListWorkflowRuns(string owner, string repo, long workflowId)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/runs".FormatUri(owner, repo, workflowId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions workflow runs for a workflow.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions workflows runs for a workflow.</returns>
        public static Uri ActionsListWorkflowRuns(string owner, string repo, string workflowFileName)
        {
            return "repos/{0}/{1}/actions/workflows/{2}/runs".FormatUri(owner, repo, workflowFileName.UriEncode());
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runners for an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runners for an enterprise.</returns>
        public static Uri ActionsListSelfHostedRunnersForEnterprise(string enterprise)
        {
            return "enterprises/{0}/actions/runners".FormatUri(enterprise);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runners for an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runners for an organization.</returns>
        public static Uri ActionsListSelfHostedRunnersForOrganization(string org)
        {
            return "orgs/{0}/actions/runners".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runners for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runners for a repository.</returns>
        public static Uri ActionsListSelfHostedRunnersForRepository(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/runners".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <param name="runnerGroupId">The Id of the runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in an enterprise.</returns>
        public static Uri ActionsListSelfHostedRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId)
        {
            return "enterprises/{0}/actions/runner-groups/{1}/runners".FormatUri(enterprise, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <param name="runnerGroupId">The Id of the runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in an organization.</returns>
        public static Uri ActionsListSelfHostedRunnersForOrganizationRunnerGroup(string org, long runnerGroupId)
        {
            return "orgs/{0}/actions/runner-groups/{1}/runners".FormatUri(org, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runnerGroupId">The Id of the runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner for a runner group in a repository.</returns>
        public static Uri ActionsListSelfHostedRunnersForRepositoryRunnerGroup(string owner, string repo, long runnerGroupId)
        {
            return "repos/{0}/{1}/actions/runner-groups/{2}/runners".FormatUri(owner, repo, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner applications for an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner applications for an enterprise.</returns>
        public static Uri ActionsListRunnerApplicationsForEnterprise(string enterprise)
        {
            return "enterprises/{0}/actions/runners/downloads".FormatUri(enterprise);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner applications for an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner applications for an organization.</returns>
        public static Uri ActionsListRunnerApplicationsForOrganization(string org)
        {
            return "orgs/{0}/actions/runners/downloads".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner applications for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner applications for a repository.</returns>
        public static Uri ActionsListRunnerApplicationsForRepository(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/runners/downloads".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner delete from an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <param name="runnerId">The Id of the runner.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner delete from an enterprise.</returns>
        public static Uri ActionsDeleteEnterpriseRunner(string enterprise, long runnerId)
        {
            return "enterprises/{0}/actions/runners/{1}".FormatUri(enterprise, runnerId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner delete from an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <param name="runnerId">The Id of the runner.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner delete from an organization.</returns>
        public static Uri ActionsDeleteOrganizationRunner(string org, long runnerId)
        {
            return "orgs/{0}/actions/runners/{1}".FormatUri(org, runnerId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner delete from a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <param name="runnerId">The Id of the runner.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner delete from a repository.</returns>
        public static Uri ActionsDeleteRepositoryRunner(string owner, string repo, long runnerId)
        {
            return "repos/{0}/{1}/actions/runners/{2}".FormatUri(owner, repo, runnerId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner registration token for an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner registration token for an enterprise.</returns>
        public static Uri ActionsCreateEnterpriseRegistrationToken(string enterprise)
        {
            return "enterprises/{0}/actions/runners/registration-token".FormatUri(enterprise);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner registration token for an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner registration token for an organization.</returns>
        public static Uri ActionsCreateOrganizationRegistrationToken(string org)
        {
            return "orgs/{0}/actions/runners/registration-token".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner registration token for a repository.
        /// </summary>
        /// <param name="owner">The owner of repo</param>
        /// <param name="repo">The name of repo</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner registration token for a repository.</returns>
        public static Uri ActionsCreateRepositoryRegistrationToken(string owner, string repo)
        {
            return "repos/{0}/{1}/actions/runners/registration-token".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner groups for an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner groups for an enterprise.</returns>
        public static Uri ActionsGetEnterpriseRunnerGroup(string enterprise, long runnerGroupId)
        {
            return "enterprises/{0}/actions/runner-groups/{1}".FormatUri(enterprise, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner groups for an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner groups for an organization.</returns>
        public static Uri ActionsGetOrganizationRunnerGroup(string org, long runnerGroupId)
        {
            return "orgs/{0}/actions/runner-groups/{1}".FormatUri(org, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner groups for an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner groups for an enterprise.</returns>
        public static Uri ActionsListEnterpriseRunnerGroups(string enterprise)
        {
            return "enterprises/{0}/actions/runner-groups".FormatUri(enterprise);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner groups for an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner groups for an organization.</returns>
        public static Uri ActionsListOrganizationRunnerGroups(string org)
        {
            return "orgs/{0}/actions/runner-groups".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner group organizations that belong to an enterprise.
        /// </summary>
        /// <param name="enterprise">The name of the enterprise.</param>
        /// <param name="runnerGroupId">The Id of the runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner group organizations that belong to an enterprise.</returns>
        public static Uri ActionsListEnterpriseRunnerGroupOrganizations(string enterprise, long runnerGroupId)
        {
            return "enterprises/{0}/actions/runner-groups/{1}/organizations".FormatUri(enterprise, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the Actions self-hosted runner group repositories that belong to an organization.
        /// </summary>
        /// <param name="org">The name of the organization.</param>
        /// <param name="runnerGroupId">The Id of the runner group.</param>
        /// <returns>The <see cref="Uri"/> that handles the Actions self-hosted runner group repositories that belong to an organization.</returns>
        public static Uri ActionsListOrganizationRunnerGroupRepositories(string org, long runnerGroupId)
        {
            return "orgs/{0}/actions/runner-groups/{1}/repositories".FormatUri(org, runnerGroupId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles the machine availability for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repo">The name of the repository.</param>
        /// <param name="reference">The reference to check the machine availability for.</param>
        public static Uri GetAvailableMachinesForRepo(string owner, string repo, string reference)
        {
            if (reference is null)
            {
                return "repos/{0}/{1}/codespaces/machines".FormatUri(owner, repo);
            }

            return "repos/{0}/{1}/actions/runners/availability?ref={3}".FormatUri(owner, repo, reference);
        }
        
        /// <summary>
        /// Returns the <see cref="Uri"/> that handles adding or removing of copilot licenses for an organisation
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns>A Uri Instance</returns>
        public static Uri CopilotBillingLicense(string org)
        {
            return $"orgs/{org}/copilot/billing/selected_users".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that handles reading copilot billing settings for an organization
        /// </summary>
        /// <param name="org">The name of the organization</param>
        /// <returns>A Uri Instance</returns>
        public static Uri CopilotBillingSettings(string org)
        {
            return $"orgs/{org}/copilot/billing".FormatUri(org);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that allows for searching across all licenses for an organisation
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public static Uri CopilotAllocatedLicenses(string org)
        {
            return $"orgs/{org}/copilot/billing/seats".FormatUri(org);
        }

        public static Uri Codespaces()
        {
            return _currentUserAllCodespaces;
        }

        public static Uri CodespacesForRepository(string owner, string repo)
        {
            return "repos/{0}/{1}/codespaces".FormatUri(owner, repo);
        }

        public static Uri Codespace(string codespaceName)
        {
            return "user/codespaces/{0}".FormatUri(codespaceName);
        }

        public static Uri CodespaceStart(string codespaceName)
        {
            return "user/codespaces/{0}/start".FormatUri(codespaceName);
        }

        public static Uri CodespaceStop(string codespaceName)
        {
            return "user/codespaces/{0}/stop".FormatUri(codespaceName);
        }

        public static Uri CreateCodespace(string owner, string repo)
        {
            return "repos/{0}/{1}/codespaces".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that lists the artifacts for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <returns>A Uri Instance</returns>
        public static Uri ListArtifacts(string owner, string repository)
        {
            return "repos/{0}/{1}/actions/artifacts".FormatUri(owner, repository);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the specified artifact.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <param name="artifactId">The id of the artifact</param>
        /// <returns>A Uri Instance</returns>
        public static Uri Artifact(string owner, string repository, long artifactId)
        {
            return "repos/{0}/{1}/actions/artifacts/{2}".FormatUri(owner, repository, artifactId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to download the specified artifact.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <param name="artifactId">The id of the artifact</param>
        /// <param name="archiveFormat">The archive format e.g. zip</param>
        /// <returns>A Uri Instance</returns>
        public static Uri DownloadArtifact(string owner, string repository, long artifactId, string archiveFormat)
        {
            return "repos/{0}/{1}/actions/artifacts/{2}/{3}".FormatUri(owner, repository, artifactId, archiveFormat);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to list the artifacts for a workflow.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <param name="runId">The id of the workflow run</param>
        /// <returns>A Uri Instance</returns>
        public static Uri ListWorkflowArtifacts(string owner, string repository, long runId)
        {
            return "repos/{0}/{1}/actions/runs/{2}/artifacts".FormatUri(owner, repository, runId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to rename a repository branch.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <param name="branch">The name of the branch to rename</param>
        /// <returns>A Uri Instance</returns>
        public static Uri RepositoryBranchRename(string owner, string repository, string branch)
        {
            return "repos/{0}/{1}/branches/{2}/rename".FormatUri(owner, repository, branch);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to get or set an organization OIDC subject claim.
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <returns>A Uri Instance</returns>
        public static Uri ActionsOrganizationOidcSubjectClaim(string organization)
        {
            return "orgs/{0}/actions/oidc/customization/sub".FormatUri(organization);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to get or set a repository OIDC subject claim.
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <returns>A Uri Instance</returns>
        public static Uri ActionsRepositoryOidcSubjectClaim(string owner, string repository)
        {
            return "repos/{0}/{1}/actions/oidc/customization/sub".FormatUri(owner, repository);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to create an autolink
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>A Uri Instance</returns>
        public static Uri AutolinksCreate(string owner, string repo)
        {
            return "repos/{0}/{1}/autolinks".FormatUri(owner, repo);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to delete an autolink
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="autolinkId">The unique identifier of the autolink</param>
        /// <returns>A Uri Instance</returns>
        public static Uri AutolinksDelete(string owner, string repo, int autolinkId)
        {
            return "repos/{0}/{1}/autolinks/{2}".FormatUri(owner, repo, autolinkId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to get an autolink
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="autolinkId">The unique identifier of the autolink</param>
        /// <returns>A Uri Instance</returns>
        public static Uri AutolinksGet(string owner, string repo, int autolinkId)
        {
            return "repos/{0}/{1}/autolinks/{2}".FormatUri(owner, repo, autolinkId);
        }

        /// <summary>
        ///  Returns the <see cref="Uri"/> to get a list of autolinks configured for the given repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>A Uri Instance</returns>
        public static Uri AutolinksGetAll(string owner, string repo)
        {
            return "repos/{0}/{1}/autolinks".FormatUri(owner, repo);
        }
    }
}
