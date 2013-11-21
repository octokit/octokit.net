namespace Octokit
{
    /// <summary>
    /// User by <see cref="GistHistory"/> to indicate the level of change.
    /// </summary>
    public class GistChangeStatus
    {
        /// <summary>
        /// The number of deletions that occurred as part of this change.
        /// </summary>
        public int Deletions { get; set; }

        /// <summary>
        /// The number of additions that occurred as part of this change.
        /// </summary>
        public int Additions { get; set; }

        /// <summary>
        /// The total number of changes.
        /// </summary>
        public int Total { get; set; }
    }
}