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

function Die-WithOutput($exitCode, $output) {
    Write-Output $output
    Write-Output ""
    exit $exitCode
}

function Dump-Error($output) {
    $exitCode = $LastExitCode

    $errors = $output | Select-String ": error"
    if ($errors) {
        $output = "Likely errors:", $errors, "", "Full output:", $output
    }

    Die-WithOutput $exitCode $output
}

function Run-Command([scriptblock]$Command, [switch]$Fatal, [switch]$Quiet) {
    $output = ""
    if ($Quiet) {
        $output = & $Command 2>&1
    } else {
        & $Command
    }

    if (!$Fatal) {
        return
    }

    $exitCode = 0
    if ($LastExitCode -ne 0) {
        $exitCode = $LastExitCode
    } elseif (!$?) {
        $exitCode = 1
    } else {
        return
    }

    $error = "Error executing command ``$Command``."
    if ($output) {
        $error = "$error Output:", $output
    }
    Die-WithOutput $exitCode $error
}

if ($Clean) {
    Write-Output "Cleaning work tree..."
    Write-Output ""

    Run-Command -Quiet -Fatal { git clean -xdf }
}


Write-Output "Installing FAKE..."
Write-Output ""
.\tools\nuget\nuget.exe "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-Version" "2.18.1"

Write-Output "Building Octokit..."
Write-Output ""
$output = & .\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=BuildApp" "buildMode=Release"
if ($LastExitCode -ne 0) {
    Dump-Error($output)
}

Write-Output "Running unit tests..."
Write-Output ""
$output = & .\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=UnitTests" "buildMode=Release"
if ($LastExitCode -ne 0) {
    Dump-Error($output)
}


Write-Output "Running convention tests..."
Write-Output ""
$output = & .\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=ConventionTests" "buildMode=Release"
if ($LastExitCode -ne 0) {
    Dump-Error($output)
}

Write-Output "Running integration tests..."
Write-Output ""
$output = & .\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=IntegrationTests" "buildMode=Release"
if ($LastExitCode -ne 0) {
    Dump-Error($output)
}

Write-Output "Creating NuGet packages..."
Write-Output ""
$output = & .\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=CreatePackages" "buildMode=Release"
if ($LastExitCode -ne 0) {
    Dump-Error($output)
}

$exitCode = 0

exit $exitCode
