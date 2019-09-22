
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
	
	return ($answer -eq "Y")
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
		if ($optional -eq $true)
		{
			$clear = Read-Host -Prompt 'Want to remove or change this optional value, press Y'
			if ($clear -eq "Y")
			{
				$reset = Read-Host -Prompt "Press R to remove and Press C to change the value, otherwise we'll move on"
				if ($reset -eq "C")
				{
					$value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
       				SetVariable $key $value
				}
				elseif ($reset -eq "R")
				{
					SetVariable $key $null
				}
			}
		}
		else
		{
			$reset = Read-Host -Prompt "Press Y to change this value, otherwise we'll move on "
			if ($reset -eq "Y")
			{
				$value = Read-Host -Prompt "Change the $friendlyName to use for the integration tests"
       			SetVariable $key $value
			}
		}
	}

	Write-Host
}

Write-Host
Write-Warning "BIG FREAKING WARNING!!!!!" 
Write-Warning "You should use a test account when running the Octokit integration tests!"
Write-Host
Write-Host

VerifyEnvironmentVariable "test account name" "OCTOKIT_GITHUBUSERNAME"
VerifyEnvironmentVariable "test account password" "OCTOKIT_GITHUBPASSWORD" $true
VerifyEnvironmentVariable "test account OAuth token" "OCTOKIT_OAUTHTOKEN"

if (AskYesNoQuestion "Some tests require a second test account, do you want to set one up?" "OCTOKIT_PRIVATEREPOSITORIES")
{
	VerifyEnvironmentVariable "Second test account name" "OCTOKIT_GITHUBUSERNAME_2"
	VerifyEnvironmentVariable "Second account password" "OCTOKIT_GITHUBPASSWORD_2"
}

AskYesNoQuestion "Do you have private repositories associated with your test account?" "OCTOKIT_PRIVATEREPOSITORIES" | Out-Null

VerifyEnvironmentVariable "organization name" "OCTOKIT_GITHUBORGANIZATION" $true

VerifyEnvironmentVariable "Override GitHub URL" "OCTOKIT_CUSTOMURL" $true

VerifyEnvironmentVariable "application ClientID" "OCTOKIT_CLIENTID" $true
VerifyEnvironmentVariable "application Secret" "OCTOKIT_CLIENTSECRET" $true

if (AskYesNoQuestion "Do you wish to setup GitHubApps integration test settings?" "OCTOKIT_GITHUBAPP_ENABLED")
{
	VerifyEnvironmentVariable "GitHub App ID" "OCTOKIT_GITHUBAPP_ID"
	VerifyEnvironmentVariable "GitHub App SLUG" "OCTOKIT_GITHUBAPP_SLUG"
	VerifyEnvironmentVariable "GitHub App Pem File" "OCTOKIT_GITHUBAPP_PEMFILE"
}

if (AskYesNoQuestion "Do you wish to enable GitHub Enterprise (GHE) Integration Tests?" "OCTOKIT_GHE_ENABLED")
{
    VerifyEnvironmentVariable "GitHub Enterprise account name" "OCTOKIT_GHE_USERNAME"
    VerifyEnvironmentVariable "GitHub Enterprise account password" "OCTOKIT_GHE_PASSWORD" $true
    VerifyEnvironmentVariable "GitHub Enterprise Management Console password" "OCTOKIT_GHE_CONSOLEPASSWORD" $true
    VerifyEnvironmentVariable "GitHub Enterprise OAuth token" "OCTOKIT_GHE_OAUTHTOKEN"

    VerifyEnvironmentVariable "GitHub Enterprise organization name" "OCTOKIT_GHE_ORGANIZATION" $true

    VerifyEnvironmentVariable "GitHub Enterprise URL" "OCTOKIT_GHE_URL" $true

    VerifyEnvironmentVariable "GitHub Enterprise application ClientID" "OCTOKIT_GHE_CLIENTID" $true
    VerifyEnvironmentVariable "GitHub Enterprise application Secret" "OCTOKIT_GHE_CLIENTSECRET" $true
}