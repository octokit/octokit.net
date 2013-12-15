using System;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

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

    public class TheGetAllForUserMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gists = new GistsClient(Substitute.For<IApiConnection>());

            AssertEx.Throws<ArgumentNullException>(async () => await gists.GetAllForUser(null));
        }

        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            const string dummyuser = "dummyUser";

            client.GetAllForUser(dummyuser);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == string.Format("users/{0}/gists", dummyuser)));
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForCurrent();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"));
        }
    }

    public class TheGetStarredForCurrentMethod
    {
        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetStarredForCurrent();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"));
        }
    }

    public class TheGetPublicMethod
    {
        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetPublic();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"));
        }
    }

    public class TheStarMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gists = new GistsClient(Substitute.For<IApiConnection>());

            AssertEx.Throws<ArgumentNullException>(async () => await gists.Star(null));
        }

        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            const string gistId = "123456";

            client.Star(gistId);

            connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == string.Format("gists/{0}/star", gistId)));
        }
    }

    public class TheUnStarMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gists = new GistsClient(Substitute.For<IApiConnection>());

            AssertEx.Throws<ArgumentNullException>(async () => await gists.Unstar(null));
        }

        [Fact]
        public void RequestCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            const string gistId = "123456";

            client.Unstar(gistId);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == string.Format("gists/{0}/star", gistId)));
        }
    }

}