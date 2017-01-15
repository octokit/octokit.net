﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new issue to create via the <see cref="IIssuesClient.Create(string,string,NewIssue)" /> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewIssue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewIssue"/> class.
        /// </summary>
        /// <param name="title">The title of the issue.</param>
        public NewIssue(string title)
        {
            Title = title;
            Assignees = new Collection<string>();
            Labels = new Collection<string>();
        }

        /// <summary>
        /// Title of the milestone (required)
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Login for the user that this issue should be assigned to.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the assignee for new issues. The assignee is silently dropped otherwise.
        /// </remarks>
        [Obsolete("Please use Assignees property.  This property will no longer be supported by the GitHub API and will be removed in a future version")]
        public string Assignee { get; set; }

        /// <summary>
        /// List of logins for the multiple users that this issue should be assigned to
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the multiple assignees for new issues.  The assignees are silently dropped otherwise.
        /// </remarks>
        public Collection<string> Assignees { get; private set; }

        /// <summary>
        /// Milestone to associate this issue with.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the milestone for new issues. The milestone is silently dropped
        /// otherwise
        /// </remarks>
        public int? Milestone { get; set; }

        /// <summary>
        /// Labels to associate with this issue.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set labels for new issues. Labels are silently dropped otherwise.
        /// </remarks>
        public Collection<string> Labels { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                var labels = Labels ?? new Collection<string>();
                return string.Format(CultureInfo.InvariantCulture, "Title: {0} Labels: {1}", Title, string.Join(",", labels));
            }
        }
    }
}
