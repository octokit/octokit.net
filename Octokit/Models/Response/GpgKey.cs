using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GpgKey
    {
        public GpgKey() { }

        public GpgKey(int id, int? primaryKeyId, string keyId, string publicKey, IReadOnlyList<EmailAddress> emails, IReadOnlyList<GpgKey> subkeys, bool canSign, bool canEncryptCommunications, bool canEncryptStorage, bool canCertify, DateTimeOffset createdAt, DateTimeOffset? expiresAt)
        {
            Id = id;
            PrimaryKeyId = primaryKeyId;
            KeyId = keyId;
            PublicKey = publicKey;
            Emails = emails;
            Subkeys = subkeys;
            CanSign = canSign;
            CanEncryptCommunications = canEncryptCommunications;
            CanEncryptStorage = canEncryptStorage;
            CanCertify = canCertify;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public int Id { get; private set; }
        public int? PrimaryKeyId { get; private set; }
        public string KeyId { get; private set; }
        public string PublicKey { get; private set; }
        public IReadOnlyList<EmailAddress> Emails { get; private set; }
        public IReadOnlyList<GpgKey> Subkeys { get; private set; }
        public bool CanSign { get; private set; }
        [Parameter(Key = "can_encrypt_comms")]
        public bool CanEncryptCommunications { get; private set; }
        public bool CanEncryptStorage { get; private set; }
        public bool CanCertify { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? ExpiresAt { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Key: {1}", Id, PublicKey); }
        }
    }
}
