using System.Net.Http;

namespace Octokit.Internal
{
    public static class HttpVerb
    {
        static readonly HttpMethod patch = new HttpMethod("PATCH");

        public static HttpMethod Patch
        {
            get { return patch; }
        }
    }
}
