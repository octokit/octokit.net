using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Text;

namespace Octokit.Generators
{

  /// <summary>
  /// AsyncPaginationExtensionsGenerator for generating pagination extensions for Octokit.net Clients that return collections.
  /// </summary>
  /// <remarks>
  /// This generator originally appeared in https://github.com/octokit/octokit.net/pull/2516
  /// The generator solves a small part of a larger effort that is being discussed:
  ///  https://github.com/octokit/octokit.net/discussions/2499
  ///  https://github.com/octokit/octokit.net/discussions/2495
  ///  https://github.com/octokit/octokit.net/issues/2517
  ///  In the future, we should be able to unify generation for
  ///   * models (request and response)
  ///   * clients
  ///   * routing and related helpers
  ///   TODO: Convert to use Rosyln source generators
  /// </remarks>
  class AsyncPaginationExtensionsGenerator
  {

  private const string HEADER = (
@"using System;
using System.Collections.Generic;

namespace Octokit.AsyncPaginationExtension
{
  /// <summary>
  /// Provides all extensions for pagination.
  /// </summary>
  /// <remarks>
  /// The <code>pageSize</code> parameter at the end of all methods allows for specifying the amount of elements to be fetched per page.
  /// Only useful to optimize the amount of API calls made.
  /// </remarks>
  public static class Extensions
  {
    private const int DEFAULT_PAGE_SIZE = 30;
  ");

  private const string FOOTER = (
@"
  }
}");

    /// <summary>
    /// GenerateAsync static entry point for generating pagination extensions.
    /// </summary>
    /// <remarks>
    /// This defaults the search path to the root of the project
    /// This expects to generate the resulting code and put it in Octokit.AsyncPaginationExtension
    /// This does a wholesale overwrite on ./Octokit.AsyncPaginationExtension/Extensions.cs
    /// </remarks>
    public static async Task GenerateAsync(string root = "./")
    {
      var sb = new StringBuilder(HEADER);
      var enumOptions = new EnumerationOptions { RecurseSubdirectories = true };
      var paginatedCallRegex = new Regex(@".*Task<IReadOnlyList<(?<returnType>\w+)>>\s*(?<name>\w+)(?<template><.*>)?\((?<arg>.*?)(, )?ApiOptions \w*\);");

      foreach (var file in Directory.EnumerateFiles(root, "I*.cs", enumOptions)) {
          var type = Path.GetFileNameWithoutExtension(file);

          foreach (var line in File.ReadAllLines(file)) {
              var match = paginatedCallRegex.Match(line);

              if (!match.Success) { continue; }
              sb.Append(BuildBodyFromTemplate(match, type));
          }
      }

      sb.Append(FOOTER);

      await File.WriteAllTextAsync("./Octokit.AsyncPaginationExtension/Extensions.cs", sb.ToString());
    }

    /// <summary>
    /// BuildBodyFromTemplate uses the match from the regex search and parses values from the given source
    /// to use to generate the paging implementations.
    /// </summary>
    /// <remarks>
    /// TODO: This should be reworked to use source templates
    /// </remarks>
    private static string BuildBodyFromTemplate(Match match, string type)
    {
      var argSplitRegex = new Regex(@" (?![^<]*>)");
      var returnType = match.Groups["returnType"].Value;
      var name = match.Groups["name"].Value;
      var arg = match.Groups["arg"].Value;
      var template = match.Groups["template"];
      var templateStr = template.Success ? template.Value : string.Empty;
      var splitArgs = argSplitRegex.Split(arg).ToArray();

      var lambda = arg.Length == 0
          ? $"t.{name}{templateStr}"
          : $"options => t.{name}{templateStr}({string.Join(' ', splitArgs.Where((_, i) => i % 2 == 1))}, options)";

      var docArgs = string.Join(", ", splitArgs.Where((_, i) => i % 2 == 0)).Replace('<', '{').Replace('>', '}');
      if (docArgs.Length != 0) {
          docArgs += ", ";
      }

      if (arg.Length != 0) {
          arg += ", ";
      }

      return ($@"
    /// <inheritdoc cref=""{type}.{name}({docArgs}ApiOptions)""/>
    public static IPaginatedList<{returnType}> {name}Async{templateStr}(this {type} t, {arg}int pageSize = DEFAULT_PAGE_SIZE)
        => pageSize > 0 ? new PaginatedList<{returnType}>({lambda}, pageSize) : throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, ""The page size must be positive."");
    ");
    }
  }
}
