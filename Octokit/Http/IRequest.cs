﻿using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Octokit.Internal
{
    public interface IRequest
    {
        object Body { get; set; }
        Dictionary<string, string> Headers { get; }
        HttpMethod Method { get; }
        Dictionary<string, string> Parameters { get; }
        Uri BaseAddress { get; }
        Uri Endpoint { get; }
        string ContentType { get; }
    }
}
