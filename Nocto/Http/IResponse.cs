using System;
using System.Collections.Generic;

namespace Nocto.Http
{
    public interface IResponse<T>
    {
        string Body { get; set; }
        T BodyAsObject { get; set; }
        Dictionary<string, string> Headers { get; }
        Uri ResponseUri { get; set; }
        ApiInfo ApiInfo { get; set; }
    }
}
