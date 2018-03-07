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

        public Credentials(string login, string password) : this(login, password, AuthenticationType.Basic) { }

        public Credentials(string login, string password, AuthenticationType authenticationType)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNullOrEmptyString(password, nameof(password));

            Login = login;
            Password = password;
            AuthenticationType = authenticationType;
        }

        public Credentials(string token) : this(token, AuthenticationType.Oauth) { }

        public Credentials(string token, AuthenticationType authenticationType)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, nameof(token));

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