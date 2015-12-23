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

        // naming things is hard, so here's a quick set of rules
        // we're using to map the route values to our conventions
        //
        // most of this is around de-pluralization to ensure we're
        // naming things consistently across the codebase
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

        // these routes are handled by specific clients
        // which you won't find by traversing from the root
        // i've called out the specific clients here - and why
        // they're located in a different spot, so we can
        // parse them properly in the future
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

        // these routes either don't exist or are incorrectly named
        // so I've opened issues to capture these distinctions 
        // and ignore them from subsequent tests
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
            if (reportedIssues.ContainsKey(route))
            {
                // don't validate routes with known issues
                return Enumerable.Empty<string>();
            }

            if (Endpoints.All(a => a.IsDeprecated))
            {
                // we assume all endpoints of a section
                // are obsolete, so this is kind of a dumb check
                return Enumerable.Empty<string>();
            }

            Type clientType;

            if (manualMappings.ContainsKey(route))
            {
                clientType = manualMappings[route];
            }
            else
            {
                var tuple = ResolveFromRoot(route);

                if (tuple.Item1.Any())
                {
                    return tuple.Item1;
                }

                clientType = tuple.Item2;
            }

            // TODO: validate methods implement each
            // of the documented endpoints

            return Enumerable.Empty<string>();
        }

        static Tuple<List<string>, Type> ResolveFromRoot(string route)
        {

            var properties = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> errors = new List<string>();

            // starting at the `IGitHubClient` interface
            // we'll look for properties which match with
            // the route for this page
            //
            // for example:
            //
            // /activity/events/
            //
            // 1. find a property named Activity on IGitHubClient
            // 2. find a property named Events on IActivitiesClient
            //
            // this code will report back as soon as it isn't able
            // to resolve a property, and it will use the original
            // route value, not a translated value
            //

            var currentType = typeof(IGitHubClient);

            foreach (var property in properties)
            {
                string lookup = pluralToSingleMap.ContainsKey(property)
                    ? pluralToSingleMap[property]
                    : property;

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

            return Tuple.Create(errors, currentType);
        }
    }
}
