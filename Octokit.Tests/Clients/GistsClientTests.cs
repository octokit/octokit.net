using System;
using NSubstitute;
using Octokit;
using Xunit;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new GistsClient(null));
        }

        [Fact]
        public void SetCommentsClient()
        {
            var apiConnection = Substitute.For<IApiConnection>();
            var client = new GistsClient(apiConnection);
            Assert.NotNull(client.Comment);
        }
    }
}
