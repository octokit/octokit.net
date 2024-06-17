using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Custom property name and associated value
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CustomPropertyValueUpdate
    {
        public CustomPropertyValueUpdate() { }

        public CustomPropertyValueUpdate(string propertyName, string value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public CustomPropertyValueUpdate(string propertyName, IReadOnlyList<string> value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The value assigned to the property
        /// </summary>
        [SerializeNull]
        [Parameter(Key = "value")]
        public object Value { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
