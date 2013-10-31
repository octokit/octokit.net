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

Write-Output "Building Octokit..."
Write-Output ""
$output = .\Build-Solution.ps1 Build Release -MSBuildVerbosity quiet 2>&1
if ($LastExitCode -ne 0) {
    $exitCode = $LastExitCode

    $errors = $output | Select-String ": error"
    if ($errors) {
        $output = "Likely errors:", $errors, "", "Full output:", $output
    }

    Die-WithOutput $exitCode $output
}

function Run-XUnit([string]$platform, [string]$project, [int]$timeoutDuration, [string]$projectFolder = $project) {
    $dll = "$projectFolder\bin\Release\$platform\$project.dll"

    $xunitDirectory = Join-Path $rootDirectory tools\xunit
    $consoleRunner = Join-Path $xunitDirectory xunit.console.clr4.x86.exe
    $xml = Join-Path $rootDirectory "nunit-$project.xml"

    $output=(& $consoleRunner $dll /nunit $xml /silent /noshadow)

    $result = New-Object System.Object
    $result | Add-Member -Type NoteProperty -Name Output -Value $output
    $result | Add-Member -Type NoteProperty -Name ExitCode -Value $LastExitCode
    $result
}

$exitCode = 0

Write-Output "Running Octokit.Tests..."
$result = Run-XUnit Net45 Octokit.Tests 120
if ($result.ExitCode -eq 0) {
    # Print out the test result summary.
    Write-Output $result.Output[-1]
} else {
    $exitCode = $result.ExitCode
    Write-Output $result.Output
}
Write-Output ""

Write-Output "Running Octokit.Tests-NetCore45..."
$result = Run-XUnit NetCore45 Octokit.Tests-NetCore45 120 Octokit.Tests
if ($result.ExitCode -eq 0) {
    # Print out the test result summary.
    Write-Output $result.Output[-1]
} else {
    $exitCode = $result.ExitCode
    Write-Output $result.Output
}
Write-Output ""

Write-Output "Running Octokit.Tests.Integration..."
$result = Run-XUnit Net45 Octokit.Tests.Integration 180
if ($result.ExitCode -eq 0) {
    # Print out the test result summary.
    Write-Output $result.Output[-1]
} else {
    $exitCode = $result.ExitCode
    Write-Output $result.Output
}
Write-Output ""

exit $exitCode
