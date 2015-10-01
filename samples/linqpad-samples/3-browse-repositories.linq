<Query Kind="Program">
  <NuGetReference>Octokit.Reactive</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>Octokit</Namespace>
  <Namespace>Octokit.Reactive</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main(string[] args)
{
		var userName = string.Empty;
		GitHubClient client = new GitHubClient(new Octokit.ProductHeaderValue("octokit.samples"));

		#if CMD
			userName = args[0];
			// For integration testing	
			client.Credentials = new Credentials(
			Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME"),
			Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD"));
		
		#else
			userName = "naveensrinivasan";
			// basic authentication
			//client.Credentials = new Credentials("username", "password");
		
			// or if you don't want to give an app your creds
			// you can use a token from an OAuth app
			// Here is the URL to get tokens https://github.com/settings/tokens
			// and save the token using Util.SetPassword("github","CHANGETHIS")
			client.Credentials = new Credentials(Util.GetPassword("github"));
		#endif
		
		var repositories = await client.Repository.GetAllForUser(userName);
		repositories.Select(r => new { r.Name }).Dump(userName + "Repos");

		// and then fetch the repositories for the current user
		var repos = await client.Repository.GetAllForCurrent();
		repos.Select(r => r.Name).Dump("Your Repos");
}