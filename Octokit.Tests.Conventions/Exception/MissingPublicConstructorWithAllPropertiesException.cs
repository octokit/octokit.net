using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class MissingPublicConstructorWithAllPropertiesException : Exception
    {
        public MissingPublicConstructorWithAllPropertiesException(Type modelType, IEnumerable<PropertyInfo> missingProperties)
            : base(CreateMessage(modelType, missingProperties))
        { }

        private static string CreateMessage(Type modelType, IEnumerable<PropertyInfo> missingProperties)
        {
            return string.Format("Model type '{0}' is missing a constructor with all properties. Closest match is missing the following properties: {1}{2}",
                modelType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, missingProperties.Select(prop => prop.Name)));
        }
    }
}
