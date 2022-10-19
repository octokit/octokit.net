using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ResponseModelSetterlessAutoPropertyException : Exception
    {
        public ResponseModelSetterlessAutoPropertyException(Type modelType, IEnumerable<PropertyInfo> setterlessProperties)
            : base(CreateMessage(modelType, setterlessProperties))
        { }

        static string CreateMessage(Type modelType, IEnumerable<PropertyInfo> setterlessProperties)
        {
            return string.Format("Model type '{0}' contains the following setterless properties: {1}{2}",
                modelType.FullName,
                Environment.NewLine,
                string.Join(Environment.NewLine, setterlessProperties.Select(x => x.Name)));
        }
    }
}