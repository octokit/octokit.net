namespace Octokit
{
    /// <summary>
    /// Container for the static <see cref="Empty"/> method that represents an
    /// intentional empty request body to avoid overloading <code>null</code>.
    /// </summary>
    public static class RequestBody
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static object Empty = new object();
    }
}