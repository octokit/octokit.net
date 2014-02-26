using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewGist
    {
        public NewGist()
        {
            Files = new Dictionary<string, string>();
        }

        /// <summary>
        /// The description of the gist.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the gist is public
        /// </summary>
        public bool Public { get; set; }
        
        /// <summary>
        /// Files that make up this gist using the key as Filename
        /// and value as Content
        /// </summary>
        public IDictionary<string, string> Files { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }
}