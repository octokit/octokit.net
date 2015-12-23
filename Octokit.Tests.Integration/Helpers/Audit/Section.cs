using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Integration
{
    public class Section
    {
        public string route;
        public string url;

        readonly IEnumerable<Endpoint> endpoints;

        public IEnumerable<Endpoint> Endpoints { get { return endpoints; } }

        public Section(string route, string url)
        {
            this.route = route;
            this.url = url;

            endpoints = WebsiteScraper.FindEndpointsAtUrl(url);
        }

        static readonly Dictionary<string, string> pluralToSingleMap = new Dictionary<string, string>() {
                // dropping the pluralization
                { "gists", "gist" },
                { "comments", "comment" },
                { "issues", "issue" },
                { "assignees", "assignee" },
                { "milestones", "milestone" },
                { "users", "user" },
                { "commits", "commit" },
                { "blobs", "blob" },
                { "trees", "tree" },
                { "tags", "tag" },
                { "teams", "team" },
                { "members", "member" },
                { "deployments", "deployment" },
                { "emails", "email" },
                { "contents", "content" },
                // replace abbreviation with something more user-friendly
                { "pulls", "pullrequest" },
                { "repos", "repository" },
                { "orgs", "organization" },
                { "refs", "reference" },
                { "oauth_authorizations", "authorization" }
            };

        static readonly Dictionary<string, Type> manualMappings = new Dictionary<string, Type>() {
                // these should be implemented on the same client
                // unless there's some significant granularity
                { "/rate_limit/", typeof(IMiscellaneousClient) },
                { "/emojis/", typeof(IMiscellaneousClient) },
                { "/gitignore/", typeof(IMiscellaneousClient) },
                { "/meta/", typeof(IMiscellaneousClient) },
                { "/markdown/", typeof(IMiscellaneousClient) },
                { "/licenses/", typeof(IMiscellaneousClient) },
                // due to the cascading rename, these are currently broken
                { "/git/blobs/", typeof(IBlobsClient) },
                { "/git/commits/", typeof(ICommitsClient) },
                { "/git/refs/", typeof(IReferencesClient) },
                { "/git/tags/", typeof(ITagsClient) },
                { "/git/trees/", typeof(ITreesClient) },
                // this property is called DeployKeys - i kinda like that name
                { "/repos/keys/", typeof(IRepositoryDeployKeysClient) }
            };

        static readonly Dictionary<string, string> reportedIssues = new Dictionary<string, string>() {
                { "/orgs/migrations/", "#1029" },
                { "/orgs/hooks/", "#1028" },
                { "/enterprise/search_indexing/", "#1026" },
                { "/enterprise/orgs/", "#1025" },
                { "/enterprise/management_console/", "#1024" },
                { "/enterprise/license/", "#1023" },
                { "/enterprise/ldap/", "#1022" },
                { "/enterprise/admin_stats/", "#1021" },
                { "/users/administration/", "#1030" },
                { "/repos/comments/", "#1031" },
                { "/repos/releases/", "#1032" },
                { "/repos/pages/", "#1033" },
                { "/repos/downloads/", "OBSOLETE" },
                { "/repos/statuses/", "#1034" },
                { "/repos/commits/", "#1035" },
                { "/repos/collaborators/", "#1036" },
                { "/git/", "#1037" }
            };

        public IEnumerable<string> Validate()
        {
            var currentType = typeof(IGitHubClient);

            if (reportedIssues.ContainsKey(route))
            {
                return Enumerable.Empty<string>();
            }

            if (manualMappings.ContainsKey(route))
            {
                // TODO: need to verify this type
                // implements all the necessary rules
                return Enumerable.Empty<string>();
            }

            var properties = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> errors = new List<string>();

            foreach (var property in properties)
            {
                string lookup;

                if (pluralToSingleMap.ContainsKey(property))
                {
                    lookup = pluralToSingleMap[property];
                }
                else
                {
                    lookup = property;
                }

                var found = currentType.GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(lookup, StringComparison.OrdinalIgnoreCase));

                if (found == null)
                {
                    errors.Add(string.Format("For route '{0}' a property named '{1}' is expected on type '{2}' but it wasn't found",
                        route,
                        property,
                        currentType.Name));
                    break;
                }

                currentType = found.PropertyType;
            }

            // TODO: validate methods are documented

            return errors;
        }
    }
}
