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

Write-Output "Checking core.autocrlf..."

$output = & git config --local core.autocrlf

if ($output -ne "input") {
    Write-Warning "Line endings not configured correctly"
    Write-Output  "You will have problems with running SourceLink to generate the source indexing."
    Write-Output  ""
    Write-Output  "Please run these commands from the root of the repository"
    Write-Output  "> git config core.autocrlf input"
    Write-Output  "> git rm --cached -r ."
    Write-Output  "> git reset --hard"
    Write-Output  ""
    Write-Output  "After doing that, run this script again..."
    exit
}
