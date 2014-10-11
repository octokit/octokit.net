using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A new hook description.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewHook"/> class.
        /// </summary>
        public NewHook()
        {
            Events = new List<string>();
            Config = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewHook"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public NewHook(string name, bool active) : this()
        {
            Name = name;
            Active = active;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewHook"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="events">The events.</param>
        /// <param name="config">The configuration.</param>
        public NewHook(string name, bool active, ICollection<string> events, IDictionary<string, object> config)
        {
            Name = name;
            Active = active;
            Events = events;
            Config = config;
        }

        /// <summary>
        /// The name of this hook.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A flag indicating if this hook is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The events subscribed by this hook.
        /// </summary>
        public ICollection<string> Events { get; private set; }

        /// <summary>
        /// The configuration for this hook.
        /// </summary>
        public IDictionary<string, object> Config { get; private set; }

        /// <summary>
        /// Creates a new webhook NewHook request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#create-a-hook</remarks>
        /// <param name="url">The URL.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="events">The events.</param>
        /// <returns>A NewHook that can be used with <see cref="IRepositoryHooksClient.Create"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public static NewHook CreateWeb(string url, string contentType, params string[] events)
        {
            return new NewHook("web", true, new Collection<string>(events), new Dictionary<string, object>() { { "url", url }, { "content_type", contentType } });
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name {0} Active:{1} Events: {2}", Name, Active, string.Join(", ", Events));
            }
        }
    }
}