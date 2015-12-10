# TODO: this should indicate whether a variable is required or optional

set-alias ?: Invoke-Ternary -Option AllScope -Description "PSCX filter alias"
filter Invoke-Ternary ([scriptblock]$decider, [scriptblock]$ifTrue, [scriptblock]$ifFalse) 
{
   if (&$decider) { 
      &$ifTrue
   } else { 
      &$ifFalse 
   }
}

function VerifyEnvironmentVariable([string]$friendlyName, [string]$key, [bool]$optional = $false)
{
    $existing_value = [environment]::GetEnvironmentVariable($key,"User")
    if ($existing_value -eq $null)
    {
       $value = Read-Host -Prompt "Set the $friendlyName to use for the integration tests " + (?: $optional "(required)" "(optional)")
       [environment]::SetEnvironmentVariable($key, $value,"User")
    }
    else
    {
       Write-Host "$existing_value found as the configured $friendlyName"
       $reset = Read-Host -Prompt "Want to change this? Press Y, otherwise we'll move on"
       if ($reset -eq "Y")
       {
           $value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
           [environment]::SetEnvironmentVariable($key, $value, "User")
       }

       if ($optional)
       {
            $clear = Read-Host -Prompt 'Want to remove this optional value, press Y'
            if ($clear -eq "Y")
            {
                [Environment]::SetEnvironmentVariable($key,$null,"User")
            }
       }
    }
}

Write-Host "BIG FREAKING WARNING!!!!!" 
Write-Host "You should use a test account when running the Octokit integration tests!"
Write-Host
Write-Host

VerifyEnvironmentVariable "account name" "OCTOKIT_GITHUBUSERNAME"
VerifyEnvironmentVariable "organization name" "OCTOKIT_GITHUBORGANIZATION" $true
VerifyEnvironmentVariable "OAuth token" "OCTOKIT_OAUTHTOKEN"
