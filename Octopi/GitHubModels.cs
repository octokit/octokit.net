using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi
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
    public class User
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
        public long Id { get; set; }

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
        /// The type of this record. (User for user account, Organization for org account)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }

        /// <summary>
        /// The api URL for this user.
        /// </summary>
        public string Url { get; set; }
    }

    public class Organization
    {
        /// <summary>
        /// URL for this user's avatar.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The system-wide unique Id for this user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// This org's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The api URL for this organization.
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
        public long Id { get; set; }

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
        public long Space { get; set; }
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
        public long Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string Language { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsFork { get; set; }
        public int ForksCount { get; set; }
        public int WatchersCount { get; set; }
        public long Size { get; set; }
        public string MasterBranch { get; set; }
        public int OpenIssuesCount { get; set; }
        public DateTimeOffset PushedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public User Organization { get; set; }
        public Repository Parent { get; set; }
        public Repository Source { get; set; }
        public bool HasIssues { get; set; }
        public bool HasWiki { get; set; }
        public bool HasDownloads { get; set; }

        public int NetworkCount { get; set; }
    }
}
