using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Models
{
    public class RepositoryUpdateTests
    {
        [Fact]
        public void CanSerialize()
        {
            var expected = "{\"name\":\"Hello-World\"," +
                           "\"description\":\"This is your first repository\"," +
                           "\"homepage\":\"https://github.com\"," +
                           "\"private\":true," +
                           "\"has_issues\":true," +
                           "\"has_wiki\":true," +
                           "\"has_downloads\":true}";

            var update = new RepositoryUpdate("Hello-World");
            update.Description = "This is your first repository";
            update.Homepage = "https://github.com";
            update.Private = true;
            update.HasIssues = true;
            update.HasWiki = true;
            update.HasDownloads = true;

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }
    }
}
