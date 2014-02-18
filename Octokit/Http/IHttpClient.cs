using System.Threading;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    /// <summary>
    /// Generic Http client. Useful for those who want to swap out System.Net.HttpClient with something else.
    /// </summary>
    /// <remarks>
    /// Most folks won't ever need to swap this out. But if you're trying to run this on Windows Phone, you might.
    /// </remarks>
    public interface IHttpClient
    {
        /// <summary>
        /// Sends the specified request and returns a response.
        /// </summary>
        /// <typeparam name="T">The type of data to send</typeparam>
        /// <param name="request">A <see cref="IRequest"/> that represents the HTTP request</param>
        /// <param name="cancellationToken">Used to cancel the request</param>
        /// <returns>A <see cref="Task{T}" /> of <see cref="IResponse{T}"/></returns>
        Task<IResponse<T>> Send<T>(IRequest request, CancellationToken cancellationToken);
    }
}
