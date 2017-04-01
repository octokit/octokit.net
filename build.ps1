<#
.SYNOPSIS
This is a Powershell script to bootstrap a Cake.Frosting build.
.DESCRIPTION
This Powershell script will download NuGet if missing, restore NuGet tools (including Cake)
and execute your Cake build script with the parameters you provide.
.PARAMETER Target
The build script target to run.
.PARAMETER Configuration
The build configuration to use.
.PARAMETER Verbosity
Specifies the amount of information to be displayed.
.PARAMETER WhatIf
Performs a dry run of the build script.
No tasks will be executed.
.PARAMETER ScriptArgs
Remaining arguments are added here.
.LINK
https://github.com/cake-build/frosting
#>

[CmdletBinding()]
Param(
    [string]$Target = "Default",
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [switch]$LinkSources,
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")]
    [string]$Verbosity = "Verbose",
    [switch]$WhatIf,
    [Parameter(Position=0,Mandatory=$false,ValueFromRemainingArguments=$true)]
    [string[]]$ScriptArgs
)

$DotNetChannel = "preview";
$DotNetVersion = "1.0.1";
$DotNetInstallerUri = "https://raw.githubusercontent.com/dotnet/cli/rel/1.0.1/scripts/obtain/dotnet-install.ps1";
$NugetUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

# Make sure tools folder exists
$PSScriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent
$ToolPath = Join-Path $PSScriptRoot "tools"
if (!(Test-Path $ToolPath)) {
    Write-Verbose "Creating tools directory..."
    New-Item -Path $ToolPath -Type directory | out-null
}

###########################################################################
# INSTALL .NET CORE CLI
###########################################################################

Function Remove-PathVariable([string]$VariableToRemove)
{
  $path = [Environment]::GetEnvironmentVariable("PATH", "User")
  $newItems = $path.Split(';') | Where-Object { $_.ToString() -inotlike $VariableToRemove }
  [Environment]::SetEnvironmentVariable("PATH", [System.String]::Join(';', $newItems), "User")
  $path = [Environment]::GetEnvironmentVariable("PATH", "Process")
  $newItems = $path.Split(';') | Where-Object { $_.ToString() -inotlike $VariableToRemove }
  [Environment]::SetEnvironmentVariable("PATH", [System.String]::Join(';', $newItems), "Process")
}

# Get .NET Core CLI path if installed.
$FoundDotNetCliVersion = $null;
if (Get-Command dotnet -ErrorAction SilentlyContinue) {
    $FoundDotNetCliVersion = dotnet --version;
}

if($FoundDotNetCliVersion -ne $DotNetVersion) {
    $InstallPath = Join-Path $PSScriptRoot ".dotnet"
    if (!(Test-Path $InstallPath)) {
        mkdir -Force $InstallPath | Out-Null;
    }
    (New-Object System.Net.WebClient).DownloadFile($DotNetInstallerUri, "$InstallPath\dotnet-install.ps1");
    & $InstallPath\dotnet-install.ps1 -Channel $DotNetChannel -Version $DotNetVersion -InstallDir $InstallPath;

    Remove-PathVariable "$InstallPath"
    $env:PATH = "$InstallPath;$env:PATH"
    $env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
    $env:DOTNET_CLI_TELEMETRY_OPTOUT=1
}

###########################################################################
# INSTALL NUGET
###########################################################################

# Make sure nuget.exe exists.
$NugetPath = Join-Path $ToolPath "nuget.exe"
if (!(Test-Path $NugetPath)) {
    Write-Host "Downloading NuGet.exe..."
    (New-Object System.Net.WebClient).DownloadFile($NugetUrl, $NugetPath);
}

###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Build the argument list.
$Arguments = @{
    target=$Target;
    configuration=$Configuration;
    linkSources=$LinkSources;
    verbosity=$Verbosity;
    dryrun=$WhatIf;
}.GetEnumerator() | %{"--{0}=`"{1}`"" -f $_.key, $_.value };

# Start Cake
Push-Location
Set-Location build
Write-Host "Restoring packages..."
Invoke-Expression "dotnet restore"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}
Write-Host "Running build..."
Invoke-Expression "dotnet run -- $Arguments"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}
Pop-Location
