using System;

namespace Octokit.Internal
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class SerializeNullAttribute : Attribute
    {
    }
}