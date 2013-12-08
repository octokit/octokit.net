using System;
using System.Text.RegularExpressions;

namespace Octokit
{
    public class LabelUpdate
    {
        private string _color;

        public LabelUpdate(string name, string color)
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
                if (string.IsNullOrEmpty(value)
                    || value.Length != 6
                    || !Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z"))
                {
                    throw new ArgumentOutOfRangeException("value", "Color should be an hexadecimal string of length 6");
                }

                _color = value;
            }
        }
    }
}
