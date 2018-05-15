using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunImage
    {
        public CheckRunImage()
        {
        }

        public CheckRunImage(string alt, string imageUrl, string caption)
        {
            Alt = alt;
            ImageUrl = imageUrl;
            Caption = caption;
        }

        public string Alt { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Caption { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "ImageUrl: {0}", ImageUrl);
    }
}