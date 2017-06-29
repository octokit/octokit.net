using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ModelNotUsingStringEnumException : Exception
    {
        public ModelNotUsingStringEnumException(Type modelType, IEnumerable<PropertyInfo> enumProperties)
            : base(CreateMessage(modelType, enumProperties))
        { }

        static string CreateMessage(Type modelType, IEnumerable<PropertyInfo> enumProperties)
        {
            return string.Format("Model type '{0}' contains the following Enum properties which should be wrapped in a StringEnum<TEnum> instead: {1}{2}",
                modelType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, enumProperties.Select(x => x.PropertyType.Name + " " + x.Name)));
        }
    }
}