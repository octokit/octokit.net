using System;

namespace Octokit
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExcludeFromCtorWithAllPropertiesConventionTestAttribute : Attribute
    {
        public ExcludeFromCtorWithAllPropertiesConventionTestAttribute(params string[] properties)
        {
            Properties = properties;
        }

        public string[] Properties { get; private set; }
    }
}
