using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationCustomPropertyUpdate
    {
        public OrganizationCustomPropertyUpdate() { }

        public OrganizationCustomPropertyUpdate(string propertyName, CustomPropertyValueType valueType, string defaultValue)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Required = true;
            DefaultValue = defaultValue;
        }

        public OrganizationCustomPropertyUpdate(string propertyName, CustomPropertyValueType valueType, IReadOnlyList<string> defaultValue)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Required = true;
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The type of the value for the property
        /// </summary>
        public StringEnum<CustomPropertyValueType> ValueType { get; set; }

        /// <summary>
        /// Whether the property is required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Default value of the property
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Short description of the property
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An ordered list of the allowed values of the property.
        /// The property can have up to 200 allowed values.
        /// </summary>
        public IEnumerable<string> AllowedValues { get; set; }

        /// <summary>
        /// Who can edit the values of the property
        /// </summary>
        public StringEnum<CustomPropertyValuesEditableBy>? ValuesEditableBy { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
