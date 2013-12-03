﻿using System;
using NSubstitute;
using Octokit;
using Xunit;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Tests.Helpers;

public class GistsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Get("1");

            connection.Received().Get<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1"), null);
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var newGist = new NewGist();
            newGist.Description = "my new gist";
            newGist.Public = true;

            newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");
            
            client.Create(newGist);

            connection.Received().Post<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), Arg.Any<object>());
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Delete("1");

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "gists/1"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Delete(null));
        }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new GistsClient(null));
        }
    }
}