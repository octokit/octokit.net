using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class RepositoryContext : IDisposable
    {
        internal RepositoryContext(Repository repo)
        {
            Repository = repo;
            RepositoryOwner = repo.Owner.Login;
            RepositoryName = repo.Name;
        }

        internal string RepositoryOwner { get; private set; }
        internal string RepositoryName { get; private set; }

        internal Repository Repository { get; private set; }

        public void Dispose()
        {
            Helper.DeleteRepo(Repository);
        }
    }
}
