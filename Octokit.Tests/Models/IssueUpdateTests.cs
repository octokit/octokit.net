using System.Linq;
using Xunit;

namespace Octokit.Tests.Models
{
    public class IssueUpdateTests
    {
        [Fact]
        public void Can_Initialise_With_Label()
        {
            var issueUpdate = new IssueUpdate
            {
                Labels = { "Foo" }
            };
            
            Assert.Single(issueUpdate.Labels);
            Assert.Equal("Foo", issueUpdate.Labels.First());
        }
    
        [Fact]
        public void Can_Initialise_With_Assignee()
        {
            var issueUpdate = new IssueUpdate
            {
                Assignees = { "Foo" }
            };
            
            Assert.Single(issueUpdate.Assignees);
            Assert.Equal("Foo", issueUpdate.Assignees.First());
        }
    }
}
