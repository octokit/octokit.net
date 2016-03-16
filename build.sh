#!/bin/bash
if test "$OS" = "Windows_NT"
then
  # use .Net

"./tools/nuget/nuget.exe" "install" "xunit.runner.console" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.0.0" -verbosity quiet
"./tools/nuget/nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"  -verbosity quiet
"./tools/nuget/nuget.exe" "install" "SourceLink.Fake" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.1.0"  -verbosity quiet
packages/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
else
  # use mono
mono "./tools/nuget/NuGet.exe" "install" "xunit.runner.console" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.0.0"  -verbosity quiet
mono "./tools/nuget/NuGet.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"  -verbosity quiet
mono "./tools/nuget/NuGet.exe" "install" "SourceLink.Fake" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.1.0"  -verbosity quiet
mono "./tools/nuget/NuGet.exe" "install" "System.Net.Http" "-OutputDirectory" "tools"  -verbosity quiet
mono "./tools/nuget/NuGet.exe" "install" "Microsoft.Net.Http" "-OutputDirectory" "tools"  -verbosity quiet
mono ./tools/FAKE.Core/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
fi
