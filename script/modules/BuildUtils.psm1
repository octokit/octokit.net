Set-StrictMode -Version Latest

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
