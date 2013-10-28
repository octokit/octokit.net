using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Base class for classes which represent query string parameters to certain API endpoints.
    /// </summary>
    public abstract class RequestParameters
    {
        static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _propertiesMap =
        new ConcurrentDictionary<Type, List<PropertyInfo>>();

        public virtual IDictionary<string, string> ToParametersDictionary()
        {
            var properties = _propertiesMap.GetOrAdd(GetType(), GetPropertiesForType);

            var dict = (from property in properties
                let value = GetValue(property)
                let key = GetKey(property)
                where value != null
                select new { key, value }).ToDictionary(kvp => kvp.key, kvp => kvp.value);
            return dict;
        }

        static List<PropertyInfo> GetPropertiesForType(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "GitHub API depends on lower case strings")]
        static string GetKey(PropertyInfo property)
        {
            var attribute = property.GetCustomAttributes(typeof(ParameterAttribute), false)
                .Cast<ParameterAttribute>()
                .FirstOrDefault(attr => attr.Key != null);
            
            return attribute == null 
                ? property.Name.ToLowerInvariant()
                : attribute.Key;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "GitHub API depends on lower case strings")]
        string GetValue(PropertyInfo property)
        {
            var value = property.GetValue(this, null);

            if (typeof(IEnumerable<string>).IsAssignableFrom(property.PropertyType))
            {
                var list = (IEnumerable<string>)value;
                return !list.Any() ? null : String.Join(",", list);
            }

            if (property.PropertyType.IsDateTimeOffset() && value != null)
            {
                return ((DateTimeOffset)value).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture);
            }

            if (property.PropertyType.IsEnum && value != null)
            {
                var member = property.PropertyType.GetMember(value.ToString()).FirstOrDefault();
                if (member != null)
                {
                    var attribute = member.GetCustomAttributes(typeof(ParameterAttribute), false)
                        .Cast<ParameterAttribute>()
                        .FirstOrDefault();
                    if (attribute != null)
                    {
                        return attribute.Value;
                    }
                }
            }

            return value != null 
                ? value.ToString().ToLowerInvariant()
                : null;
        }
    }
}
