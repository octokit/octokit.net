**Scenario:** I have a lot of small pull requests to review, but things are a mess
- old pull requests which might be superseded by new ones, and it's hard to see from
the descriptions what the changes actually represent.

**Goal:** a list of files, ordered by the number of pull requests which modify the
file (most popular at the top). This then gives me a list of pull requests that I
can review as a group.

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;

class Program
{
    static string owner = "github";
    static string name = "gitignore";

    static InMemoryCredentialStore credentials = new InMemoryCredentialStore(new Credentials("your-token-here"));
    static ObservableGitHubClient client = new ObservableGitHubClient(new ProductHeaderValue("ophion"), credentials);

    static void Main(string[] args)
    {
        var request = new PullRequestRequest();
        var results = new Dictionary<string, List<int>>();

        // fetch all open pull requests
        client.PullRequest.GetAllForRepository(owner, name, request)
            .SelectMany(pr =>
            {
                // for each pull request, get the files and associate
                // with the pull request number - this is a bit kludgey
                return client.PullRequest.Files(owner, name, pr.Number)
                    .Select(file => Tuple.Create(pr.Number, file.FileName));
            })
            .Subscribe(data =>
                {
                    if (results.ContainsKey(data.Item2))
                    {
                        // if we're already tracking this file, add it
                        results[data.Item2].Add(data.Item1);
                    }
                    else
                    {
                        // otherwise, we create a new list
                        var list = new List<int> { data.Item1 };
                        results[data.Item2] = list;
                    }
                },
                ex =>
                {
                    Console.WriteLine("Exception found: " + ex.ToString());
                },
                () =>
                {
                    // after that's done, let's sort by the most popular files
                    var sortbyPopularity = results
                        .OrderByDescending(kvp => kvp.Value.Count);

                    // output each file with the related pull requests
                    foreach (var file in sortbyPopularity)
                    {
                        Console.WriteLine("File: {0}", file.Key);

                        foreach (var id in file.Value)
                        {
                            Console.WriteLine(" - PR: {0}", id);
                        }

                        Console.WriteLine();
                    }
                });

        // this will take a while, let's wait for user input before exiting
        Console.ReadLine();
    }
}
```


**Notes:**

I'm using the Octokit.Reactive package as this helps represent the flow than
tasks. This code also isn't very advanced - it could be more clever, but it
works for what I needed.
