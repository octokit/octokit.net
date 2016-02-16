using Octokit.Helpers;
using Octokit.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Octokit.Tests
{
    public class SimpleJsonSerializerTests
    {
        public class TheSerializeMethod
        {
            [Fact]
            public void UsesRubyCasing()
            {
                var item = new Sample { Id = 42, FirstName = "Phil", IsSomething = true, Private = true };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"id\":42,\"first_name\":\"Phil\",\"is_something\":true,\"private\":true}", json);
            }

            [Fact]
            public void OmitsPropertiesWithNullValue()
            {
                var item = new
                {
                    Object = (object)null,
                    NullableInt = (int?)null,
                    NullableBool = (bool?)null
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{}", json);
            }

            [Fact]
            public void DoesNotOmitsNullablePropertiesWithAValue()
            {
                var item = new
                {
                    Object = new { Id = 42 },
                    NullableInt = (int?)1066,
                    NullableBool = (bool?)true
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"object\":{\"id\":42},\"nullable_int\":1066,\"nullable_bool\":true}", json);
            }

            [Fact]
            public void HandlesMixingNullAndNotNullData()
            {
                var item = new
                {
                    Int = 42,
                    Bool = true,
                    NullableInt = (int?)null,
                    NullableBool = (bool?)null
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"int\":42,\"bool\":true}", json);
            }

            [Fact]
            public void HandleUnicodeCharacters()
            {
                const string backspace = "\b";
                const string tab = "\t";

                var sb = new StringBuilder();
                sb.Append("My name has Unicode characters");
                Enumerable.Range(0, 19).Select(e => System.Convert.ToChar(e))
                .Aggregate(sb, (a, b) => a.Append(b));
                sb.Append(backspace).Append(tab);
                var data = sb.ToString();

                var json = new SimpleJsonSerializer().Serialize(data);
                var lastTabCharacter = (json
                        .Reverse()
                        .Skip(1)
                        .Take(2)
                        .Reverse()
                    .Aggregate(new StringBuilder(), (a, b) => a.Append(b)));

                var deserializeData = new SimpleJsonSerializer().Deserialize<string>(json);

                Assert.True(lastTabCharacter.ToString().Equals("\\t"));
                Assert.Equal(data, deserializeData);
            }

            [Fact]
            public void HandlesBase64EncodedStrings()
            {
                var item = new SomeObject
                {
                    Name = "Ferris Bueller",
                    Content = "Day off",
                    Description = "stuff"
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"name\":\"RmVycmlzIEJ1ZWxsZXI=\",\"description\":\"stuff\",\"content\":\"RGF5IG9mZg==\"}", json);
            }
        }


        public class TheDeserializeMethod
        {
            [Fact]
            public void DeserializesEventInfosWithUnderscoresInName()
            {
                const string json = "{\"event\":\"head_ref_deleted\"}";
                new SimpleJsonSerializer().Deserialize<EventInfo>(json);
            }

            public class MessageSingle
            {
                public string Message { get; private set; }
            }

            [Fact]
            public void DeserializesStringsWithHyphensAndUnderscoresIntoString()
            {
                const string json = @"{""message"":""-my-test-string_with_underscores_""}";

                var response = new SimpleJsonSerializer().Deserialize<MessageSingle>(json);
                Assert.Equal("-my-test-string_with_underscores_", response.Message);
            }

            public class MessageList
            {
                public IReadOnlyList<string> Message { get; private set; }
            }

            [Fact]
            public void DeserializesStringsWithHyphensAndUnderscoresIntoStringList()
            {
                const string json = @"{""message"":""-my-test-string_with_underscores_""}";

                var response = new SimpleJsonSerializer().Deserialize<MessageList>(json);
                Assert.Equal("-my-test-string_with_underscores_", response.Message[0]);
            }

            [Fact]
            public void UnderstandsRubyCasing()
            {
                const string json = "{\"id\":42,\"first_name\":\"Phil\",\"is_something\":true,\"private\":true}";

                var sample = new SimpleJsonSerializer().Deserialize<Sample>(json);

                Assert.Equal(42, sample.Id);
                Assert.Equal("Phil", sample.FirstName);
                Assert.True(sample.IsSomething);
                Assert.True(sample.Private);
            }

            [Fact]
            public void DeserializesPublicReadonlyAutoProperties()
            {
                const string json = "{\"content\":\"hello\"}";

                var someObject = new SimpleJsonSerializer().Deserialize<ReadOnlyAutoProperties>(json);

                Assert.Equal("hello", someObject.Content);
            }

            public class ReadOnlyAutoProperties
            {
                public string Content { get; private set; }
            }

            [Fact]
            public void DeserializesProtectedProperties()
            {
                const string json = "{\"content\":\"hello\"}";

                var someObject = new SimpleJsonSerializer().Deserialize<AnotherObject>(json);

                Assert.Equal("*hello*", someObject.Content);
            }

            public class AnotherObject
            {
                [Parameter(Key = "content")]
                protected string EncodedContent { get; set; }

                public string Content { get { return "*" + EncodedContent + "*"; } }
            }

            [Fact]
            public void HandlesBase64EncodedStrings()
            {
                const string json = "{\"name\":\"RmVycmlzIEJ1ZWxsZXI=\",\"description\":\"stuff\",\"content\":\"RGF5IG9mZg==\"}";

                var item = new SimpleJsonSerializer().Deserialize<SomeObject>(json);

                Assert.Equal("Ferris Bueller", item.Name);
                Assert.Equal("Day off", item.Content);
                Assert.Equal("stuff", item.Description);
            }

            [Fact]
            public void CanDeserializeOrganization()
            {
                const string json = "{" +
                  "\"login\": \"mono\"," +
                  "\"id\": 53395," +
                  "\"avatar_url\": \"https://avatars.githubusercontent.com/u/53395?\"," +
                  "\"gravatar_id\": \"f275a99c0b4e6044d3e81daf445f8174\"," +
                  "\"url\": \"https://api.github.com/users/mono\"," +
                  "\"html_url\": \"https://github.com/mono\"," +
                  "\"followers_url\": \"https://api.github.com/users/mono/followers\"," +
                  "\"following_url\": \"https://api.github.com/users/mono/following{/other_user}\"," +
                  "\"gists_url\": \"https://api.github.com/users/mono/gists{/gist_id}\"," +
                  "\"starred_url\": \"https://api.github.com/users/mono/starred{/owner}{/repo}\"," +
                  "\"subscriptions_url\": \"https://api.github.com/users/mono/subscriptions\"," +
                  "\"organizations_url\": \"https://api.github.com/users/mono/orgs\"," +
                  "\"repos_url\": \"https://api.github.com/users/mono/repos\"," +
                  "\"events_url\": \"https://api.github.com/users/mono/events{/privacy}\"," +
                  "\"received_events_url\": \"https://api.github.com/users/mono/received_events\"," +
                  "\"type\": \"Organization\"," +
                  "\"site_admin\": false," +
                  "\"name\": \"Mono Project\"," +
                  "\"company\": null," +
                  "\"blog\": \"http://mono-project.com\"," +
                  "\"location\": \"Boston, MA\"," +
                  "\"email\": \"mono@xamarin.com\"," +
                  "\"hireable\": null," +
                  "\"bio\": null," +
                  "\"public_repos\": 161," +
                  "\"public_gists\": 0," +
                  "\"followers\": 0," +
                  "\"following\": 0," +
                  "\"created_at\": \"2009-02-10T17:53:17Z\"," +
                  "\"updated_at\": \"2014-07-07T00:12:56Z\"" +
                "}";

                var result = new SimpleJsonSerializer().Deserialize<User>(json);

                Assert.Equal("Mono Project", result.Name);
                Assert.Null(result.Hireable);
            }

            [Fact]
            public void DeserializesInheritedProperties()
            {
                const string json = "{\"sha\":\"commit-sha\",\"url\":\"commit-url\",\"message\":\"commit-message\"}";

                var result = new SimpleJsonSerializer().Deserialize<Commit>(json);

                Assert.Equal("commit-sha", result.Sha);
                Assert.Equal("commit-url", result.Url);
                Assert.Equal("commit-message", result.Message);
            }

            [Fact]
            public void RespectsParameterKeyName()
            {
                const string json = "{\"_links\":\"blah\"}";

                var result = new SimpleJsonSerializer().Deserialize<Sample>(json);

                Assert.Equal("blah", result.Links);
            }

            [Fact]
            public void DefaultsMissingParameters()
            {
                const string json = @"{""private"":true}";

                var sample = new SimpleJsonSerializer().Deserialize<Sample>(json);

                Assert.Equal(0, sample.Id);
                Assert.Equal(null, sample.FirstName);
                Assert.False(sample.IsSomething);
                Assert.True(sample.Private);
            }
        }

        public class Sample
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public bool IsSomething { get; set; }
            public bool Private { get; set; }
            [Parameter(Key = "_links")]
            public string Links { get; set; }
        }

        public class SomeObject
        {
            [SerializeAsBase64]
            public string Name { get; set; }

            [SerializeAsBase64]
            public string Content;

            public string Description { get; set; }
        }
    }
}
