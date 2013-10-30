@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

Powershell -ExecutionPolicy Unrestricted %~dp0Build-Solution.ps1 FullBuild %config%

rd packaging /s /q  REM delete the old stuff

if not exist packaging\octokit\lib\net45 mkdir packaging\octokit\lib\net45\
if not exist packaging\octokit\lib\netcore45 mkdir packaging\octokit\lib\netcore45\

copy LICENSE.txt packaging\octokit\
copy README.md packaging\octokit\

copy Octokit\bin\Release\Octokit.dll packaging\octokit\lib\net45\
copy Octokit\bin\WinRT\Release\OctokitRT.dll packaging\octokit\lib\netcore45\

tools\nuget\nuget.exe pack "octokit.nuspec" -BasePath packaging\octokit -Output packaging


if not exist packaging\octokit.reactive\lib\net45 mkdir packaging\octokit.reactive\lib\net45\

copy LICENSE.txt packaging\octokit.reactive\
copy README.md packaging\octokit.reactive\

copy Octokit.Reactive\bin\Release\Octokit.Reactive.dll packaging\octokit.reactive\lib\net45\

tools\nuget\nuget.exe pack "octokit.reactive.nuspec" -BasePath packaging\octokit.reactive -Output packaging