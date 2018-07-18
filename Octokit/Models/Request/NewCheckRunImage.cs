using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRunImage
    {
        /// <summary>
        /// Constructs a CheckRunImage request object
        /// </summary>
        /// <param name="alt">Required. The alternative text for the image</param>
        /// <param name="imageUrl">Required. The full URL of the image</param>
        public NewCheckRunImage(string alt, string imageUrl)
        {
            Alt = alt;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Required. The alternative text for the image
        /// </summary>
        public string Alt { get; protected set; }

        /// <summary>
        /// Required. The full URL of the image
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// A short image description
        /// </summary>
        public string Caption { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Alt: {0}, ImageUrl: {1}", Alt, ImageUrl);
    }
}