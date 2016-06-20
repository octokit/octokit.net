using System;
namespace Octokit
{
	public enum VerificationReason
	{
		ExpiredKey,
		NotSigningKey,
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

