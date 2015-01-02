using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit
{
    public interface IApiResponse<out T>
    {
        T Body { get; }

        ApiInfo ApiInfo { get; }

        HttpStatusCode StatusCode { get; }

        IResponse HttpResponse { get; }
    }

    public interface IResponse
    {
        object Body { get; }
        IReadOnlyDictionary<string, string> Headers { get; }
        ApiInfo ApiInfo { get; }
        HttpStatusCode StatusCode { get; }
        string ContentType { get; }
    }
}
