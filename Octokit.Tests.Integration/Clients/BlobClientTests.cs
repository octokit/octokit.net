using System;
using System.Net.Http.Headers;
using Octokit;
using Octokit.Tests.Integration;
using System.Threading.Tasks;
using Xunit;
using System.Text;

public class BlobClientTests : IDisposable
{
    readonly IBlobsClient _fixture;
    readonly Repository _repository;
    readonly string _owner;

    public BlobClientTests()
    {
        var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = client.GitDatabase.Blob;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateABlob()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_owner, _repository.Name, blob);

        Assert.False(String.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanGetABlob()
    {
        var newBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_owner, _repository.Name, newBlob);
        var blob = await _fixture.Get(_owner, _repository.Name, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        var contents = Encoding.UTF8.GetString(Convert.FromBase64String(blob.Content));

        Assert.Equal("Hello World!", contents);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
