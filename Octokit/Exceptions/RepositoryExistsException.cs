using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when creating a repository, but it already exists on the server.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class RepositoryExistsException : ApiValidationException
    {
        string _message;

        /// <summary>
        /// Constructs an instance of RepositoryExistsException.
        /// </summary>
        /// <param name="owner">The login of the owner of the existing repository</param>
        /// <param name="name">The name of the existing repository</param>
        /// <param name="baseAddress">The base address of the repository.</param>
        /// <param name="innerException">The inner validation exception.</param>
        public RepositoryExistsException(
            string owner,
            string name,
            Uri baseAddress,
            ApiValidationException innerException)
            : base(innerException)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "repositoryName");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            Owner = owner;
            RepositoryName = name;
            OwnerIsOrganization = !String.IsNullOrWhiteSpace(owner);
            var webBaseAddress = baseAddress.Host != GitHubClient.GitHubApiUrl.Host
                        ? baseAddress
                        : GitHubClient.GitHubDotComUrl;
            ExistingRepositoryWebUrl = OwnerIsOrganization 
                ? new Uri(webBaseAddress, new Uri(owner + "/" + name, UriKind.Relative))
                : null;
            string messageFormat = OwnerIsOrganization 
                ? "There is already a repository named '{0}' in the organization '{1}'."
                : "There is already a repository named '{0}' for the current account.";

            _message = String.Format(CultureInfo.InvariantCulture, messageFormat, name, owner);
        }

        /// <summary>
        /// The Name of the repository that already exists.
        /// </summary>
        public string RepositoryName { get; private set; }

        /// <summary>
        /// The URL to the existing repository's web page on github.com (or enterprise instance).
        /// </summary>
        public Uri ExistingRepositoryWebUrl { get; set; }

        /// <summary>
        /// A useful default error message.
        /// </summary>
        public override string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// The login of the owner of the repository.
        /// </summary>
        public string Owner { get; private set; }

        /// <summary>
        /// True if the owner is an organization and not the user.
        /// </summary>
        public bool OwnerIsOrganization { get; private set; }

#if !NETFX_CORE
        protected RepositoryExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            _message = info.GetString("Message"); 
            RepositoryName = info.GetString("RepositoryName");
            Owner = info.GetString("Owner");
            OwnerIsOrganization = info.GetBoolean("OwnerIsOrganization"); 
            ExistingRepositoryWebUrl = (Uri)(info.GetValue("ExistingRepositoryWebUrl", typeof(Uri)));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Message", Message);
            info.AddValue("RepositoryName", RepositoryName);
            info.AddValue("Owner", Owner);
            info.AddValue("OwnerIsOrganization", OwnerIsOrganization);
            info.AddValue("ExistingRepositoryWebUrl", ExistingRepositoryWebUrl);
        }
#endif
    }
}
