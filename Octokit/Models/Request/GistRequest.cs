using System;
using Octokit.Internal;

namespace Octokit
{
    public class GistRequest : RequestParameters
    {
        public GistRequest(DateTimeOffset since)
        {
            Since = since;
        }

        public DateTimeOffset Since { get; set; }
    }
}
