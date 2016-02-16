using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class PublicKeyContext : IDisposable
    {
        internal PublicKeyContext(PublicKey key)
        {
            Key = key;
            KeyId = key.Id;
            KeyTitle = key.Title;
            KeyData = key.Key;
        }

        internal int KeyId { get; private set; }
        internal string KeyTitle { get; private set; }
        internal string KeyData { get; private set; }

        internal PublicKey Key { get; private set; }

        public void Dispose()
        {
            Helper.DeleteKey(Key);
        }
    }
}
