using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
	public enum VerificationReason
	{
		ExpiredKey,
		NotSigningKey,
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpgverify")]
		GpgverifyUnavailable,
		Unsigned,
		UnknownSignatureType,
		NoUser,
		VerifiedEmail,
		BadEmail,
		UnknownKey,
		MalformedSignature,
		Invalid,
		Valid
	}
}

