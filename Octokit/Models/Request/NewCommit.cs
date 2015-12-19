using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Used to create a commit.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/git/commits/#create-a-commit
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCommit
    {
        /// <summary>
        /// Create a new commit which has multiple parents (i.e. a merge commit)
        /// </summary>
        /// <param name="message">The message to associate with the commit</param>
        /// <param name="tree">The tree associated with the commit</param>
        /// <param name="parents">
        /// The SHAs of the commits that were the parents of this commit. If empty, the commit will be written as a
        /// root commit. For a single parent, an array of one SHA should be provided; for a merge commit, an array of
        /// more than one should be provided.
        /// </param>
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
            : this(message, tree, Enumerable.Empty<string>())
        {
        }

        /// <summary>
        /// Create a new commit which has one parent
        /// </summary>
        /// <param name="message">The message to associate with the commit</param>
        /// <param name="tree">The tree associated with the commit</param>
        /// <param name="parent">The commit to use as a parent</param>
        public NewCommit(string message, string tree, string parent)
            : this(message, tree, new[] { parent })
        {
        }

        /// <summary>
        /// Gets the commit message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the tree associated with the commit.
        /// </summary>
        /// <value>
        /// The tree.
        /// </value>
        public string Tree { get; private set; }

        /// <summary>
        /// Gets the SHAs of the commits that were the parents of this commit. If empty, the commit will be written as
        /// a root commit. For a single parent, an array of one SHA should be provided; for a merge commit, an array of
        /// more than one should be provided.
        /// </summary>
        /// <value>
        /// The parents.
        /// </value>
        public IEnumerable<string> Parents { get; private set; }

        /// <summary>
        /// Gets or sets the author of the commit. If omitted, it will be filled in with the authenticated user’s
        /// information and the current date.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public Committer Author { get; set; }

        /// <summary>
        /// Gets or sets the person who applied the commit. If omitted, this will be filled in with the
        /// <see cref="Author"/>.
        /// </summary>
        /// <value>
        /// The committer.
        /// </value>
        public Committer Committer { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0}", Message);
            }
        }
    }
}