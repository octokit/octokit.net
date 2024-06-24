using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create or update custom property values for a repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertRepositoryCustomPropertyValues
    {
        /// <summary>
        /// List of custom property names and associated values
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#create-or-update-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        [Parameter(Value = "properties")]
        public List<CustomPropertyValueUpdate> Properties { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Count: {0}", Properties?.Count);
            }
        }
    }
}
