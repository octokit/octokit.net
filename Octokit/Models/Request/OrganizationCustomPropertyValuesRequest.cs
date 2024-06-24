using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationCustomPropertyValuesRequest : SearchRepositoriesRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCustomPropertyValuesRequest"/> class.
        /// </summary>
        public OrganizationCustomPropertyValuesRequest() : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCustomPropertyValuesRequest"/> class.
        /// </summary>
        public OrganizationCustomPropertyValuesRequest(string term)
            : base(term)
        { }

        public override string Sort => null;

        /// <summary>
        /// Get the query parameters that will be appending onto the search
        /// </summary>
        public new IDictionary<string, string> Parameters
        {
            get
            {
                var parameters = base.Parameters;

                // Remove the default sort and order parameters as they are not supported by the API
                parameters.Remove("order");
                parameters.Remove("sort");

                // Replace the default query parameter "q" with the custom query parameter
                var query = parameters["q"];

                if (!string.IsNullOrWhiteSpace(query))
                {
                    parameters.Add("repository_query", query);
                }

                parameters.Remove("q");

                return parameters;
            }
        }
    }
}
