<#
.SYNOPSIS
    Builds and tests Octokit
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

$fake = Join-Path $rootDirectory ".\tools\FAKE.Core\tools\Fake.exe"
$script = Join-Path $rootDirectory "build.fsx"

if ((Test-Path $fake) -eq $false) {
    Write-Warning "FAKE executable not found in repository"
    Write-Output "Please run /script/bootstrap before running this again..."
    Pop-Location
    Die-WithOutput -1
}

Write-Output "Running unit tests..."
Write-Output ""
. $fake $script "target=UnitTests" "buildMode=Release"

Write-Output "Running convention tests..."
Write-Output ""
. $fake $script "target=ConventionTests" "buildMode=Release"

Pop-Location