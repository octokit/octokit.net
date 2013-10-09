namespace Octokit
{
    public static class CredentialsExtensions
    {
        public static string GetToken(this Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            return credentials.Password;
        }
    }
}