﻿using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
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

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectCardsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null).ToTask());
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
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectCardsClient(gitHubClient);
                var newCard = new NewProjectCard("someNote");

                client.Create(1, newCard);

                gitHubClient.Repository.Project.Card.Received().Create(1, newCard);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                var newCard = new NewProjectCard("someNote");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null).ToTask());
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectCardsClient(gitHubClient);
                var updateCard = new ProjectCardUpdate("someNewNote");

                client.Update(1, updateCard);

                gitHubClient.Repository.Project.Card.Received().Update(1, updateCard);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                var updateCard = new ProjectCardUpdate("someNewNote");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null).ToTask());
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
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectCardsClient(gitHubClient);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                client.Move(1, position);

                gitHubClient.Repository.Project.Card.Received().Move(1, position);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableProjectCardsClient(Substitute.For<IGitHubClient>());
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Move(1, null).ToTask());
            }
        }
    }
}
