using System;

namespace Octokit
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ExcludeFromTestAttribute : Attribute
    {
    }
}
