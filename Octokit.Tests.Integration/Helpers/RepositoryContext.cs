using System;

namespace Octokit.Tests.Integration.Helpers
{
    public sealed class RepositoryContext : IDisposable
    {
        internal RepositoryContext(IConnection connection, Repository repo)
        {
            _connection = connection;
            Repository = repo;
            RepositoryId = repo.Id;
            RepositoryOwner = repo.Owner.Login;
            RepositoryName = repo.Name;
        }

        private IConnection _connection;
        internal long RepositoryId { get; private set; }
        internal string RepositoryOwner { get; private set; }
        internal string RepositoryName { get; private set; }

        internal Repository Repository { get; private set; }

        public void Dispose()
        {
            Helper.DeleteRepo(_connection, Repository);
        }
    }
}
