using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;

namespace clean_up_after_tests
{
    class Program
    {
        static readonly Regex _repoNameRegex = new Regex(@"\-[0-9]{17}$");

        static void Main()
        {
            if (Helper.Credentials == null)
            {
                Console.WriteLine("The environment variable OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBPASSWORD must be set. Exiting.");
                Console.WriteLine();
            }
            else
            {
                DeleteRepos().Wait();
            }

#if DEBUG
            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
            Console.WriteLine();
#endif
        }

        static async Task DeleteRepos()
        {
            var api = new GitHubClient(new ProductHeaderValue("Octokit.net", "clean-up-after-test.exe"))
            {
                Credentials = Helper.Credentials
            };

            Console.WriteLine("Getting all repositories for the test account.");
            var repos = await api.Repository.GetAllForCurrent();
            foreach (var repo in repos)
            {
                if (_repoNameRegex.IsMatch(repo.Name))
                {
                    await api.Repository.Delete(repo.Owner.Login, repo.Name);
                    Console.WriteLine("Deleted {0}.", repo.FullName);
                }
                else
                    Console.WriteLine("Skipped {0}.", repo.FullName);
            }
        }
    }
}
