using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(DefaultValues))]
    public class OrganizationCustomProperty
    {
        public OrganizationCustomProperty() { }

        public OrganizationCustomProperty(string propertyName, CustomPropertyValueType valueType, bool required, object defaultValue, string description, IReadOnlyList<string> allowedValues, CustomPropertyValuesEditableBy? valuesEditableBy)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Required = required;
            this.defaultValue = defaultValue;
            Description = description;
            AllowedValues = allowedValues;
            ValuesEditableBy = valuesEditableBy;
        }

        [Parameter(Key = "default_value")]
        public object defaultValue { get; private set; }

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
        public string DefaultValue {
            get {
                if (defaultValue is null)
                {
                    return null;
                }
                if (defaultValue is string stringValue)
                {
                    return stringValue;
                }
                else if (defaultValue is JsonArray stringValues)
                {
                    return "[" + string.Join(",", stringValues.ConvertAll(x => x.ToString())) + "]";
                }

                return null;
            }
        }

        /// <summary>
        /// Default values of the property
        /// </summary>
        public IReadOnlyList<string> DefaultValues {
            get {
                if (defaultValue is null)
                {
                    return null;
                }
                else if (defaultValue is string stringValue)
                {
                    return new List<string> { stringValue };
                }
                else if (defaultValue is JsonArray stringValues)
                {
                    return stringValues.ConvertAll(x => x.ToString());
                }

                return null;
            }
        }

        /// <summary>
        /// Short description of the property
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// An ordered list of the allowed values of the property.
        /// The property can have up to 200 allowed values.
        /// </summary>
        public IReadOnlyList<string> AllowedValues { get; private set; }

        /// <summary>
        /// Who can edit the values of the property
        /// </summary>
        public StringEnum<CustomPropertyValuesEditableBy>? ValuesEditableBy { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "PropertyName: {0}", PropertyName);
    }
}
