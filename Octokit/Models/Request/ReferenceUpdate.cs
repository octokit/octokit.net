namespace Octokit
{
    public class ReferenceUpdate
    {
        public ReferenceUpdate(string sha, bool force = false)
        {
            Sha = sha;
            Force = force;
        }

        public string Sha { get; set; }
        public bool Force { get; set; }
    }
}