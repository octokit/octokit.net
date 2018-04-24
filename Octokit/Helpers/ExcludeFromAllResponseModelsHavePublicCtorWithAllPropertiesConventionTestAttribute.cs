using System;

namespace Octokit
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeFromAllResponseModelsHavePublicCtorWithAllPropertiesConventionTestAttribute : Attribute
    {
        public ExcludeFromAllResponseModelsHavePublicCtorWithAllPropertiesConventionTestAttribute(params string[] properties)
        {
            Properties = properties;
        }

        public string[] Properties { get; private set; }
    }
}
