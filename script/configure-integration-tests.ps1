# TODO: this should indicate whether a variable is required or optional

function SetVariable([string]$key, [string]$value)
{
    [environment]::SetEnvironmentVariable($key, $value, "User")
    [environment]::SetEnvironmentVariable($key, $value)
}

function AskYesNoQuestion([string]$question, [string]$key)
{
  $answer = Read-Host -Prompt ($question + " Press Y to set this, otherwise we'll skip it")
  if ($answer -eq "Y")
  {
      SetVariable $key "YES"
  }
  else
  {
      SetVariable $key $null
  }

  Write-Host
}

function VerifyEnvironmentVariable([string]$friendlyName, [string]$key, [bool]$optional = $false)
{
    if ($optional -eq $true)
    {
       $label = "(optional)"
    }
    else
    {
       $label = "(required)"
    }

    $existing_value = [environment]::GetEnvironmentVariable($key,"User")
    if ($existing_value -eq $null)
    {
       $value = Read-Host -Prompt "Set the $friendlyName to use for the integration tests $label"
       SetVariable $key $value
    }
    else
    {
       Write-Host "$existing_value found as the configured $friendlyName"
       $reset = Read-Host -Prompt "Want to change this? Press Y, otherwise we'll move on"
       if ($reset -eq "Y")
       {
           $value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
           SetVariable $key $value
       }

       if ($optional -eq $true)
       {
            $clear = Read-Host -Prompt 'Want to remove this optional value, press Y'
            if ($clear -eq "Y")
            {
                SetVariable $key $null
            }
       }
    }

    Write-Host
}

Write-Host
Write-Host "BIG FREAKING WARNING!!!!!" 
Write-Host "You should use a test account when running the Octokit integration tests!"
Write-Host
Write-Host

VerifyEnvironmentVariable "account name" "OCTOKIT_GITHUBUSERNAME"
VerifyEnvironmentVariable "account password" "OCTOKIT_GITHUBPASSWORD" $true
VerifyEnvironmentVariable "OAuth token" "OCTOKIT_OAUTHTOKEN"

AskYesNoQuestion "Do you have private repositories associated with your test account?" "OCTOKIT_PRIVATEREPOSITORIES"

VerifyEnvironmentVariable "organization name" "OCTOKIT_GITHUBORGANIZATION" $true

VerifyEnvironmentVariable "application ClientID" "OCTOKIT_CLIENTID" $true
VerifyEnvironmentVariable "application Secret" "OCTOKIT_CLIENTSECRET" $true