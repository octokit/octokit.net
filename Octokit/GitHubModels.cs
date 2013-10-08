using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Octokit.Http;

namespace Octokit
{
    /// <summary>
    /// Represents an oauth application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// <see cref="Application"/> Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url of this <see cref="Application"/>.
        /// </summary>
        public string Url { get; set; }
    }

    public class AuthorizationUpdate
    {
        /// <summary>
        /// Replace scopes with this list.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Special type of model object that only updates none-null fields.")]
        public string[] Scopes { get; set; }

        /// <summary>
        /// Notes about this particular <see cref="Authorization"/>.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// A url for more information about notes.
        /// </summary>
        public string NoteUrl { get; set; }
    }

    /// <summary>
    /// Represents an oauth access given to a particular application.
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// The Id of this <see cref="Authorization"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The API URL for this <see cref="Authorization"/>.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The <see cref="Application"/> that created this <see cref="Authorization"/>.
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// The oauth token (be careful with these, they are like passwords!).
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Notes about this particular <see cref="Authorization"/>.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// A url for more information about notes.
        /// </summary>
        public string NoteUrl { get; set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// The scopes that this <see cref="Authorization"/> has. This is the kind of access that the token allows.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Special type of model object that only updates none-null fields.")]
        public string[] Scopes { get; set; }

        public string ScopesDelimited
        {
            get { return string.Join(",", Scopes); }
        }
    }

    /// <summary>
    /// Represents updatable fields on a user. Values that are null will not be sent in the request.
    /// Use string.empty if you want to clear clear a value.
    /// </summary>
    public class UserUpdate
    {
        /// <summary>
        /// This user's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// URL for this user's blog.
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// The company this user's works for.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// This user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The geographic location of this user.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// This user's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tells if this user is currently hireable.
        /// </summary>
        public bool? Hireable { get; set; }
    }

    /// <summary>
    /// Represents a user on GitHub.
    /// </summary>
    public class User : Account
    {
        /// <summary>
        /// Hex Gravatar identifier
        /// </summary>
        public string GravatarId { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; set; }
    }

    public class Organization : Account
    {
        /// <summary>
        /// The billing address for an organization. This is only returned when updating 
        /// an organization.
        /// </summary>
        public string BillingAddress { get; set; }
    }

    public abstract class Account
    {
        /// <summary>
        /// URL for this user's avatar.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// This user's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// URL for this user's blog.
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// Number of collaborators this user has on their account.
        /// </summary>
        public int Collaborators { get; set; }

        /// <summary>
        /// The company this user's works for.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The date this user account was create.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The amout of disk space this user is currently using.
        /// </summary>
        public int DiskUsage { get; set; }

        /// <summary>
        /// This user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Number of follwers this user has.
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// Number of other users this user is following.
        /// </summary>
        public int Following { get; set; }

        /// <summary>
        /// Tells if this user is currently hireable.
        /// </summary>
        public bool Hireable { get; set; }

        /// <summary>
        /// The HTML URL for this user on github.com.
        /// </summary>
        public string HtmlUrl { get; set; }

        /// <summary>
        /// The system-wide unique Id for this user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The geographic location of this user.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// This user's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// This user's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of private repos owned by this user.
        /// </summary>
        public int OwnedPrivateRepos { get; set; }

        /// <summary>
        /// The plan this user pays for.
        /// </summary>
        public Plan Plan { get; set; }

        /// <summary>
        /// The number of private gists this user has created.
        /// </summary>
        public int PrivateGists { get; set; }

        /// <summary>
        /// The number of public gists this user has created.
        /// </summary>
        public int PublicGists { get; set; }

        /// <summary>
        /// The number of public repos owned by this user.
        /// </summary>
        public int PublicRepos { get; set; }

        /// <summary>
        /// The total number of private repos this user owns.
        /// </summary>
        public int TotalPrivateRepos { get; set; }

        /// <summary>
        /// The api URL for this user.
        /// </summary>
        public string Url { get; set; }
    }

    internal class ReadmeResponse
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public string HtmlUrl { get; set; }
        public string Url { get; set; }
        public string Encoding { get; set; }
    }

    public class Readme
    {
        readonly Lazy<Task<string>> htmlContent;

        internal Readme(ReadmeResponse response, IApiConnection<Repository> client)
        {
            Ensure.ArgumentNotNull(response, "response");
            Ensure.ArgumentNotNull(client, "client");

            Name = response.Name;
            Url = new Uri(response.Url);
            HtmlUrl = new Uri(response.HtmlUrl);
            if (response.Encoding.Equals("base64", StringComparison.OrdinalIgnoreCase))
            {
                var contentAsBytes = Convert.FromBase64String(response.Content);
                Content = Encoding.UTF8.GetString(contentAsBytes, 0, contentAsBytes.Length);
            }
            htmlContent = new Lazy<Task<string>>(async () => await client.GetHtml(HtmlUrl));
        }

        public string Content { get; private set; }
        public string Name { get; private set; }
        public Uri HtmlUrl { get; private set; }
        public Uri Url { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makse a network request")]
        public async Task<string> GetHtmlContent()
        {
            return await htmlContent.Value;
        }
    }

    public class SshKey
    {
        /// <summary>
        /// The system-wide unique Id for this user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The SSH Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The title of the SSH key
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The api URL for this organization.
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents the data and name parsed from the Ssh key.
    /// </summary>
    public class SshKeyInfo
    {
        public SshKeyInfo(string data, string name)
        {
            Ensure.ArgumentNotNull(data, "data");
            Ensure.ArgumentNotNull(name, "name");

            Data = data;
            Name = name;
        }

        public string Data { get; private set; }
        public string Name { get; private set; }
    }

    public class SshKeyUpdate
    {
        /// <summary>
        /// The SSH Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The title of the SSH key
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// A plan (either paid or free) for a particular user
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// The number of collaborators allowed with this plan.
        /// </summary>
        public int Collaborators { get; set; }

        /// <summary>
        /// The name of the plan.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of private repositories allowed with this plan.
        /// </summary>
        public int PrivateRepos { get; set; }

        /// <summary>
        /// The amount of disk space allowed with this plan.
        /// </summary>
        public int Space { get; set; }

        /// <summary>
        /// The billing email for the organization. Only has a value in response to editing an organization.
        /// </summary>
        public string BillingEmail { get; set; }
    }

    public class Repository
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string GitUrl { get; set; }
        public string SshUrl { get; set; }
        public string SvnUrl { get; set; }
        public string MirrorUrl { get; set; }
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string Language { get; set; }
        public bool Private { get; set; }
        public bool Fork { get; set; }
        public int ForksCount { get; set; }
        public int WatchersCount { get; set; }
        public string MasterBranch { get; set; }
        public int OpenIssuesCount { get; set; }
        public DateTimeOffset? PushedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public User Organization { get; set; }
        public Repository Parent { get; set; }
        public Repository Source { get; set; }
        public bool HasIssues { get; set; }
        public bool HasWiki { get; set; }
        public bool HasDownloads { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }

        public bool Verified { get; set; }

        public bool Primary { get; set; }
    }

    public class Release
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string AssetsUrl { get; set; }
        public string UploadUrl { get; set; }
        public int Id { get; set; }
        public string TagName { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Commitish")]
        public string TargetCommitish { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
    }

    public class ReleaseUpdate
    {
        public ReleaseUpdate(string tagName)
        {
            Ensure.ArgumentNotNullOrEmptyString(tagName, "tagName");
            TagName = tagName;
        }

        public string TagName { get; private set; }
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Commitish")]
        public string TargetCommitish { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }
    }

    public class ReleaseAsset
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string State { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public int DownloadCount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public class ReleaseAssetUpload
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream RawData { get; set; }
    }

    public class ApiError
    {
        public string Message { get; set; }

        // TODO: This ought to be an IReadOnlyList<ApiErrorDetail> but we need to add support to SimpleJson for that.
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<ApiErrorDetail> Errors { get; set; }
    }

    public class ApiErrorDetail
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public string Field { get; set; }

        public string Resource { get; set; }
    }

    /// <summary>
    /// Describes a new repository to create via the <see cref="IRepositoriesClient.Create"/> method.
    /// </summary>
    public class NewRepository
    {
        /// <summary>
        /// Optional. Gets or sets whether to create an initial commit with empty README. The default is false.
        /// </summary>
        public bool? AutoInit { get; set; }

        /// <summary>
        /// Required. Gets or sets the new repository's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable downloads for the new repository. The default is true.
        /// </summary>
        public bool? HasDownloads { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable issues for the new repository. The default is true.
        /// </summary>
        public bool? HasIssues { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable the wiki for the new repository. The default is true.
        /// </summary>
        public bool? HasWiki { get; set; }

        /// <summary>
        /// Optional. Gets or sets the new repository's optional website.
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// Optional. Gets or sets the desired language's or platform's .gitignore template to apply. Use the name of the template without the extension; "Haskell", for example. Ignored if <see cref="AutoInit"/> is null or false.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gitignore", Justification = "It needs to be this way for proper serialization.")]
        public string GitignoreTemplate { get; set; }

        /// <summary>
        /// Required. Gets or sets the new repository's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository is private; the default is false.
        /// </summary>
        public bool? Private { get; set; }

        /// <summary>
        /// Optional. Gets or sets the ID of the team to grant access to this repository. This is only valid when creating a repository for an organization.
        /// </summary>
        public int? TeamId { get; set; }
    }
}
