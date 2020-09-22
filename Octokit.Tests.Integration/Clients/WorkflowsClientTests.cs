using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class WorkflowsClientTests
    {
        public class TheGetAllForRepositoryMethod
        {
            readonly RepositoryContext context;
            readonly IWorkflowsClient fixture;

            public TheGetAllForRepositoryMethod()
            {
                var client = Helper.GetAuthenticatedClient();
                fixture = client.Actions.Workflows;
                context = client.CreateRepositoryContext("public-repo").Result;
            }
            
            [IntegrationTest]
            public async Task GetAllForRepository()
            {
                var workflows = await fixture.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);
                Assert.NotNull(workflows);
            }
        }

        public class TheCreateWorkflowDispatchEventMethod
        {
            readonly RepositoryContext context;
            readonly IWorkflowsClient fixture;
            private readonly string workflowId = "0";

            public TheCreateWorkflowDispatchEventMethod()
            {
                var client = Helper.GetAuthenticatedClient();
                fixture = client.Actions.Workflows;
                context = client.CreateRepositoryContext("public-repo").Result;
            }
            
            [IntegrationTest]
            public async Task CreatesWorkflowDispatchEventWithoutParametersForRepository()
            {
                var workflowDispatchEventInput = new WorkflowDispatchEvent {Ref = "refs/heads/master", Inputs = "{\"name\": \"value\"}"};
                await fixture.CreateWorkflowDispatchEvent(context.RepositoryOwner, context.RepositoryName, workflowId, workflowDispatchEventInput);
            }
        }
    }
}
