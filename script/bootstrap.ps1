<#
.SYNOPSIS
    Configure your local development machine for building and testing Octokit
.DESCRIPTION
    Janky runs this script after checking out a revision and cleaning its
    working tree.
.PARAMETER Clean
    When true, all untracked (and ignored) files will be removed from the work
    tree. Defaults to false.
#>

Param(
    [switch]
    $Clean = $false
)

Set-StrictMode -Version Latest

try {
    # Make the output width reeeeeaaaaaly wide so our output doesn't get hard-wrapped.
    # <http://stackoverflow.com/questions/978777/powershell-output-column-width>
    $Host.UI.RawUI.BufferSize = New-Object Management.Automation.Host.Size -ArgumentList 5000, 25
} catch {
    # Most likely we were run in a cmd.exe terminal which won't allow setting
    # the BufferSize to such a strange size.
}

$rootDirectory = Split-Path (Split-Path $MyInvocation.MyCommand.Path)

Push-Location $rootDirectory

Import-Module (Join-Path $rootDirectory "script\modules\BuildUtils.psm1") 3>$null # Ignore warnings

if ($Clean) {
    Write-Output "Cleaning work tree..."
    Write-Output ""

    Run-Command -Quiet -Fatal { git clean -xdf }
}

$output = & git config --local core.autocrlf

if ($output -ne "input") {
    Write-Warning "core.autocrlf not configured correctly for repository"
    Write-Output  "You will have problems with generate the source indexing during packaging."
    Write-Output  ""
    Write-Output  "But guess what? I can set this up for you!"
    Write-Output  "This will overwrite any changes in your local working tree."
    Write-Output  "If you want to continue, press Y."
    Write-Output  "Press any other key to skip this."
    Write-Output  ""
    $confirmation = Read-Host "Would you like me to configure this"

    if ($confirmation -eq "Y" -or $confirmation -eq "y") {
        . git config core.autocrlf input 2>&1> $null
        . git rm --cached -r . 2>&1> $null
        . git reset --hard 2>&1> $null

        Write-Output  "Done!"
    }
}

$nuget = Join-Path $rootDirectory "tools\nuget\nuget.exe"

Write-Output "Installing dependencies..."
Write-Output ""
. $nuget "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.5.3"
. $nuget "install" "xunit.runner.console" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.1.0"
. $nuget "install" "SourceLink.Fake" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.1.0"
. $nuget "install" "FSharp.Data" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.2.5"

Pop-Location
