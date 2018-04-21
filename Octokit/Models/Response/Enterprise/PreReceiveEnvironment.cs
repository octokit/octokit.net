using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a pre-receive environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PreReceiveEnvironment
    {
        public PreReceiveEnvironment()
        { }

        public PreReceiveEnvironment(long id, string name, string url, string imageUrl, string htmlUrl, bool defaultEnvironment, DateTimeOffset? createdAt, int hooksCount, PreReceiveEnvironmentDownload download)
        {
            Id = id;
            Name = name;
            Url = url;
            ImageUrl = imageUrl;
            HtmlUrl = htmlUrl;
            DefaultEnvironment = defaultEnvironment;
            CreatedAt = createdAt;
            HooksCount = hooksCount;
            Download = download;
        }

        /// <summary>
        /// Identifier for the pre-receive environment.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The name of the environment as displayed in the UI.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// URL to the pre-receive environment.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// URL to the tarball that will be downloaded and extracted.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// UI URL to the pre-receive environment.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// Whether this is the default environment that ships with GitHub Enterprise.
        /// </summary>
        public bool DefaultEnvironment { get; protected set; }

        /// <summary>
        /// The time when the pre-receive environment was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; protected set; }

        /// <summary>
        /// The number of pre-receive hooks that use this environment.
        /// </summary>
        public int HooksCount { get; protected set; }

        /// <summary>
        /// This environment's download status.
        /// </summary>
        public PreReceiveEnvironmentDownload Download { get; protected set; }

        /// <summary>
        /// Prepares an <see cref="UpdatePreReceiveEnvironment"/> for use when updating a pre-receive environment.
        /// </summary>
        /// <returns></returns>
        public UpdatePreReceiveEnvironment ToUpdate()
        {
            return new UpdatePreReceiveEnvironment
            {
                Name = Name,
                ImageUrl = ImageUrl
            };
        }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1} ImageUrl: {2}", Id, Name, ImageUrl); }
        }
    }
}
