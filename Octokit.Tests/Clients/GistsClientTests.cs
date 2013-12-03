using System;
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

    public class TheGetAllMethods
    {
        [Fact]
        public void RequestsCorrectGetAllUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAll();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), null);
        }

        [Fact]
        public void RequestsCorrectGetAllWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAll(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), 
                Arg.Is<IDictionary<string,string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllPublicUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            
            client.GetAllPublic();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllPublicWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllPublic(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllStarredUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllStarred();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllStarredWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllStarred(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForUser("octokit");

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllForUser("octokit", since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
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

    public class TheStarMethods
    {
        [Fact]
        public void RequestsCorrectStarUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Star("1");

            connection.Received().Get<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"), null);
        }

        [Fact]
        public void RequestsCorrectUnstarUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Unstar("1");

            connection.Received().Get<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"), null);
        }

        [Fact]
        public void RequestsCorrectCheckIfStarredUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.IsStarred("1");

            connection.Received().Get<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"), null);
        }
    }

    public class TheForkMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Fork("1");

            connection.Received().Post<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1/fork"), 
                                             Arg.Is<object>(o => o == null));
        }
    }

    public class TheEditMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var updateGist = new GistUpdate();
            updateGist.Description = "my newly updated gist";
            var gistFileUpdate = new GistFileUpdate 
            { 
                NewFileName = "myNewGistTestFile.cs",
                Content = "new GistsClient(connection).Edit();"
            };

            updateGist.Files.Add("myGistTestFile.cs", gistFileUpdate);

            client.Edit("1", updateGist);

            connection.Received().Post<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1"), Arg.Any<object>());
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