namespace Octokit
{
    /// <summary>
    /// Wraps a string for the body of a request.
    /// </summary>
    public class BodyWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyWrapper"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        public BodyWrapper(string body)
        {
            Body = body;
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; private set; }
    }
}