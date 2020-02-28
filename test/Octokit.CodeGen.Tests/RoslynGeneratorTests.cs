using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class RoslynGeneratorTests
    {
        [Fact]
        public void CanGenerateAStubCodeFileFromSomeModel()
        {
            var stub = new ApiBuilderResult
            {
              FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
              InterfaceName = "ISomeSortOfClient",
              ClassName = "SomeSortOfClient"
            };

            var result = RoslynGenerator.GenerateSourceFile(stub);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            Assert.Equal("ISomeSortOfClient", interfaceNode.Identifier.ValueText);
            var methodNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            Assert.Equal("SomeSortOfClient", methodNode.Identifier.ValueText);

            var baseClasses = methodNode.DescendantNodes().OfType<BaseTypeSyntax>();
            Assert.Single(baseClasses.Where(b => b.DescendantNodes().OfType<IdentifierNameSyntax>().Any(b => b.Identifier.ValueText == "ISomeSortOfClient")));
        }
    }
}
