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