using System.Threading.Tasks;

namespace Octokit.Generators
{
  /// <summary>
  /// Provides an entry point for code generation of various types.
  /// </summary>
  /// <remarks>
  /// The backing source for generation will either be the source files in this repo or
  /// the OpenAPI Descriptions from the GitHub REST API: https://github.com/github/rest-api-description
  /// </remarks>
  class Generator
    {

        static void Main(string[] args)
        {

          var operation = args.Length != 0 ? args[0] : "AsyncPaginationExtensions";

          if (operation == "AsyncPaginationExtensions")
          {
            Task task = Task.Run( () => AsyncPaginationExtensionsGenerator.GenerateAsync());
            task.Wait();
          }

          // Put more generation operations here, convert to case when needed.
        }
    }
}
