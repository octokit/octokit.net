namespace Octokit
{
    public class NewReference
    {
        public NewReference(string @ref, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(@ref, "ref");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Ref = @ref;
            Sha = sha;
        }

        public string Ref { get; private set; }
        public string Sha { get; private set; }
    }
}