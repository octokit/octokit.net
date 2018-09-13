using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit;

namespace DemoApp
{
    internal class Program
    {
        private static IReadOnlyList<Repository> repositories;

        private static void Main(string[] args)

        {
            var client = new GitHubClient(new ProductHeaderValue("ziranquliu"));
            client.Credentials = new Credentials("5e4c4ad9502292eaf0afb8870912d8062ae06019");
            repositories = client.Repository.GetAllForCurrent().Result;

            startgit();
            //var issues=client.Issue.GetAllForRepository("octokit", "octokit.net").Result;
            //var user = client.User.Get("ziranquliu").Result;
            //Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
            //    user.Name,
            //    user.PublicRepos,
            //    user.Url);
            //var pullRequestsClient = client.PullRequest;
            //var pullRequests = pullRequestsClient.GetAllForRepository(repositories[0].Id).Result;

            Console.Read();
        }

        private static void startgit()
        {
            if (repositories.Count == 0) return;
            using (Process process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = "git.exe";
                process.StartInfo.Arguments = " clone " + repositories[0].HtmlUrl + " " + repositories[0].Name;
                Console.Title = " clone " + repositories[0].Url + " " + repositories[0].Name;
                Console.WriteLine(" clone " + repositories[0].Url + " " + repositories[0].Name);
                // 必须禁用操作系统外壳程序
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();

                //string output = process.StandardOutput.ReadToEnd();

                //if (String.IsNullOrEmpty(output) == false)
                //    Console.WriteLine(output);

                //process.WaitForExit();
                //process.Close();

                // 异步获取命令行内容
                process.BeginOutputReadLine();

                // 为异步获取订阅事件
                process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            }
        }

        private static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data) == false)
                Console.WriteLine(e.Data);
        }
    }
}