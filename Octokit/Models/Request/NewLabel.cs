using System;
using System.Text.RegularExpressions;

namespace Octokit
{
    /// <summary>
    /// Describes a new label to create via the <see cref="IIssuesLabelsClient.Create(string,string,NewLabel)"/> method.
    /// </summary>
    public class NewLabel
    {
        private string _color;

        public NewLabel(string name, string color)
        {
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
                if (!string.IsNullOrEmpty(value)
                    && value.Length == 6
                    && Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z"))
                {
                    throw new ArgumentOutOfRangeException("value", "Color should be an hexadecimal string of length 6");
                }

                _color = value;
            }
        }
    }
}
