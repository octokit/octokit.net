using System;
using System.IO;
using Cake.Core;
using Cake.Frosting;
using Octokit;

public class Program
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host = new CakeHostBuilder()
            .WithArguments(args)
            .ConfigureServices(services =>
            {
                // Use a custom settings class.
                services.UseContext<BuildContext>();

                // Use a custom lifetime to initialize the context.
                services.UseLifetime<BuildLifetime>();

                // Octokit client
                var githubClient = new GitHubClient(new ProductHeaderValue("octokit-cake-build"));

                var credentials = GetOctokitCredentials();
                if (credentials != null)
                {
                    githubClient.Credentials = credentials;
                }

                services
                    .RegisterInstance(githubClient)
                    .As(typeof(IGitHubClient));

                if (Directory.GetCurrentDirectory().EndsWith("build"))
                {
                    // Use the parent directory as the working directory.
                    services.UseWorkingDirectory("..");
                }
            })
            .Build();

        // Run the host.
        return host.Run();
    }

    private static Credentials GetOctokitCredentials()
    {
        var githubToken = Environment.GetEnvironmentVariable("OCTOKIT_OAUTHTOKEN");
        if (githubToken != null)
        {
            return new Credentials(githubToken);
        }

        var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME");
        var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");
        if (githubUsername == null || githubPassword == null)
        {
            return null;
        }

        return new Credentials(githubUsername, githubPassword);
    }
}