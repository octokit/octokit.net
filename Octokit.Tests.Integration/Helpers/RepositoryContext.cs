using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class RepositoryContext : IDisposable
    {
        internal RepositoryContext(IConnection connection, Repository repo)
        {
            _connection = connection;
            Repository = repo;
            RepositoryId = repo.Id;
            RepositoryOwner = repo.Owner.Login;
            RepositoryName = repo.Name;
            RepositoryDefaultBranch = repo.DefaultBranch;
        }

        private IConnection _connection;
        internal long RepositoryId { get; private set; }
        internal string RepositoryOwner { get; private set; }
        internal string RepositoryName { get; private set; }
        internal string RepositoryDefaultBranch { get; private set; }

        internal Repository Repository { get; private set; }

        public void Dispose()
        {
            Helper.DeleteRepo(_connection, Repository);
        }
    }
}
