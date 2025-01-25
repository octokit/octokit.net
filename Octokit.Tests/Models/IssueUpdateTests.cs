using Xunit;

namespace Octokit.Tests.Models
{
    public class IssueUpdateTests
    {
        [Fact]
        public void Can_Initialise_With_Label()
        {
            _ = new IssueUpdate
            {
                Labels = { "Foo" }
            };
        }
    
        [Fact]
        public void Can_Initialise_With_Assignee()
        {
            _ = new IssueUpdate
            {
                Assignees = { "Foo" }
            };
        }
    }
}
