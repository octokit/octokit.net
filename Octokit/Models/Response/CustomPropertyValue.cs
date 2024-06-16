using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Custom property name and associated value
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CustomPropertyValue
    {
        public CustomPropertyValue() { }

        public CustomPropertyValue(string propertyName, string value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public CustomPropertyValue(string propertyName, IReadOnlyList<string> value)
        {
            PropertyName = propertyName;
            Values = value;
        }

        public CustomPropertyValue(string propertyName, string value, IReadOnlyList<string> values)
        {
            PropertyName = propertyName;
            Value = value;
            Values = values;
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The value assigned to the property
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The values assigned to the property
        /// </summary>
        public IReadOnlyList<string> Values { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
