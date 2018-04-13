using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Describes a pre-receive environment.
    /// </summary>
    public class PreReceiveEnvironment
    {
        /// <summary>
        /// Identifier for the pre-receive environment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the environment as displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to the pre-receive environment.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// URL to the tarball that will be downloaded and extracted.
        /// </summary>
        [Parameter(Key = "image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// UI URL to the pre-receive environment.
        /// </summary>
        [Parameter(Key = "html_url")]
        public string HtmlUrl { get; set; }

        /// <summary>
        /// Whether this is the default environment that ships with GitHub Enterprise.
        /// </summary>
        [Parameter(Key = "default_environment")]
        public bool DefaultEnvironment { get; set; }

        /// <summary>
        /// The time when the pre-receive environment was created.
        /// </summary>
        [Parameter(Key = "created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The number of pre-receive hooks that use this environment.
        /// </summary>
        [Parameter(Key = "hooks_count")]
        public int HooksCount { get; set; }

        /// <summary>
        /// This environment's download status.
        /// </summary>
        public PreReceiveEnvironmentDownload Download { get; set; }

        /// <summary>
        /// Prepares an <see cref="UpdatePreReceiveEnvironment"/> for use when updating a pre-receive environment.
        /// </summary>
        /// <returns></returns>
        public UpdatePreReceiveEnvironment ToUpdate()
        {
            return new UpdatePreReceiveEnvironment(Name, ImageUrl);
        }
    }
}