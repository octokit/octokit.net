using System;
using System.Collections.Generic;
using System.Threading;
using NSubstitute;
using Octokit.Internal;

namespace Octokit.Tests
{
    internal class Args
    {
        public static AuthorizationUpdate AuthorizationUpdate
        {
            get { return Arg.Any<AuthorizationUpdate>(); }
        }

        public static NewAuthorization NewAuthorization
        {
            get { return Arg.Any<NewAuthorization>(); }
        }

        public static Uri Uri
        {
            get { return Arg.Any<Uri>(); }
        }

        public static UserUpdate UserUpdate
        {
            get { return Arg.Any<UserUpdate>(); }
        }

        public static IRequest Request
        {
            get { return Arg.Any<IRequest>(); }
        }

        public static object Object
        {
            get { return Arg.Any<object>(); }
        }

        public static string String
        {
            get { return Arg.Any<string>(); }
        }

        public static NewRepository NewRepository
        {
            get { return Arg.Any<NewRepository>(); }
        }

        public static Dictionary<string, string> EmptyDictionary
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.Count == 0); }
        }

        public static Dictionary<string, string> DictionaryWithSince
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("since")); }
        }

        public static Dictionary<string, string> DictionaryWithApiOptions
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("per_page") && d.ContainsKey("page")); }
        }

        public static Dictionary<string, string> DictionaryWithApiOptionsAndSince
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("per_page") && d.ContainsKey("page") && d.ContainsKey("since")); }
        }

        public static OrganizationUpdate OrganizationUpdate
        {
            get { return Arg.Any<OrganizationUpdate>(); }
        }

        public static CancellationToken CancellationToken
        {
            get { return Arg.Any<CancellationToken>(); }
        }

        public static NewDeployKey NewDeployKey
        {
            get { return Arg.Any<NewDeployKey>(); }
        }

        public static ApiOptions ApiOptions
        {
            get { return Arg.Any<ApiOptions>(); }
        }
    }
}
