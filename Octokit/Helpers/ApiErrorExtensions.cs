using System.Linq;

namespace Octokit
{
    internal static class ApiErrorExtensions
    {
        public static string FirstErrorMessageSafe(this ApiError apiError)
        {
            if (apiError == null) return null;
            if (apiError.Errors == null) return apiError.Message;
            var firstError = apiError.Errors.FirstOrDefault();
            return firstError == null ? null : firstError.Message;
        }
    }
}
