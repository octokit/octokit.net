namespace Octokit
{
    /// <summary>
    /// Represents an oauth application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// <see cref="Application"/> Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url of this <see cref="Application"/>.
        /// </summary>
        public string Url { get; set; }
    }
}