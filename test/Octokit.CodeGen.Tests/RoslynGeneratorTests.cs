using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Octokit.CodeGen.Tests
{
    public class RoslynGeneratorTests
    {
        [Fact]
        public void CanGenerateAStubCodeFileFromSomeModel()
        {
            var stub = new ApiCodeFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                InterfaceName = "ISomeSortOfClient",
                ClassName = "SomeSortOfClient"
            };

            var result = RoslynGenerator.GenerateSourceFile(stub);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            Assert.Equal("ISomeSortOfClient", interfaceNode.Identifier.ValueText);
            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            Assert.Equal("SomeSortOfClient", classNode.Identifier.ValueText);

            var baseClasses = classNode.DescendantNodes().OfType<BaseTypeSyntax>();
            Assert.Single(baseClasses.Where(b => b.DescendantNodes().OfType<IdentifierNameSyntax>().Any(b => b.Identifier.ValueText == "ISomeSortOfClient")));
        }

        [Fact]
        public void GenerateSourceFile_AddsMethod_ToInterfaceAndImplementation()
        {
            var stub = new ApiCodeFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                InterfaceName = "ISomeSortOfClient",
                ClassName = "SomeSortOfClient",
                Methods = new List<ApiMethodMetadata>
              {
                new ApiMethodMetadata{
                  Name = "GetAll",
                  Parameters = new List<ApiParameterMetadata>
                  {
                    new ApiParameterMetadata{
                      Name = "userId",
                      Type = "number",
                    }
                  },
                  //ReturnType = new TaskOfListType("SomeResponseType")
                }
              }
            };

            var result = RoslynGenerator.GenerateSourceFile(stub);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            var interfaceMethod = Assert.Single(interfaceNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", interfaceMethod.Identifier.ValueText);

            var parameter = Assert.Single(interfaceNode.DescendantNodes().OfType<ParameterSyntax>());
            Assert.Equal("userId", parameter.Identifier.ValueText);

            var longNode = PredefinedType(Token(SyntaxKind.LongKeyword));
            Assert.Equal(longNode.Kind(), parameter.Type.Kind());

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", classMethodNode.Identifier.ValueText);

            parameter = Assert.Single(classMethodNode.DescendantNodes().OfType<ParameterSyntax>());
            Assert.Equal("userId", parameter.Identifier.ValueText);
            Assert.Equal(longNode.Kind(), parameter.Type.Kind());
        }
    }
}
