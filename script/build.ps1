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

if ($Clean) {
    Write-Output "Cleaning work tree..."
    Write-Output ""

    Run-Command -Quiet -Fatal { git clean -xdf }
}

Write-Output "Installing dependencies..."
Write-Output ""
.\tools\nuget\nuget.exe "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.5.1"
.\tools\nuget\nuget.exe "install" "xunit.runner.console" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.1.0"
.\tools\nuget\nuget.exe "install" "SourceLink.Fake" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.1.0"

Write-Output "Building Octokit..."
Write-Output ""
.\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=BuildApp" "buildMode=Release"

Write-Output "Running unit tests..."
Write-Output ""
.\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=UnitTests" "buildMode=Release"

Write-Output "Running convention tests..."
Write-Output ""
.\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=ConventionTests" "buildMode=Release"