using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;


namespace Octokit.Tests.Reactive
{
    public class ObservableGistsTests
    {
        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNull()
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.EmptyDictionary, null);
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
                                  Args.DictionaryWithApiOptions, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAll(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.DictionaryWithSince, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAll(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"),
                     Args.DictionaryWithApiOptionsAndSince, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), Args.EmptyDictionary, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                                  Args.DictionaryWithApiOptions, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAllPublic(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), Args.DictionaryWithSince, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAllPublic(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                     Args.DictionaryWithApiOptionsAndSince, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), Args.EmptyDictionary, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                                  Args.DictionaryWithApiOptions, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAllStarred(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), Args.DictionaryWithSince, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAllStarred(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                     Args.DictionaryWithApiOptionsAndSince, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), Args.EmptyDictionary, null);
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

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"),
                                  Args.DictionaryWithApiOptions, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                var user = "samthedev";
                client.GetAllForUser(user, since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"), Args.DictionaryWithSince, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                var user = "samthedev";
                client.GetAllForUser(user, since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "users/samthedev/gists"),
                     Args.DictionaryWithApiOptionsAndSince, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser(null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser(""));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser(null, DateTimeOffset.Now));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser("", DateTimeOffset.Now));
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAllForUser("samthedev",DateTimeOffset.Now, null));
                Assert.Throws<ArgumentException>(() => gitsClient.GetAllForUser("",DateTimeOffset.Now, ApiOptions.None));
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

                gitHubClient.Connection.Received(1).Get<List<GistHistory>>(Arg.Is<Uri>(u => u.ToString() == "gists/id/commits"), Args.EmptyDictionary, null);
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

                gitHubClient.Connection.Received(1).Get<List<GistHistory>>(Arg.Is<Uri>(u => u.ToString() == "gists/id/commits"),
                                  Args.DictionaryWithApiOptions, null);
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

                gitHubClient.Connection.Received(1).Get<List<GistFork>>(new Uri("gists/id/forks", UriKind.Relative), Args.EmptyDictionary, null);
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

                gitHubClient.Connection.Received(1).Get<List<GistFork>>(new Uri("gists/id/forks", UriKind.Relative),
                                  Args.DictionaryWithApiOptions, null);
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
