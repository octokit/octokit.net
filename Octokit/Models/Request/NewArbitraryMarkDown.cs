using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Used to create anarbitrary markdown
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/markdown/#render-an-arbitrary-markdown-document
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewArbitraryMarkdown
    {
        const string _markdown = "markdown";
        const string _gfm = "gfm";

        /// <summary>
        /// Create an arbitrary markdown
        /// </summary>
        /// <param name="text">The Markdown text to render</param>
        /// <param name="mode">The rendering mode. Can be either markdown by default or gfm</param>
        /// <param name="context">
        /// The repository context. Only taken into account when rendering as gfm
        /// </param>
        public NewArbitraryMarkdown(string text, string mode, string context)
        {
            Text = text;
            Mode = GetMode(mode);
            Context = context;
        }

        /// <summary>
        /// Create an arbitrary markdown
        /// </summary>
        /// <param name="text">The Markdown text to render
        /// </param>

        public NewArbitraryMarkdown(string text)
            : this(text, _markdown, null)
        {
        }

        /// <summary>
        /// Create an arbitrary markdown
        /// </summary>
        /// <param name="text">The Markdown text to render</param>
        /// <param name="mode">The rendering mode. Can be either markdown by default or gfm</param>
        public NewArbitraryMarkdown(string text, string mode)
            : this(text, mode, null)
        {
        }

        /// <summary>
        /// Gets the markdown text
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the mode of the text
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public string Mode { get; private set; }

        /// <summary>
        /// Gets the context of the markdown
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public string Context { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "gfm")]
        static string GetMode(string mode)
        {
            if (mode != _markdown && mode != _gfm)
            {
                throw (new FormatException("The mode must be either 'markdown' or 'gfm'"));
            }
            else
                return mode;
        }
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Text: {0}", Text);
            }
        }
    }
}
