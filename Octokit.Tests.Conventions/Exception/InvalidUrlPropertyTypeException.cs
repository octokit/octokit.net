using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class InvalidUrlPropertyTypeException : Exception
    {
        public InvalidUrlPropertyTypeException(Type modelType, Type expectedUrlPropertyType, IEnumerable<PropertyInfo> propertiesWithInvalidType)
            : base(CreateMessage(modelType, expectedUrlPropertyType, propertiesWithInvalidType))
        { }

        static string CreateMessage(Type modelType, Type expectedUrlPropertyType, IEnumerable<PropertyInfo> propertiesWithInvalidType)
        {
            return string.Format("Model type '{0}' contains the following properties that are named or suffixed with 'Url' but are not of type {1}: {2}{3}",
                modelType.FullName,
                expectedUrlPropertyType.Name,
                Environment.NewLine,
                string.Join(Environment.NewLine, propertiesWithInvalidType.Select(x => x.Name)));
        }
    }
}
