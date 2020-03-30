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

        public SearchCommit(string url, string sha, string htmlUrl, string commentsUrl, CommitInfo commitInfo, User author, User committer, IReadOnlyList<Parent> parents, Repository repository)
        {
            Url = url;
            Sha = sha;
            HtmlUrl = htmlUrl;
            CommentsUrl = commentsUrl;
            CommitInfo = commitInfo;
            Author = author;
            Committer = committer;
            Parents = parents;
            Repository = repository;
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

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitInfo
    {
        public CommitInfo() { }

        public CommitInfo(string url, CommitUserInfo author, CommitUserInfo committer, string message, Tree tree, string commentCount)
        {
            Url = url;
            Author = author;
            Committer = committer;
            Message = message;
            Tree = tree;
            CommentCount = commentCount;
        }

        public string Url { get; protected set; }
        public CommitUserInfo Author { get; protected set; }
        public CommitUserInfo Committer { get; protected set; }
        public string Message { get; protected set; }
        public Tree Tree { get; protected set; }
        public string CommentCount { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "CommitInfo: Author: {0}, Committer: {1}, Message: {2]", Author.Name, Committer.Name, Message);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitUserInfo
    {
        public CommitUserInfo() { }

        public CommitUserInfo(DateTime? date, string name, string email)
        {
            Date = date;
            Name = name;
            Email = email;
        }

        public DateTime? Date { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "CommitUserInfo: Date: {0} Name: {1}, Email: {2}", Date, Name, Email);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Tree
    {
        public Tree() { }

        public Tree(string url, string sha)
        {
            Url = url;
            Sha = sha;
        }

        public string Url { get; protected set; }
        public string Sha { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Tree: Sha: {0}, Url: {1]", Sha, Url);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Parent
    {
        public Parent() { }

        public Parent(string url, string htmlUrl, string sha)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            Sha = sha;
        }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string Sha { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Parent: Sha: {0}, Url: {1]", Sha, Url);
            }
        }
    }
}
