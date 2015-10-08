namespace Octokit
{
    /// <summary>
    /// Provides a property for the Last recorded API infomation
    /// </summary>
    public interface IApiInfoProvider
    {
        /// <summary>
        /// Gets the latest API Info - this will be null if no API calls have been made
        /// </summary>
        /// <returns><seealso cref="ApiInfo"/> representing the information returned as part of an Api call</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        ApiInfo GetLastApiInfo();
    }
}
