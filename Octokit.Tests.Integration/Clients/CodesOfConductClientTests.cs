using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class CodesOfConductClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task ReturnsAll()
        {
            var github = Helper.GetAuthenticatedClient();
            var codesOfConduct = await github.CodesOfConduct.GetAll();

            Assert.NotNull(codesOfConduct);
            Assert.Equal(CodeOfConductType.CitizenCodeOfConduct, codesOfConduct[0].Key);
            Assert.Equal(CodeOfConductType.ContributorCovenant, codesOfConduct[1].Key);
        }
    }

    public class TheGetByKeyMethod
    {
        readonly ICodesOfConductClient _codesOfConductClient;
        public TheGetByKeyMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _codesOfConductClient = github.CodesOfConduct;
        }

        [IntegrationTest]
        public async Task GetCitizenConduct()
        {
            var codesOfConduct = await _codesOfConductClient.Get(CodeOfConductType.CitizenCodeOfConduct);

            Assert.Equal("Citizen Code of Conduct", codesOfConduct.Name);
            Assert.Equal("https://api.github.com/codes_of_conduct/citizen_code_of_conduct", codesOfConduct.Url);
            Assert.Null(codesOfConduct.Body);
        }

        [IntegrationTest]
        public async Task GetContributorCovenant()
        {
            var codesOfConduct = await _codesOfConductClient.Get(CodeOfConductType.ContributorCovenant);

            Assert.Equal("Contributor Covenant", codesOfConduct.Name);
            Assert.Equal("https://api.github.com/codes_of_conduct/contributor_covenant", codesOfConduct.Url);
            Assert.Null(codesOfConduct.Body);
        }
    }

    public class TheGetByRepositoryMethod
    {
        [IntegrationTest]
        public async Task GetByRepository()
        {
            var github = Helper.GetAuthenticatedClient();
            var codesOfConduct = await github.CodesOfConduct.Get("octokit", "octokit.net");

            Assert.Equal(CodeOfConductType.ContributorCovenant, codesOfConduct.Key);
        }
    }
}
