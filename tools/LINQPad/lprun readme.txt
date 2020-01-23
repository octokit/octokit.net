LPRun.exe is the command-line version of LINQPad. Call LPRun with no arguments for help.

You can improve startup performance by generating native images for the Roslyn assemblies.
To do this, open the administrator command prompt, and call:

	LINQPad.exe -ngen 

This generates local native images that will be used by both LPRun.exe and LINQPad.exe.
It does not install anything to the GAC.