param (
    [Parameter(Mandatory=$true)][string]$UserName,
    [Parameter(Mandatory=$true)][string]$Password,
    [Parameter(Mandatory=$true)][string]$ClientId,
    [Parameter(Mandatory=$true)][string]$ClientSecret
 )

[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME", $UserName, "User")
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD", $Password, "User")
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBCLIENTID", $ClientId, "User")
[Environment]::SetEnvironmentVariable("OCTOKIT_GITHUBCLIENTSECRET", $ClientSecret, "User")

Write-Host "If you're running these changes inside a shell, they won't be applied until you restart the shell" -Foreground Yellow