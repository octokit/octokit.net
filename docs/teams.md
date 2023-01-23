# Working with Teams

To access the teams API, you need to be an authenticated member of the organization's team. OAuth access tokens require the read:org scope. The ITeamsClient houses the endpoints for the teams API.

### Create, update or delete teams

To create a new team, you need to be a member of owner of the organization.

```csharp
var newTeam = new NewTeam("team-name")
{
    Description = "my cool team description",
    Privacy = TeamPrivacy.Closed
};
newTeam.Maintainers.Add("maintainer-name");
newTeam.RepoNames.Add("repository-name");

var team = await github.Organization.Team.Create("organization-name", newTeam);
```

Updating and deleting a team is also possible

```csharp
var update = new UpdateTeam("team-name",)
{
    Description = "my new team description",
    Privacy = TeamPrivacy.Closed,
    Permission = TeamPermission.Push,
};

var team = await _github.Organization.Team.Update("organization-name", "team-slug", update);
```

```csharp
var team = await _github.Organization.Team.Delete("organization-name", "team-slug");
```

### Working with repositories for the team

You can get the list of repositories for the team by following

```csharp
var allRepositories = await github.Organization.Team.GetAllRepositories(teamContext.TeamId);
```
Or check permissions for a specific repository with the CheckTeamPermissionsForARepository method.

```csharp
await github.Organization.Team.CheckTeamPermissionsForARepository(
                        "organization-name",
                        "team-slug",
                        "repository-owner",
                        "repository-name",
                        false);
```

 The following snippet shows how to add or update team repository permissions. 
 
 Permissions can be one of pull, triage, push, maintain, admin and you can also specify a custom repository role name, if the owning organization has defined any. If no permission is specified, the team's permission attribute will be used to determine what permission to grant the team on this repository.

```csharp
await github.Organization.Team.AddOrUpdateTeamRepositoryPermissions(
                      "organization-name",
                      "team-slug",
                      "repository-owner",
                      "repository-name",
                      "admin");
```

To remove a repository form a team, use

```csharp
await github.Organization.Team.RemoveRepositoryFromATeam(
                      "organization-name",
                      "team-slug",
                      "repository-owner",
                      "repository-name");
```