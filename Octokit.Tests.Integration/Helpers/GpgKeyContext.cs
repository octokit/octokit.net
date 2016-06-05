using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    public class GpgKeyContext : IDisposable
    {
        internal GpgKeyContext(GpgKey key)
        {
            Key = key;
            GpgKeyId = key.Id;
            KeyId = key.KeyId;
            PublicKeyData = key.PublicKey;
        }


        internal int GpgKeyId { get; set; }
        internal string KeyId { get; set; }
        internal string PublicKeyData { get; set; }

        internal GpgKey Key { get; set; }

        public void Dispose()
        {
            if (Key != null)
            {
                var api = Helper.GetBasicAuthClient();
                try
                {
                    api.User.GpgKeys.Delete(Key.Id).Wait(TimeSpan.FromSeconds(15));
                }
                catch { }
            }
        }
    }
}
