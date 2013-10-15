﻿using System;
using System.Collections.Generic;
#if NET_45
using System.Collections.ObjectModel;
#endif
using System.Linq;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public class MiscellaneousClient : IMiscellaneousClient
    {
        readonly IConnection _connection;

        public MiscellaneousClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            _connection = connection;
        }

        public async Task<IReadOnlyDictionary<string, Uri>> GetEmojis()
        {
            var endpoint = new Uri("/emojis", UriKind.Relative);
            var response = await _connection.GetAsync<Dictionary<string, string>>(endpoint, null, null);
            return new ReadOnlyDictionary<string, Uri>(
                response.BodyAsObject.ToDictionary(kvp => kvp.Key, kvp => new Uri(kvp.Value)));
        }

        public async Task<string> RenderRawMarkdown(string markdown)
        {
            var endpoint = new Uri("/markdown/raw", UriKind.Relative);
            var response = await _connection.PostAsync<string>(endpoint, markdown, "text/html", "text/plain");
            return response.Body;
        }
    }
}
