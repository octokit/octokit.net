using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistFile
    {
        public GistFile() { }

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public GistFile(int size, string filename, string type, string language, string content, string rawUrl)
        {
            Size = size;
            Filename = filename;
            Type = type;
            Language = language;
            Content = content;
            RawUrl = rawUrl;
        }

        /// <summary>
        /// The size in bytes of the file.
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; protected set; }

        /// <summary>
        /// The mime type of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; protected set; }

        /// <summary>
        /// The programming language of the file, if any.
        /// </summary>
        public string Language { get; protected set; }

        /// <summary>
        /// The text content of the file.
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// The url to download the file.
        /// </summary>
        public string RawUrl { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Filename: {0}, Size: {1}, Type: {2}, Language: {3}", Filename, Size, Type, Language); }
        }
    }
}