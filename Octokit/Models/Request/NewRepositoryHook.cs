using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Creates a Webhook for the repository.
    /// </summary>
    /// <remarks>
    /// To create a webhook, the following fields are required by the config:
    /// <list type="bullet">
    /// <item>
    ///   <term>url</term>
    ///   <description>A required string defining the URL to which the payloads will be delivered.</description>
    /// </item>
    /// <item>
    ///   <term>content_type</term>
    ///   <description>
    ///     An optional string defining the media type used to serialize the payloads. Supported values include json and
    ///     form. The default is form.
    ///   </description>
    /// </item>
    /// <item>
    ///   <term>secret</term>
    ///   <description>
    ///     An optional string that’s passed with the HTTP requests as an X-Hub-Signature header. The value of this
    ///     header is computed as the HMAC hex digest of the body, using the secret as the key.
    ///   </description>
    /// </item>
    /// <item>
    ///   <term>insecure_ssl:</term>
    ///   <description>
    ///     An optional string that determines whether the SSL certificate of the host for url will be verified when 
    ///     delivering payloads. Supported values include "0" (verification is performed) and "1" (verification is not 
    ///     performed). The default is "0".
    ///   </description>
    /// </item>
    /// </list>
    /// <para>
    /// API: https://developer.github.com/v3/repos/hooks/#create-a-hook
    /// </para>
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewRepositoryHook"/> class.
        /// </summary>
        /// <param name="name">
        /// Use "web" for a webhook or use the name of a valid service. (See 
        /// <see href="https://api.github.com/hooks">https://api.github.com/hooks</see> for the list of valid service
        /// names.)
        /// </param>
        /// <param name="config">
        /// Key/value pairs to provide settings for this hook. These settings vary between the services and are
        /// defined in the github-services repository. Booleans are stored internally as “1” for true, and “0” for
        /// false. Any JSON true/false values will be converted automatically.
        /// </param>
        public NewRepositoryHook(string name, IReadOnlyDictionary<string, string> config)
        {
            Name = name;
            Config = config;
        }

        /// <summary>
        /// Gets the name of the hook to create. Use "web" for a webhook or use the name of a valid service. (See 
        /// <see href="https://api.github.com/hooks">https://api.github.com/hooks</see> for the list of valid service
        /// names.)
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Key/value pairs to provide settings for this hook. These settings vary between the services and are
        /// defined in the github-services repository. Booleans are stored internally as “1” for true, and “0” for
        /// false. Any JSON true/false values will be converted automatically.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IReadOnlyDictionary<string, string> Config { get; private set; }

        /// <summary>
        /// Determines what events the hook is triggered for. Default: ["push"]
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public IEnumerable<string> Events { get; set; }

        /// <summary>
        /// Determines whether the hook is actually triggered on pushes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        public virtual NewRepositoryHook ToRequest()
        {
            return this;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0}, Events: {1}", Name, string.Join(", ", Events));
            }
        }
    }
}
