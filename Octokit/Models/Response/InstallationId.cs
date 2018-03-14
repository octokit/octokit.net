namespace Octokit
{
    public class InstallationId
    {
        public InstallationId() { }

        public InstallationId(long id)
        {
            Id = id;
        }

        /// <summary>
        /// The Installation Id.
        /// </summary>
        public long Id { get; private set; }
    }
}