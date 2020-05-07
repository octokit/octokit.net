namespace Octokit
{
    public static class CredentialsExtensions
    {
        public static string GetToken(this Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, nameof(credentials));

            return credentials.Password;
        }
    }
}