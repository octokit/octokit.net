using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    public class GpgKeyContext : IDisposable
    {
        internal GpgKeyContext(IConnection connection, GpgKey key)
        {
            _connection = connection;
            Key = key;
            GpgKeyId = key.Id;
            KeyId = key.KeyId;
            PublicKeyData = key.PublicKey;
        }

        private readonly IConnection _connection;
        internal long GpgKeyId { get; set; }
        internal string KeyId { get; set; }
        internal string PublicKeyData { get; set; }

        internal GpgKey Key { get; set; }

        public void Dispose()
        {
            if (Key != null)
            {
                Helper.DeleteGpgKey(_connection, Key);
            }
        }
    }
}
