using System;

namespace Octokit
{
    public class Label
    {
        /// <summary>
        /// Url of the label
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Name of the label
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the label
        /// </summary>
        public string Color { get; set; }
    }
}