using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryHook
    {
        public NewRepositoryHook(string name, dynamic config)
        {
            Name = name;
            Config = config;
        }

        [Parameter(Key = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Is a key value structure determined by the web hook being created
        /// </summary>
        [Parameter(Key = "config")]
        public dynamic Config { get; set; }

        [Parameter(Key = "events")]
        public IEnumerable<string> Events { get; set; }

        [Parameter(Key = "active")]
        public bool Active { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0}, Events: {1}", Name, string.Join(", ", Events));
            }
        }
    }
}
