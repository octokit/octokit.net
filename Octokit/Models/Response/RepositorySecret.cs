using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit
{
    /// <summary>
    /// Represents a repository secret.
    /// Does not contain the secret value
    /// </summary>
    public class RepositorySecret
    {
        [Parameter(Key = "name")]
        public string Name { get; set; }
        [Parameter(Key = "created_at")]
        public DateTime CreatedAt { get; set; }
        [Parameter(Key = "updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
