using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests
{
	public class VerificationTests
	{
		private static readonly string verificationJson = @"{
      		""verified"": true,
      		""reason"": ""valid"",
      		""signature"": ""-----BEGIN PGP MESSAGE-----\n...\n-----END PGP MESSAGE-----"",
      		""payload"": ""tree 6dcb09b5b57875f334f61aebed695e2e4193db5e\n...""
    	}";
		
		private static readonly Verification verificationObject = new Verification(
			verified: true,
			reason: VerificationReason.Valid,
			signature: "-----BEGIN PGP MESSAGE-----\n...\n-----END PGP MESSAGE-----",
			payload: "tree 6dcb09b5b57875f334f61aebed695e2e4193db5e\n..."
		);

		[Fact]
		public void CanBeDeserialized()
		{
			var serializer = new SimpleJsonSerializer();

			var verification = serializer.Deserialize<Verification>(verificationJson);

			Assert.Equal(verificationObject.Reason, verification.Reason);
			Assert.Equal(verificationObject.Signature, verification.Signature);
			Assert.Equal(verificationObject.Payload, verification.Payload);
		}
	}
}

