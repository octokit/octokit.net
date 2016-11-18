namespace Octokit
{
    /// <summary>
    /// Used to qualify a search term.
    /// </summary>
    public enum SearchQualifierOperator
    {
        /// <summary>
        /// Greater than "&gt;"
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Less than "&lt;"
        /// </summary>
        LessThan,

        /// <summary>
        /// Less than or equal to. "&lt;="
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// Greater than or equal to. "&gt;="
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        UnknownType
    }
}
