using System;
using System.Threading.Tasks;

namespace Octokit
{
    public static class AuthorizationExtensions
    {
        public static async Task<Authorization> GetOrCreateApplicationAuthentication(
            this IAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization,
            Func<TwoFactorRequiredException, Task<TwoFactorChallengeResult>> twoFactorChallengeHandler
            )
        {
            TwoFactorRequiredException twoFactorException = null;
            try
            {
                return await authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, authorization);

            }
            catch (TwoFactorRequiredException exception)
            {
                twoFactorException = exception;
            }
            var twoFactorChallengeResult = await twoFactorChallengeHandler(twoFactorException);

            return await (twoFactorChallengeResult.ResendCodeRequested
                ? authorizationsClient.GetOrCreateApplicationAuthentication(
                    clientId,
                    clientSecret,
                    authorization,
                    twoFactorChallengeHandler)
                : authorizationsClient.GetOrCreateApplicationAuthentication(clientId,
                    clientSecret,
                    authorization,
                    twoFactorChallengeResult.AuthenticationCode));

        }
    }
}
