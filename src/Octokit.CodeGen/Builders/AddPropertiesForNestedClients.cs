using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using OneOf;

namespace Octokit.CodeGen
{
    using TypeMergeFunc = System.Func<List<ApiClientFileMetadata>, List<ApiClientFileMetadata>>;

    public partial class Builders
    {
        public static readonly TypeMergeFunc AddPropertiesForNestedClients = (clients) =>
        {
          return clients;
        };
    }
}
