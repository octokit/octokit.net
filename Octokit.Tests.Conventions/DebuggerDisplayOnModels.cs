using System;
using System.Diagnostics;
using System.Linq;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Conventions
{
    public class DebuggerDisplayOnModels
    {
        [Fact]
        public void CheckModelsForDebuggerDisplayAttributeExample()
        {
            CheckModelsForDebuggerDisplayAttribute(typeof(IAuthorizationsClient));
        }

        [Theory]
        [ClassData(typeof(ClientInterfaces))]
        public void CheckModelsForDebuggerDisplayAttribute(Type clientInterface)
        {
            var methods = clientInterface.GetMethods();
            var modelTypes =
                from modelType in
                    (from type in (
                        from method in methods from parameter in method.GetParameters() select parameter.ParameterType
                        ).Union(
                        from method in methods select method.ReturnType)
                    select type.GetTypeInfo().Type)
                where TypeExtensions.IsModel(modelType)
                select modelType;
            foreach(var modelType in modelTypes.Distinct())
            {
                AssertEx.HasAttribute<DebuggerDisplayAttribute>(modelType);
            }
        }
    }
}