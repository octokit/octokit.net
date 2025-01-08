using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit;
using Octokit.Tests;
using Xunit;

using static Octokit.Internal.TestSetup;

public class CodespacesClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new CodespacesClient(null));
        }
    }

    public class TheGetAllMethod
    {
        [Fact]
        public void RequestsCorrectGetAllUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);

            client.GetAll();
            connection.Received().Get<CodespacesCollection>(Arg.Is<Uri>(u => u.ToString() == "user/codespaces"));
        }

        [Fact]
        public void RequestsCorrectGetForRepositoryUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            client.GetForRepository("owner", "repo");
            connection.Received().Get<CodespacesCollection>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/codespaces"));
        }

        [Fact]
        public void RequestsCorrectGetUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            client.Get("codespaceName");
            connection.Received().Get<Codespace>(Arg.Is<Uri>(u => u.ToString() == "user/codespaces/codespaceName"));
        }

        [Fact]
        public void RequestsCorrectStartUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            client.Start("codespaceName");
            connection.Received().Post<Codespace>(Arg.Is<Uri>(u => u.ToString() == "user/codespaces/codespaceName/start"));
        }

        [Fact]
        public void RequestsCorrectStopUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            client.Stop("codespaceName");
            connection.Received().Post<Codespace>(Arg.Is<Uri>(u => u.ToString() == "user/codespaces/codespaceName/stop"));
        }

        [Fact]
        public void RequestsCorrectGetAvailableMachinesForRepoUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            client.GetAvailableMachinesForRepo("owner", "repo");
            connection.Received().Get<MachinesCollection>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/codespaces/machines"));
        }

        [Fact]
        public void RequestsCorrectCreateNewCodespaceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CodespacesClient(connection);
            NewCodespace newCodespace = new NewCodespace(new Machine());
            client.Create("owner", "repo", newCodespace);
            connection.Received().Post<Codespace>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/codespaces"), newCodespace);
        }
    }
}
