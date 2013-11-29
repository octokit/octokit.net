 param (
    [string]$Target = "Default",
    [string]$BuildMode = "Release"
 )

function Write-Warning($string) {
    Write-Host $string -Foreground Yellow
}

function Get-Token {
 param (
    [Parameter(Mandatory=$true)][string]$UserName,
    [Parameter(Mandatory=$true)][string]$Password,
    [Parameter(Mandatory=$true)][string]$ClientId,
    [Parameter(Mandatory=$true)][string]$ClientSecret
 )

    $authorization = [System.Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($UserName + ":" + $Password)) 

    $req = [System.Net.HttpWebRequest]::Create("https://api.github.com/authorizations/clients/$ClientId");
    $req.Headers["Authorization"] = "Basic " + $authorization
    $req.UserAgent = "Octokit.net Build Script"
    $req.Method = "PUT"
    $stream = new-object System.IO.StreamWriter($req.GetRequestStream())
    $stream.WriteLine("{ `"client_secret`": `"$ClientSecret`", `"scopes`": [ `"repo`", `"user`", `"delete_repo`" ], `"note`": `"just testing Octokit`" }")
    $stream.Close()

    $resp = $req.GetResponse()
    $stream = new-object System.IO.StreamReader($resp.GetResponseStream())
    $text = $stream.ReadToEnd()
    $resp.Close()

    $json = ConvertFrom-Json $text
    $json.token
}

function Validate-Token {

  # TODO: if you have a token in your environment variables, validate it against the API
  # TODO: if it is valid, bail out


  $username = $env:OCTOKIT_GITHUBUSERNAME
  $password = $env:OCTOKIT_GITHUBPASSWORD
  $clientId = $env:OCTOKIT_GITHUBCLIENTID
  $clientSecret = $env:OCTOKIT_GITHUBCLIENTSECRET

  if ($username -ne $null `
     -and $password -ne $null `
     -and $clientId -ne $null `
     -and $clientSecret -ne $null) {

    Write-Host "brb fetching you a token"

    $token = Get-Token -UserName $username `
                       -Password $password `
                       -ClientId $clientId `
                       -ClientSecret $clientSecret

    # set the token for the current session
    $env:OCTOKIT_OAUTHTOKEN = $token
    # set the token for future sessions as well
    [Environment]::SetEnvironmentVariable("OCTOKIT_OAUTHTOKEN", $token, "User")

  } else {
    Write-Warning "You should setup an OAuth app to test Octokit.net, as this will enable a higher API limit"
    Write-Warning "Simply sign into your GitHub account and then visit https://github.com/settings/applications/new"
    Write-Warning "Put in whatever values you like, they're not relevant to the task at hand"
    Write-Warning "And then set these environment variables to pass them to the script"
    Write-Host ""
    Write-Warning "OCTOKIT_GITHUBUSERNAME: your GitHub account to authorize and use against the API"
    Write-Warning "OCTOKIT_GITHUBPASSWORD: the password for your GitHub account"
    Write-Warning "OCTOKIT_CLIENTID: the Client ID for your OAuth application"
    Write-Warning "OCTOKIT_CLIENTSECRET: the client Secret for your OAuth application"
  }
}

& "tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.2.0"

# because we want to run specific steps inline on qed
# we need to break the dependency chain
# this ensures we do a build before running any tests

$postBuildTasks = "Default", "UnitTests", "IntegrationTests", "CreatePackages"

if ($postBuildTasks -contains $Target) {
  & "tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=BuildApp" "buildMode=$BuildMode"
  Validate-Token
}
& "tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=$Target" "buildMode=$BuildMode"