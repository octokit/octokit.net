using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit
{
    public interface IResponse<T> : IResponse
    {
        new T BodyAsObject { get; set; }
    }

    public interface IResponse
    {
        object BodyAsObject { get; set; }
        string Body { get; set; }
        Dictionary<string, string> Headers { get; }
        Uri ResponseUri { get; set; }
        ApiInfo ApiInfo { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string ContentType { get; set; }
    }
}
