using System;

namespace Octokit.Internal
{
    /// <summary>
    /// Indicate to the serializer that this property or field
    /// should be included, even when currently set to `null`
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class SerializeNullAttribute : Attribute
    {
    }
}