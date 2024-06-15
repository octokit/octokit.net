using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationCustomProperty
    {
        public OrganizationCustomProperty() { }

        public OrganizationCustomProperty(string propertyName, CustomPropertyValueType valueType, bool required, IReadOnlyList<string> defaultValue, string description, IReadOnlyList<string> allowedValues, CustomPropertyValuesEditableBy? valuesEditableBy)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Required = required;
            DefaultValues = defaultValue;
            Description = description;
            AllowedValues = allowedValues;
            ValuesEditableBy = valuesEditableBy;
        }

        public OrganizationCustomProperty(string propertyName, CustomPropertyValueType valueType, bool required, string defaultValue, string description, IReadOnlyList<string> allowedValues, CustomPropertyValuesEditableBy? valuesEditableBy)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Required = required;
            DefaultValue = defaultValue;
            Description = description;
            AllowedValues = allowedValues;
            ValuesEditableBy = valuesEditableBy;
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The type of the value for the property
        /// </summary>
        public StringEnum<CustomPropertyValueType>? ValueType { get; private set; }

        /// <summary>
        /// Whether the property is required
        /// </summary>
        public bool Required { get; private set; }

        /// <summary>
        /// Default value of the property
        /// </summary>
        public string DefaultValue { get; private set; }

        /// <summary>
        /// Default values of the property
        /// </summary>
        public IReadOnlyList<string> DefaultValues { get; private set; }

        /// <summary>
        /// Short description of the property
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// An ordered list of the allowed values of the property.
        /// The property can have up to 200 allowed values.
        /// </summary>
        public IEnumerable<string> AllowedValues { get; private set; }

        /// <summary>
        /// Who can edit the values of the property
        /// </summary>
        public StringEnum<CustomPropertyValuesEditableBy>? ValuesEditableBy { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
