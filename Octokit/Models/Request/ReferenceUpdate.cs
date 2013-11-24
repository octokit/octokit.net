namespace Octokit
{
    public class ReferenceUpdate
    {
        public ReferenceUpdate(string sha) : this(sha, false)
        {
        }

        public ReferenceUpdate(string sha, bool force)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Sha = sha;
            Force = force;
        }

        public string Sha { get; private set; }
        public bool Force { get; private set; }
    }
}