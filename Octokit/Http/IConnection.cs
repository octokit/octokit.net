﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IConnection
    {
        Task<IResponse<string>> GetHtml(Uri uri, IDictionary<string, string> parameters);
        Task<IResponse<T>> GetAsync<T>(Uri uri, IDictionary<string, string> parameters, string accepts);
        Task<IResponse<T>> PatchAsync<T>(Uri uri, object body);
        Task<IResponse<T>> PostAsync<T>(Uri uri, object body, string accepts, string contentType);
        Task<IResponse<T>> PutAsync<T>(Uri uri, object body);
        Task<IResponse<T>> PutAsync<T>(Uri uri, object body, string twoFactorAuthenticationCode);

        Task DeleteAsync(Uri uri);

        Uri BaseAddress { get; }

        ICredentialStore CredentialStore { get; }

        Credentials Credentials { get; set; }
    }
}
