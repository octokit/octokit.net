using System;

namespace Nocto.Helpers
{
    /// <summary>
    ///   Ensure input parameters
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name = "value">The argument value to check.</param>
        /// <param name = "name">The name of the argument.</param>
        public static void ArgumentNotNull(object value, string name)
        {
            if (value != null) return;

            throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks a string argument to ensure it isn't null or empty.
        /// </summary>
        /// <param name = "value">The argument value to check.</param>
        /// <param name = "name">The name of the argument.</param>
        public static void ArgumentNotNullOrEmptyString(string value, string name)
        {
            ArgumentNotNull(value, name);
            if (!string.IsNullOrWhiteSpace(value)) return;

            throw new ArgumentException("String cannot be empty", name);
        }

        /// <summary>
        /// Checks that basic auth is being used as the AuthenticationType
        /// </summary>
        /// <param name="authenticationType"></param>
        public static void IsUsingBasicAuthentication(AuthenticationType authenticationType)
        {
            if (authenticationType != AuthenticationType.Basic)
            {
                throw new AuthenticationException("You must use basic authentication to call this method. Please " + 
                    "supply a login and password.");
            }
        }
    }
}
