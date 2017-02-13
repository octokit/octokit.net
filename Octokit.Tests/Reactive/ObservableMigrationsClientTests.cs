﻿using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableMigrationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new EventsClient(null));
            }
        }

        public class TheStartMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);
                var migrationRequest = new StartMigrationRequest(
                    new List<string> { "fake/repo" },
                    true,
                    false);

                client.Start("fake", migrationRequest);
                github.Migration.Migrations.Received(1).Start(
                    "fake",
                    Arg.Is<StartMigrationRequest>(m => m.Equals(migrationRequest)));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);

                client.GetAll("fake");
                github.Migration.Migrations.Received(1).GetAll("fake");
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);

                client.Get("fake", 69);
                github.Migration.Migrations.Received(1).Get("fake", 69);
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);

                client.GetArchive("fake", 69);
                github.Migration.Migrations.Received(1).GetArchive("fake", 69);
            }
        }

        public class TheDeleteArchiveMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);

                client.DeleteArchive("fake", 69);
                github.Migration.Migrations.Received(1).DeleteArchive("fake", 69);
            }
        }

        public class TheUnlockRepositoryMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableMigrationsClient(github);

                client.UnlockRepository("fake", 69, "repo");
                github.Migration.Migrations.Received(1).UnlockRepository("fake", 69, "repo");
            }
        }
    }
}