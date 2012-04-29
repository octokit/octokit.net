using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    public interface IGitHubModel
    {
        ApiInfo ApiInfo { get; }
    }

    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public string[] OauthScopes { get; set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public string[] AcceptedOauthScopes { get; set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Rate limit in requests/hr.
        /// </summary>
        public int RateLimit { get; set; }

        /// <summary>
        /// Number of calls remaining before hitting the rate limit.
        /// </summary>
        public int RateLimitRemaining { get; set; }
    }

    /// <summary>
    /// A user on GitHub.
    /// </summary>
    [DebuggerDisplay("{Login} ({Name})")]
    public class User : IGitHubModel
    {
        public User()
        {
            ApiInfo = new ApiInfo();
        }

        public ApiInfo ApiInfo { get; private set; }

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
        /// The type of this record. (User for user account, Organization for org account)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The api URL for this user.
        /// </summary>
        public string Url { get; set; }
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
}
