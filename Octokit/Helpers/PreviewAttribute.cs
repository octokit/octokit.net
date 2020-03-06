using System;

namespace Octokit
{
    /// <summary>
    /// Metadata for tracking which endpoints rely on preview API behaviour
    /// </summary>
    /// <remarks>https://developer.github.com/v3/previews/</remarks>

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PreviewAttribute : Attribute
    {
        public string Name { get; private set; }

        public PreviewAttribute(string name)
        {
            this.Name = name;
        }
    }
}
