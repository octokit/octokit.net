using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Octokit
{
    /// <summary>
    /// Used to update an existing label.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LabelUpdate
    {
        private string _color;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelUpdate"/> class.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <param name="color">The color of the label.</param>
        public LabelUpdate(string name, string color)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(color, "color");

            Name = name;
            Color = color;
        }

        /// <summary>
        /// Name of the label (required).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the label (required).
        /// </summary>
        public string Color
        {
            get { return _color; }
            set
            {
                if (!Regex.IsMatch(value, @"\A\b[0-9a-fA-F]{6}\b\Z"))
                {
                    throw new ArgumentOutOfRangeException("value", "Color should be an hexadecimal string of length 6");
                }

                _color = value;
            }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}
