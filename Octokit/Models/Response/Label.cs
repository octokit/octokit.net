using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Label
    {
        public Label() { }

        public Label(Uri url, string name, string color)
        {
            Url = url;
            Name = name;
            Color = color;
        }

        /// <summary>
        /// Url of the label
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// Name of the label
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Color of the label
        /// </summary>
        public string Color { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Color: {1}", Name, Color); }
        }
    }
}
