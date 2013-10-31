using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyVersion(SolutionInfo.Version + ".0")]
[assembly: AssemblyInformationalVersion(SolutionInfo.Version)]
[assembly: AssemblyFileVersion(SolutionInfo.Version + ".0")]

[assembly: ComVisible(false)]

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("GitHub")]
[assembly: AssemblyProduct("Octokit")]
[assembly: AssemblyCopyright("Copyright © GitHub 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: InternalsVisibleTo("Octokit.Tests")]
[assembly: InternalsVisibleTo("Octokit.Tests-NetCore45")]
[assembly: CLSCompliant(false)]

class SolutionInfo
{
    public const string Version = "0.1.1";
}
