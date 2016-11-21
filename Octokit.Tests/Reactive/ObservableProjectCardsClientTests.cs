using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableProjectCardsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableProjectCardsClient(null));
            }

            public class TheGetAllMethod
            {
                [Fact]
                public void RequestCorrectURL()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);

                    client.GetAll(1);

                    gitHubClient.Received().Repository.Project.Card.GetAll(1);
                }
            }

            public class TheGetMethod
            {
                [Fact]
                public void RequestCorrectURL()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);

                    client.Get(1);

                    gitHubClient.Repository.Project.Card.Received().Get(1);
                }
            }

            public class TheCreateMethod
            {
                [Fact]
                public void PostToCorrectURL()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);
                    var newCard = new NewProjectCard("someNote");

                    client.Create(1, newCard);

                    gitHubClient.Repository.Project.Card.Received().Create(1, newCard);
                }

                [Fact]
                public void EnsureNonNullArguments()
                {
                    var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                    var newCard = new NewProjectCard("someNote");

                    Assert.Throws<ArgumentNullException>(() => client.Create(1, null));
                }
            }

            public class TheUpdateMethod
            {
                [Fact]
                public void PostToCorrectURL()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);
                    var updateCard = new ProjectCardUpdate("someNewNote");

                    client.Update(1, updateCard);

                    gitHubClient.Repository.Project.Card.Received().Update(1, updateCard);
                }

                [Fact]
                public void EnsureNonNullArguments()
                {
                    var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                    var updateCard = new ProjectCardUpdate("someNewNote");

                    Assert.Throws<ArgumentNullException>(() => client.Update(1, null));
                }
            }

            public class TheDeleteMethod
            {
                [Fact]
                public void DeletesCorrectURL()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);

                    client.Delete(1);

                    gitHubClient.Repository.Project.Card.Received().Delete(1);
                }
            }

            public class TheMoveMethod
            {
                [Fact]
                public void PostToCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservableProjectCardsClient(gitHubClient);
                    var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                    client.Move(1, position);

                    gitHubClient.Repository.Project.Card.Received().Move(1, position);
                }

                [Fact]
                public void EnsureNonNullArguments()
                {
                    var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                    var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                    Assert.Throws<ArgumentNullException>(() => client.Move(1, null));
                }
            }
        }
    }
}
