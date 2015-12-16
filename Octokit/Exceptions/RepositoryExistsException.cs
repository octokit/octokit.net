using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

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
        readonly string _message;

        /// <summary>
        /// Constructs an instance of RepositoryExistsException for an organization.
        /// </summary>
        /// <param name="organization">The name of the organization of the existing repository</param>
        /// <param name="name">The name of the existing repository</param>
        /// <param name="baseAddress">The base address of the repository.</param>
        /// <param name="innerException">The inner validation exception.</param>
        public RepositoryExistsException(
            string organization,
            string name,
            Uri baseAddress,
            ApiValidationException innerException)
            : base(innerException)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            Organization = organization;
            RepositoryName = name;
            OwnerIsOrganization = true;
            var webBaseAddress = baseAddress.Host != GitHubClient.GitHubApiUrl.Host
                        ? baseAddress
                        : GitHubClient.GitHubDotComUrl;
            ExistingRepositoryWebUrl = new Uri(webBaseAddress, new Uri(organization + "/" + name, UriKind.Relative));

            _message = string.Format(CultureInfo.InvariantCulture, "There is already a repository named '{0}' in the organization '{1}'.", name, organization);
        }

        /// <summary>
        /// Constructs an instance of RepositoryExistsException for an account.
        /// </summary>
        /// <param name="name">The name of the existing repository</param>
        /// <param name="innerException">The inner validation exception</param>
        public RepositoryExistsException(
            string name,
            ApiValidationException innerException)
            : base(innerException)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            RepositoryName = name;

            _message = string.Format(CultureInfo.InvariantCulture, "There is already a repository named '{0}' for the current account.", name);
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
        /// The login of the organization of the repository.
        /// </summary>
        public string Organization { get; private set; }

        /// <summary>
        /// True if the owner is an organization and not the user.
        /// </summary>
        public bool OwnerIsOrganization { get; private set; }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of RepositoryExistsException.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected RepositoryExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            _message = info.GetString("Message");
            RepositoryName = info.GetString("RepositoryName");
            Organization = info.GetString("Organization");
            OwnerIsOrganization = info.GetBoolean("OwnerIsOrganization");
            ExistingRepositoryWebUrl = (Uri)(info.GetValue("ExistingRepositoryWebUrl", typeof(Uri)));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Message", Message);
            info.AddValue("RepositoryName", RepositoryName);
            info.AddValue("Organization", Organization);
            info.AddValue("OwnerIsOrganization", OwnerIsOrganization);
            info.AddValue("ExistingRepositoryWebUrl", ExistingRepositoryWebUrl);
        }
#endif
    }
}
