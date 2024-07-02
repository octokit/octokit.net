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
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(Values))]
    public class CustomPropertyValue
    {
        public CustomPropertyValue() { }

        public CustomPropertyValue(string propertyName, object value)
        {
            PropertyName = propertyName;
            this.value = value;
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        [Parameter(Key = "property_name")]
        public string PropertyName { get; private set; }


        [Parameter(Key = "value")]
        public object value { get; private set; }

        /// <summary>
        /// The value assigned to the property
        /// </summary>
        public string Value {
            get {
                if (value is null)
                {
                    return null;
                }
                if (value is string stringValue)
                {
                    return stringValue;
                }
                else if (value is JsonArray stringValues)
                {
                    return "[" + string.Join(",", stringValues.ConvertAll(x => x.ToString())) + "]";
                }

                return null;
            }
        }

        /// <summary>
        /// The array of values assigned to the property
        /// </summary>
        public IReadOnlyList<string> Values {
            get {
                if (value is null)
                {
                    return null;
                }
                else if (value is string stringValue)
                {
                    return new List<string> { stringValue };
                }
                else if (value is JsonArray stringValues)
                {
                    return stringValues.ConvertAll(x => x.ToString());
                }

                return null;
            }
        }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
