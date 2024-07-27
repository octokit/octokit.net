using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class PublicKeyContext : IDisposable
    {
        internal PublicKeyContext(IConnection connection, PublicKey key)
        {
            _connection = connection;
            Key = key;
            KeyId = key.Id;
            KeyTitle = key.Title;
            KeyData = key.Key;
        }

        private readonly IConnection _connection;
        internal long KeyId { get; private set; }
        internal string KeyTitle { get; private set; }
        internal string KeyData { get; private set; }

        internal PublicKey Key { get; private set; }

        public void Dispose()
        {
            Helper.DeleteKey(_connection, Key);
        }
    }
}
