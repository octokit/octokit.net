using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create or update a custom property value for a repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertOrganizationCustomProperty
    {
        public UpsertOrganizationCustomProperty() { }

        public UpsertOrganizationCustomProperty(CustomPropertyValueType valueType)
        {
            ValueType = valueType;
        }

        public UpsertOrganizationCustomProperty(CustomPropertyValueType valueType, string defaultValue)
        {
            ValueType = valueType;
            Required = true;
            DefaultValue = defaultValue;
        }

        public UpsertOrganizationCustomProperty(CustomPropertyValueType valueType, IReadOnlyList<string> defaultValue)
        {
            ValueType = valueType;
            Required = true;
            DefaultValue = defaultValue;
        }

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

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "ValueType: {0}", ValueType.DebuggerDisplay);
    }
}
