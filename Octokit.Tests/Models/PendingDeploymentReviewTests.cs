using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class PendingDeploymentReviewTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PendingDeploymentReview(null, PendingDeploymentReviewState.Approved, ""));
                Assert.Throws<ArgumentNullException>(() => new PendingDeploymentReview(new[] { 1L }, PendingDeploymentReviewState.Approved, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                Assert.Throws<ArgumentException>(() => new PendingDeploymentReview(new long[0], PendingDeploymentReviewState.Approved, ""));
            }
        }

        [Fact]
        public void CanBeSerialized()
        {
            var item = new PendingDeploymentReview(new[] { 1L }, PendingDeploymentReviewState.Approved, "Ship it!");

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Serialize(item);

            Assert.Equal(@"{""environment_ids"":[1],""state"":""approved"",""comment"":""Ship it!""}", payload);
        }
    }
}
