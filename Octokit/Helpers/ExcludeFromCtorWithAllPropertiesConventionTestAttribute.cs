using System;
using System.Linq;

namespace Octokit
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeFromCtorWithAllPropertiesConventionTestAttribute : Attribute
    {
        public ExcludeFromCtorWithAllPropertiesConventionTestAttribute(params string[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (properties.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(properties));
            }

            if (properties.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Cannot contain empty items.", nameof(properties));
            }

            if (properties.Distinct(StringComparer.InvariantCultureIgnoreCase).Count() != properties.Length)
            {
                throw new ArgumentException("Cannot contain duplicates.", nameof(properties));
            }

            Properties = properties;
        }

        public string[] Properties { get; private set; }
    }
}
