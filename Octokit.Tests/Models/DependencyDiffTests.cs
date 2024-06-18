using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit
{
    public class DependencyDiffTests
    {
        [Fact]
        public void CanDeserialize()
        {
            const string json = @"{
                ""change_type"": ""removed"",
                ""manifest"": ""Cargo.lock"",
                ""ecosystem"": ""cargo"",
                ""name"": ""libsqlite3-sys"",
                ""version"": ""0.22.2"",
                ""package_url"": ""pkg:cargo/libsqlite3-sys@0.22.2"",
                ""license"": ""MIT"",
                ""source_repository_url"": ""https://github.com/rusqlite/rusqlite"",
                ""scope"": ""runtime"",
                ""vulnerabilities"": [
                    {
                        ""severity"": ""high"",
                        ""advisory_ghsa_id"": ""GHSA-jw36-hf63-69r9"",
                        ""advisory_summary"": ""`libsqlite3-sys` via C SQLite improperly validates array index"",
                        ""advisory_url"": ""https://github.com/advisories/GHSA-jw36-hf63-69r9""
                    }
                ]
            }";

            var actual = new SimpleJsonSerializer().Deserialize<DependencyDiff>(json);

            Assert.Equal("removed", actual.ChangeType);
            Assert.Equal("Cargo.lock", actual.Manifest);
            Assert.Equal("cargo", actual.Ecosystem);
            Assert.Equal("libsqlite3-sys", actual.Name);
            Assert.Equal("0.22.2", actual.Version);
            Assert.Equal("pkg:cargo/libsqlite3-sys@0.22.2", actual.PackageUrl);
            Assert.Equal("MIT", actual.License);
            Assert.Equal("https://github.com/rusqlite/rusqlite", actual.SourceRepositoryUrl);
            Assert.Equal("runtime", actual.Scope);
            Assert.NotNull(actual.Vulnerabilities);
            Assert.Single(actual.Vulnerabilities);
            var vulnerability = actual.Vulnerabilities.First();
            Assert.Equal("high", vulnerability.Severity);
            Assert.Equal("GHSA-jw36-hf63-69r9", vulnerability.AdvisoryGhsaId);
            Assert.Equal("`libsqlite3-sys` via C SQLite improperly validates array index", vulnerability.AdvisorySummary);
            Assert.Equal("https://github.com/advisories/GHSA-jw36-hf63-69r9", vulnerability.AdvisoryUrl);
        }
    }
}
