using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableReactionsClientTests
    {
        public class CommitComments
        {
            public class TheGetMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var githubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableReactionsClient(githubClient);

                    client.CommitComments.GetAll("fake", "repo", 42);
                    githubClient.Received().Reaction.CommitComments.GetAll("fake", "repo", 42);
                }

                [Fact]
                public void EnsuresArgumentsNotNull()
                {
                    var githubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableReactionsClient(githubClient);

                    Assert.Throws<ArgumentNullException>(() => client.CommitComments.CreateReaction(null, "name", 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentException>(() => client.CommitComments.CreateReaction("", "name", 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentNullException>(() => client.CommitComments.CreateReaction("owner", null, 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentException>(() => client.CommitComments.CreateReaction("owner", "", 1, new NewReaction(ReactionType.Heart)));
                }
            }
            
            public class TheCreateMethod
            {
                [Fact]                
                public void RequestsCorrectUrl()
                {
                    var githubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableReactionsClient(githubClient);
                    var newReaction = new NewReaction(ReactionType.Confused);

                    client.CommitComments.CreateReaction("fake", "repo", 1, newReaction);
                    githubClient.Received().Reaction.CommitComments.CreateReaction("fake", "repo", 1, newReaction);
                }                

                [Fact]
                public void EnsuresArgumentsNotNull()
                {
                    var githubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableReactionsClient(githubClient);

                    Assert.Throws<ArgumentNullException>(() => client.CommitComments.CreateReaction(null, "name", 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentException>(() => client.CommitComments.CreateReaction("", "name", 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentNullException>(() => client.CommitComments.CreateReaction("owner", null, 1, new NewReaction(ReactionType.Heart)));
                    Assert.Throws<ArgumentException>(() => client.CommitComments.CreateReaction("owner", "", 1, new NewReaction(ReactionType.Heart)));
                }
            }
        }
    }
}

