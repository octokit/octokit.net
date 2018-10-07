using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class InvalidUrlPropertyTypeException : Exception
    {
        public InvalidUrlPropertyTypeException(Type modelType, IEnumerable<PropertyInfo> propertiesWithInvalidType)
            : base(CreateMessage(modelType, propertiesWithInvalidType))
        { }

        static string CreateMessage(Type modelType, IEnumerable<PropertyInfo> propertiesWithInvalidType)
        {
            return string.Format("Model type '{0}' contains the following properties that are named or suffixed with 'Url' but are not of type String: {1}{2}",
                modelType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, propertiesWithInvalidType.Select(x => x.Name)));
        }
    }
}
