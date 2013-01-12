using System.Diagnostics.CodeAnalysis;

namespace Nocto.Http
{
    public interface ICredentialStore
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "The credential store migth not be immediate")]
        Credentials GetCredentials();
    }
}
