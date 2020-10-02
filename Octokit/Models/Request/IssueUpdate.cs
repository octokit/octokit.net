using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Specifies the values used to update an issue.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueUpdate
    {
        /// <summary>
        /// Title of the issue (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// List of logins for the multiple users that this issue should be assigned to
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the multiple assignees for new issues.  The assignees are silently dropped otherwise.
        /// </remarks>
        public ICollection<string> Assignees { get; private set; }

        /// <summary>
        /// Milestone to associate this issue with.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the milestone for new issues. The milestone is silently dropped
        /// otherwise
        /// </remarks>
        [SerializeNull]
        public int? Milestone { get; set; }

        /// <summary>
        /// Labels to associate with this issue.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set labels for new issues. Labels are silently dropped otherwise.
        /// </remarks>
        public ICollection<string> Labels { get; private set; }

        /// <summary>
        /// Whether the issue is open or closed.
        /// </summary>
        public ItemState? State { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: {0}", Title);
            }
        }

        /// <summary>
        /// Adds the specified assignees to the issue.
        /// </summary>
        /// <param name="name">The login of the assignee.</param>
        public void AddAssignee(string name)
        {
            // lazily create the assignees array
            if (Assignees == null)
            {
                Assignees = new List<string>();
            }

            Assignees.Add(name);
        }

        /// <summary>
        /// Clears all the assignees.
        /// </summary>
        public void ClearAssignees()
        {
            // lazily create the assignees array
            if (Assignees == null)
            {
                Assignees = new List<string>();
            }
            else
            {
                Assignees.Clear();
            }
        }

        /// <summary>
        /// Removes the specified assignee from the issue
        /// </summary>
        /// <param name="name">The login of the assignee to remove</param>
        public void RemoveAssignee(string name)
        {
            // lazily create the assignees array
            if (Assignees == null)
            {
                Assignees = new List<string>();
            }
            else
            {
                Assignees.Remove(name);
            }
        }

        /// <summary>
        /// Adds the specified label to the issue.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        public void AddLabel(string name)
        {
            // lazily create the label array
            if (Labels == null)
            {
                Labels = new List<string>();
            }

            Labels.Add(name);
        }

        /// <summary>
        /// Clears all the labels.
        /// </summary>
        public void ClearLabels()
        {
            // lazily create the label array
            if (Labels == null)
            {
                Labels = new List<string>();
            }
            else
            {
                Labels.Clear();
            }
        }

        /// <summary>
        /// Removes the specified label from the issue
        /// </summary>
        /// <param name="name">The name of the label to remove</param>
        public void RemoveLabel(string name)
        {
            // lazily create the label array
            if (Labels == null)
            {
                Labels = new List<string>();
            }
            else
            {
                Labels.Remove(name);
            }
        }
    }
}