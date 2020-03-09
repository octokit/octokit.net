using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    // TODO: how do we represent parameters that are required rather than optional?
    // TODO: what shall we do about calls where pagination is available?

    public class AddResponseModelsTests
    {

        [Fact]
        public async Task PathReturningObjectResponse_PopulatesRequiredModels()
        {
            var stream = TestFixtureLoader.LoadPathWithGet();

            var paths = await PathProcessor.Process(stream);
            var path = paths[0];

            var data = new ApiClientFileMetadata();

            var result = Builders.AddResponseModels(path, data);

            Assert.Equal(5, result.Models.Count);

            var account = Assert.Single(result.Models.Where(m => m.Name == "MarketplaceListingAccount"));
            Assert.Single(account.Properties.Where(p => p.Name == "MarketplacePendingChange" && p.Type == "MarketplacePendingChange"));
            Assert.Single(account.Properties.Where(p => p.Name == "MarketplacePurchase" && p.Type == "MarketplacePurchase"));

            var pendingChange = Assert.Single(result.Models.Where(m => m.Name == "MarketplacePendingChange"));
            Assert.Single(pendingChange.Properties.Where(p => p.Name == "Plan" && p.Type == "MarketplacePendingChangePlan"));

            var purchase = Assert.Single(result.Models.Where(m => m.Name == "MarketplacePurchase"));
            Assert.Single(purchase.Properties.Where(p => p.Name == "Plan" && p.Type == "MarketplacePurchasePlan"));

            // TODO: as these are structurally the same and scoped to the same client,
            //       can we merge them into a single `Plan` type?

            var purchasePlan = Assert.Single(result.Models.Where(m => m.Name == "MarketplacePurchasePlan"));
            Assert.Single(purchasePlan.Properties.Where(p => p.Name == "Bullets" && p.Type == "IReadOnlyList<string>"));

            var plan = Assert.Single(result.Models.Where(m => m.Name == "MarketplacePendingChangePlan"));
            Assert.Single(purchasePlan.Properties.Where(p => p.Name == "Bullets" && p.Type == "IReadOnlyList<string>"));
        }
    }
}
