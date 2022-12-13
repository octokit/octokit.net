using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Octokit.Tests.Conventions
{
    public class PreviewsTests
    {
        [Fact]
        public void NoStalePreviews()
        {
            var fields = typeof(AcceptHeaders).GetFields(BindingFlags.Public | BindingFlags.Static);

            var values = fields.Where(f => f.GetCustomAttribute<ObsoleteAttribute>() == null)
                               .ToDictionary(f => f.Name, f => f.GetValue(null).ToString());

            var previewKeys = new string[]
            {
                "wyandotte",
                "ant-man",
                "squirrel-girl",
                "mockingbird",
                "machine-man",
                "inertia",
                "cloak",
                "black-panther",
                "giant-sentry-fist",
                "mercy",
                "scarlet-witch",
                "sailor-v",
                "zzzax",
                "luke-cage",
                "antiope",
                "starfox",
                "fury",
                "flash",
                "surtur",
                "corsair",
                "sombra",
                "shadow-cat",
                "switcheroo",
                "groot",
                "gambit",
                "dorian",
                "lydian",
                "london",
                "baptiste",
                "doctor-strange",
                "nebula",
                // Enterprise-specific previews
                // https://developer.github.com/enterprise/2.20/v3/enterprise-admin/pre_receive_environments/
                "eye-scream",
                // this is a new preview that seems undocumented but get mentioned here
                // https://developer.github.com/changes/2/#--multi-line-comments
                "comfort-fade"
            };

            var previewAcceptHeaders = previewKeys.Select(k => $"application/vnd.github.{k}-preview+json");

            var defaultHeaders = new string[]
            {
                // default content types
                "application/vnd.github.v3",
                "application/vnd.github.v3+json",
                // for places where content or markdown can be rendered
                "application/vnd.github.v3.html",
                "application/vnd.github.v3.raw",
                // https://developer.github.com/v3/repos/commits/#get-a-single-commit
                "application/vnd.github.v3.sha",
                // https://developer.github.com/v3/activity/starring/#alternative-response-with-star-creation-timestamps
                "application/vnd.github.v3.star+json",
                "application/vnd.github.v3.repository+json"
            };

            var validHeaders = defaultHeaders.Concat(previewAcceptHeaders);

            var notAllowedHeaders = values.Values.ToList().Except(validHeaders).OrderBy(k => k);
            if (notAllowedHeaders.Any())
            {
                throw new InvalidAcceptHeadersFound(notAllowedHeaders);
            }
        }
    }

    public class InvalidAcceptHeadersFound : Exception
    {
        public InvalidAcceptHeadersFound(IEnumerable<string> notAllowedHeaders)
            : base(CreateMessage(notAllowedHeaders))
        { }

        static string CreateMessage(IEnumerable<string> notAllowedHeaders)
        {
            return string.Format("Accept headers in use but not allowed: {0}{1}",
               Environment.NewLine,
               string.Join(Environment.NewLine, notAllowedHeaders));
        }
    }
}
