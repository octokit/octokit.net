using System;
using System.Diagnostics;
using System.Linq;
using Octokit.Tests.Helpers;
using Xunit;
using System.Collections.Generic;

namespace Octokit.Tests.Conventions
{
    public class DebuggerDisplayOnModels
    {
        [Theory]
        [MemberData("ModelTypes")]
        public void CheckModelsForDebuggerDisplayAttribute(Type modelType)
        {
            AssertEx.HasAttribute<DebuggerDisplayAttribute>(modelType);
        }

        public static IEnumerable<object[]> ModelTypes
        {
            get
            {
                foreach (var exportedType in typeof(IEventsClient).Assembly.ExportedTypes)
                {
                    if (!exportedType.IsClientInterface())
                    {
                        continue;
                    }

                    var methods = exportedType.GetMethods();

                    var parameterTypes = methods.SelectMany(method => method.GetParameters(), (method, parameter) => parameter.ParameterType);

                    var returnTypes = methods.Select(method => method.ReturnType);

                    var modelTypes = parameterTypes.Union(returnTypes).Where(type => type.IsModel());

                    foreach (var modelType in modelTypes.Distinct())
                    {
                        yield return new object[] { modelType };
                    }
                }
            }
        }
    }
}