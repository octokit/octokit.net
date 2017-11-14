using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.Models.Response
{
    public class RepositoryTopics
    {
        public RepositoryTopics(IEnumerable<string> names)
        {

            Names = names;
        }

        public IEnumerable<string> Names { get; protected set; }
    }
}
