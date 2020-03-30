using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommit
    {
        public SearchCommit()
        {
        }

        public string Url { get; protected set; }

        public string Sha { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string CommentsUrl { get; protected set; }

        public CommitInfo CommitInfo { get; protected set; }

        public User Author { get; protected set; }
        
        public User Committer { get; protected set; }

        public IReadOnlyList<Parent> Parents { get; protected set; }

        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "SearchCommit: Sha: {0} Author: {1}, Committer: {2}", Sha, Author.Name, Committer.Name);
            }
        }
    }

    public class CommitInfo
    {
        public string Url { get; protected set; }
        public CommitUserInfo Author { get; protected set; }
        public CommitUserInfo Committer { get; protected set; }
        public string Message { get; protected set; }
        public Tree Tree { get; protected set; }
        public string CommentCount { get; protected set; }
    }

    public class CommitUserInfo
    {
        public DateTime? Date { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
    }

    public class Tree
    {
        public string Url { get; protected set; }
        public string Sha { get; protected set; }
    }

    public class Parent
    {
        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string Sha { get; protected set; }
    }
}
