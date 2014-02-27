using System.Linq;

namespace Octokit
{
    internal static class ApiErrorExtensions
    {
        public static string FirstErrorMessageSafe(this ApiError apiError)
        {
            if (apiError == null) return null;
            if (apiError.Errors == null) return null;
            var firstError = apiError.Errors.FirstOrDefault();
            if (firstError == null) return null;
            return firstError.Message;
        }
    }
}
