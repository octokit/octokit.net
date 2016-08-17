using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Base class for classes which represent UrlFormEncoded parameters to certain API endpoints.
    /// </summary>
    public abstract class FormUrlEncodedParameters
    {
        /// <summary>
        /// Converts the derived object into a UrlFormEncoded parameter string containing named parameters and their json serialized values
        /// This format is required for particular API calls (eg the GitHub Enterprise Management Console API) that take a parameter formatted as json but not in the request body
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public string ToFormUrlEncodedParameterString()
        {
            var parameters = new List<string>();
            foreach (var prop in GetPropertyParametersForType(this.GetType()))
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "{0}={1}", prop.Key, prop.GetValue(this)));
            }

            return string.Join("&", parameters);
        }

        static List<JsonParameter> GetPropertyParametersForType(Type type)
        {
            return type.GetAllProperties()
                .Where(p => p.Name != "DebuggerDisplay")
                .Select(p => new JsonParameter(p))
                .ToList();
        }

        class JsonParameter
        {
            readonly PropertyInfo _property;
            public JsonParameter(PropertyInfo property)
            {
                _property = property;
                Key = GetParameterKeyFromProperty(property);
            }

            public string Key { get; private set; }

            public string GetValue(object instance)
            {
                var value = _property.GetValue(instance, null);
                return value != null ? new SimpleJsonSerializer().Serialize(value) : null;
            }

            [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
                Justification = "GitHub API depends on lower case strings")]
            static string GetParameterKeyFromProperty(PropertyInfo property)
            {
                var attribute = property.GetCustomAttributes(typeof(ParameterAttribute), false)
                    .Cast<ParameterAttribute>()
                    .FirstOrDefault(attr => attr.Key != null);

                return attribute == null
                    ? property.Name.ToLowerInvariant()
                    : attribute.Key;
            }
        }
    }
}
