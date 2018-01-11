using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class Credentials
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes"
            , Justification = "Credentials is immutable")]
        public static readonly Credentials Anonymous = new Credentials();

        private Credentials()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public Credentials(string login, string password, AuthenticationType authenticationType = AuthenticationType.Basic)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(password, "password");

            Login = login;
            Password = password;
            AuthenticationType = authenticationType;
        }

        public Credentials(string token, AuthenticationType authenticationType = AuthenticationType.Oauth)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, "token");

            Login = null;
            Password = token;
            AuthenticationType = authenticationType;
        }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public AuthenticationType AuthenticationType
        {
            get;
            private set;
        }
    }
}