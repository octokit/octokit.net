using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
    public class GpgKey
    {
        public int Id { get; protected set; }
        public int? PrimaryKeyId { get; protected set; }
        public string KeyId { get; protected set; }
        public string PublicKey { get; protected set; }
        public IReadOnlyList<EmailAddress> Emails { get; protected set; }
        public IReadOnlyList<GpgKey> Subkeys { get; protected set; }
        public bool CanSign { get; protected set; }
        [Parameter(Key = "can_encrypt_comms")]
        public bool CanEncryptCommunications { get; protected set; }
        public bool CanEncryptStorage { get; protected set; }
        public bool CanCertify { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? ExpiresAt { get; protected set; }
    }
}
