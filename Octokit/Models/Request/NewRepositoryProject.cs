using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryProject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewRepositoryProject"/> class.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        public NewRepositoryProject(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Required. Gets or sets the name of the project
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional. Gets or sets the body of the project
        /// </summary>
        public string Body { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Body: {1}", Name, Body);
            }
        }
    }
}
