using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCommit
    {
        /// <summary>
        /// Create a new commit which has multiple parents (i.e. a merge commit)
        /// </summary>
        /// <param name="message">The message to associate with the commit</param>
        /// <param name="tree">The tree associated with the commit</param>
        /// <param name="parents">An array of parent commits to associate with the commit</param>
        public NewCommit(string message, string tree, IEnumerable<string> parents)
        {
            Message = message;
            Tree = tree;
            Parents = parents;
        }

        /// <summary>
        /// Create a new commit which does not have any parents
        /// </summary>
        /// <param name="message">The message to associate with the commit</param>
        /// <param name="tree">The tree associated with the commit</param>
        public NewCommit(string message, string tree)
            : this(message,tree, Enumerable.Empty<string>())
        {
        }

        /// <summary>
        /// Create a new commit which has one parent
        /// </summary>
        /// <param name="message">The message to associate with the commit</param>
        /// <param name="tree">The tree associated with the commit</param>
        /// <param name="parent">The commit to use as a parent</param>
        public NewCommit(string message, string tree, string parent)
            : this(message, tree, new [] { parent })
        {
        }

        public string Message { get; set; }
        public string Tree { get; set; }
        public IEnumerable<string> Parents { get; set; }

        public Signature Author { get; set; }
        public Signature Committer { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: {0}", Message);
            }
        }
    }
}