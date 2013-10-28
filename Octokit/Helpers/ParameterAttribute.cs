using System;

namespace Octokit.Internal
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}