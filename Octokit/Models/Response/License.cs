﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class License : LicenseMetadata
    {
        public License(
            string key,
            string name,
            string spdxId,
            string url,
            string htmlUrl,
            bool featured,
            string description,
            string category,
            string implementation,
            string body,
            IEnumerable<string> required,
            IEnumerable<string> permitted,
            IEnumerable<string> forbidden) : base(key, name, spdxId, url, featured)
        {
            Ensure.ArgumentNotNull(htmlUrl, nameof(htmlUrl));
            Ensure.ArgumentNotNull(description, nameof(description));
            Ensure.ArgumentNotNull(category, nameof(category));
            Ensure.ArgumentNotNull(implementation, nameof(implementation));
            Ensure.ArgumentNotNull(body, nameof(body));
            Ensure.ArgumentNotNull(required, nameof(required));
            Ensure.ArgumentNotNull(permitted, nameof(permitted));
            Ensure.ArgumentNotNull(forbidden, nameof(forbidden));

            HtmlUrl = htmlUrl;
            Description = description;
            Category = category;
            Implementation = implementation;
            Body = body;
            Required = new ReadOnlyCollection<string>(required.ToList());
            Permitted = new ReadOnlyCollection<string>(permitted.ToList());
            Forbidden = new ReadOnlyCollection<string>(forbidden.ToList());
        }

        public License()
        {
        }

        /// <summary>
        /// Url to the license on https://choosealicense.com
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// A description of the license.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// A group or family that the license belongs to such as the GPL family of licenses.
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        /// Notes on how to properly apply the license.
        /// </summary>
        public string Implementation { get; protected set; }

        /// <summary>
        /// Set of codes for what is required under the terms of the license. For example, "include-copyright"
        /// </summary>
        public IReadOnlyList<string> Required { get; protected set; }

        /// <summary>
        /// Set of codes for what is permitted under the terms of the license. For example, "commercial-use"
        /// </summary>
        public IReadOnlyList<string> Permitted { get; protected set; }

        /// <summary>
        /// Set of codes for what is forbidden under the terms of the license. For example, "no-liability"
        /// </summary>
        public IReadOnlyList<string> Forbidden { get; protected set; }

        /// <summary>
        /// The text of the license
        /// </summary>
        public string Body { get; protected set; }

        internal override string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0} Category: {1}", base.DebuggerDisplay, Category);
            }
        }
    }
}
