using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class EnumMissingParameterAttributeException : Exception
    {
        public EnumMissingParameterAttributeException(Type enumType, IEnumerable<FieldInfo> enumMembers)
            : base(CreateMessage(enumType, enumMembers))
        { }

        static string CreateMessage(Type enumType, IEnumerable<FieldInfo> enumMembers)
        {
            return string.Format("Enum type '{0}' contains the following members that are missing the Parameter attribute: {1}{2}",
                enumType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, enumMembers.Select(x => x.Name)));
        }
    }
}