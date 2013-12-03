using System;
using System.Collections.Generic;
using Octokit.Internal;

namespace Octokit
{
    public class GistUpdate
    {
        public GistUpdate()
        {
            Files = new Dictionary<string, GistFileUpdate>();
        }

        public string Description { get; set; }
        public IDictionary<string, GistFileUpdate> Files { get; private set; }
    }

    public class GistFileUpdate
    {
        public string NewFileName { get; set; }
        public string Content { get; set; }
    }
}
