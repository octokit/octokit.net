using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableGistsTests
    {
        public static Dictionary<string, string> DictionaryWithSince
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("since")); }
        }
        public static Dictionary<string, string> DictionaryWithApiOptions
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("per_page") && d.ContainsKey("page")); }
        }

        public static Dictionary<string, string> DictionaryWithApiOptionsAndSince
        {
            get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("per_page") && d.ContainsKey("page") && d.ContainsKey("since")); }
        }

        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableGistsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAll(options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"),
                                  DictionaryWithApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAll(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), DictionaryWithSince);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAll(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), DictionaryWithApiOptionsAndSince);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAll(null));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAll(DateTimeOffset.Now, null));
            }
        }

        public class TheGetAllPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAllPublic();

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAllPublic(options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), DictionaryWithApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAllPublic(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), DictionaryWithSince);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAllPublic(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), DictionaryWithApiOptionsAndSince);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllPublic(null));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllPublic(DateTimeOffset.Now, null));
            }
        }

        public class TheGetAllStarredMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAllStarred();

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAllStarred(options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), DictionaryWithApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAllStarred(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), DictionaryWithSince);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAllStarred(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), DictionaryWithApiOptionsAndSince);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllStarred(null));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllStarred(DateTimeOffset.Now, null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAllForUser("samthedev");

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAllForUser("samthedev", options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), DictionaryWithApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                var user = "samthedev";
                client.GetAllForUser(user, since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), DictionaryWithSince);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                var user = "samthedev";
                client.GetAllForUser(user, since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), DictionaryWithApiOptionsAndSince);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser(null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser(""));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser(null, DateTimeOffset.Now));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser("", DateTimeOffset.Now));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser("samthedev", DateTimeOffset.Now, null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser("", DateTimeOffset.Now, ApiOptions.None));
            }
        }

        public class TheGetAllCommitsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAllCommits("id");

                gitHubClient.Connection.Received(1).Get<List<GistHistory>>(Arg.Is<Uri>(u => u.ToString() == "gists/id/commits"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAllCommits("id", options);

                gitHubClient.Connection.Received(1).Get<List<GistHistory>>(Arg.Is<Uri>(u => u.ToString() == "gists/id/commits"), DictionaryWithApiOptions);
            }


            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllCommits(null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllCommits(""));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllCommits("id", null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllCommits("", ApiOptions.None));
            }
        }

        public class TheGetAllForksMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAllForks("id");

                gitHubClient.Connection.Received(1).Get<List<GistFork>>(new Uri("gists/id/forks", UriKind.Relative), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAllForks("id", options);

                gitHubClient.Connection.Received(1).Get<List<GistFork>>(new Uri("gists/id/forks", UriKind.Relative), DictionaryWithApiOptions);
            }


            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForks(null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForks(""));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForks("id", null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForks("", ApiOptions.None));
            }
        }
    }
}
