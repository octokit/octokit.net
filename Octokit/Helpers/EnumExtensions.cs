using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Octokit.Internal;

namespace Octokit
{
    static class EnumExtensions
    {
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string ToParameter(this Enum prop)
        {
            if (prop == null) return null;

            var propString = prop.ToString();
            var member = prop.GetType().GetMember(propString).FirstOrDefault();
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(typeof(ParameterAttribute), false)
                .Cast<ParameterAttribute>()
                .FirstOrDefault();

            return attribute != null ? attribute.Value : propString.ToLowerInvariant();
        }
    }
}
