using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to update a repository variable
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateRepositoryVariable
    {
        public UpdateRepositoryVariable() { }

        public UpdateRepositoryVariable(string value)
        {
            Value = value;
        }
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/actions/variable/#create-or-update-a-repository-variable">API documentation</a> for more information on how to encrypt the secret</remarks>
        [Parameter(Value = "value")]
        public string Value { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Value: {0}", Value);
            }
        }
    }
}
