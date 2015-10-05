<Query Kind="Program">
  <NuGetReference>Octokit</NuGetReference>
  <NuGetReference>Octokit.Reactive</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>Octokit</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>Octokit.Reactive</Namespace>
</Query>

void Main(string[] args)
{
	var owner = string.Empty;
	
	#if CMD
		owner = args[0];
	#else
		owner = "octokit";
	#endif
	
	var client = new ObservableGitHubClient(new Octokit.ProductHeaderValue("Octokit.samples"));
	client.Repository.GetAllForUser(owner).Select(r => r.Name).Dump();
}