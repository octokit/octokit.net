using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class MutableModelPropertiesException : Exception
    {
        public MutableModelPropertiesException(Type modelType, IEnumerable<PropertyInfo> mutableProperties)
            : base(CreateMessage(modelType, mutableProperties))
        { }

        static string CreateMessage(Type modelType, IEnumerable<PropertyInfo> mutableProperties)
        {
            return string.Format("Model type '{0}' contains the following mutable properties: {1}{2}",
                modelType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, mutableProperties.Select(x => x.Name)));
        }
    }
}