param (
    [Parameter(Mandatory=$true)][string]$UserName,
    [Parameter(Mandatory=$true)][string]$Password,
    [Parameter(Mandatory=$true)][string]$ClientId,
    [Parameter(Mandatory=$true)][string]$ClientSecret
 )

[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME", $UserName, "User")
$env:OCTOKIT_GITHUBUSERNAME = $UserName
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD", $Password, "User")
$env:OCTOKIT_GITHUBPASSWORD = $Password
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBCLIENTID", $ClientId, "User")
$env:OCTOKIT_GITHUBCLIENTID = $ClientId
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBCLIENTSECRET", $ClientSecret, "User")
$env:OCTOKIT_GITHUBCLIENTSECRET = $ClientSecret

Write-Host "Updated!" -Foreground Green