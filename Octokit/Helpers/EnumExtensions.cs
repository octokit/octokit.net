using System;
using System.Linq;
using System.Reflection;
using Octokit.Internal;

namespace Octokit
{
    static class EnumExtensions
    {
        public static string ToParameter(this Enum prop)
        {
            if (prop == null) return null;
     
            var member = prop.GetType().GetMember(prop.ToString()).FirstOrDefault();
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(typeof(ParameterAttribute), false)
                .Cast<ParameterAttribute>()
                .FirstOrDefault();

            return attribute != null ? attribute.Value : null;
        }
    }
}
