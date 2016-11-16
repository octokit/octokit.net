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
        internal static string ToParameter(this Enum prop)
        {
            if (prop == null) return null;

            var propString = prop.ToString();
#if HAS_TYPEINFO
            var member = prop.GetType().GetTypeInfo().DeclaredMembers.FirstOrDefault(x => x.Name == propString);
#else
            var member = prop.GetType().GetMember(propString).FirstOrDefault();
#endif
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(typeof(ParameterAttribute), false)
                .Cast<ParameterAttribute>()
                .FirstOrDefault();

            return attribute != null ? attribute.Value : propString.ToLowerInvariant();
        }
    }
}
