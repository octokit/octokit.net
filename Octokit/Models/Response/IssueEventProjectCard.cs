using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventProjectCard
    {
        public IssueEventProjectCard() { }

        public IssueEventProjectCard(long id, string url, long projectId, string projectUrl, string columnName, string previousColumnName)
        {
            Id = id;
            Url = url;
            ProjectId = projectId;
            ProjectUrl = projectUrl;
            ColumnName = columnName;
            PreviousColumnName = previousColumnName;
        }

        /// <summary>
        /// The identification number of the project card.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The API URL of the project card, if the card still exists.
        /// Not included for removed_from_project events.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The identification number of the project.
        /// </summary>
        public long ProjectId { get; private set; }

        /// <summary>
        /// The API URL of the project.
        /// </summary>
        public string ProjectUrl { get; private set; }

        /// <summary>
        /// The name of the column that the card is listed in.
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// The name of the column that the card was listed in prior to column_name
        /// Only returned for moved_columns_in_project events.
        /// </summary>
        public string PreviousColumnName { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}", Id); }
        }
    }
}
