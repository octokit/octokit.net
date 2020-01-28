using Cake.Common.Tools.GitVersion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Common;
using Cake.Common.IO;
using Cake.Frosting;

public static class GitVersionRunner
{
    public static GitVersion Run(Context context, GitVersionOutput outputType)
    {
        if (outputType == GitVersionOutput.BuildServer)
        {
            var exitCode = context.StartProcess("dotnet", new ProcessSettings
            {
                Arguments = $"gitversion -output buildserver"
            });

            if (exitCode != 0)
            {
                throw new Exception("gitversion failed!");
            }

            return new GitVersion();
        }
        else if (outputType == GitVersionOutput.Json)
        {
            IEnumerable<string> stdout;
            var jsonString = string.Empty;

            var exitCode = context.StartProcess("dotnet", new ProcessSettings
            {
                Arguments = $"gitversion -output json",
                RedirectStandardOutput = true,
            },
            out stdout);

            if (exitCode != 0)
            {
                throw new Exception("gitversion failed!");
            }

            jsonString = string.Join("\n", stdout);

            var jsonSerializer = new DataContractJsonSerializer(typeof(GitVersion));

            using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (jsonSerializer.ReadObject(jsonStream) as GitVersion);
            }
        }

        return new GitVersion();
    }
}
