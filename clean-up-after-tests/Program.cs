using System;
using Octokit.Tests.Integration;

namespace clean_up_after_tests
{
    class Program
    {
        static void Main()
        {
            if (Helper.Credentials == null)
            {
                Console.WriteLine("The environment variable OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBPASSWORD must be set. Exiting.");
                Console.WriteLine();
            }

#if DEBUG
            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
            Console.WriteLine();
#endif
        }
    }
}
