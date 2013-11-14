using System;
using NSubstitute;
using Octokit;
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

            client.Get(1);

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
    }
}