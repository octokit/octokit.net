using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Octokit.CodeGen.Tests
{
    // TODO: tests for implementing the method body for each call
    // TODO: tests for rendering documentation for the interface (and inherit-doc for the implementation)

    public class RoslynGeneratorTests
    {
        [Fact]
        public void CanGenerateAStubCodeFileFromSomeModel()
        {
            var stub = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                Client = new ApiClientMetadata
                {
                    InterfaceName = "ISomeSortOfClient",
                    ClassName = "SomeSortOfClient",
                }
            };

            var result = RoslynGenerator.GenerateSourceNode(stub);

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
            var stub = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                Client = new ApiClientMetadata
                {
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
                      ReturnType = new TaskOfListType("SomeResponseType"),
                      SourceMetadata = new SourceRouteMetadata
                      {
                        Path ="/some/sort/of/endpoint",
                        Verb = "GET"
                      }
                    }
                  }
                },

            };

            var expectedReturnType = GetListReturnType("SomeResponseType");

            var result = RoslynGenerator.GenerateSourceNode(stub);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            var interfaceMethodNode = Assert.Single(interfaceNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", interfaceMethodNode.Identifier.ValueText);

            Assert.Equal(expectedReturnType.ToString(), interfaceMethodNode.ReturnType.ToString());

            var parameter = Assert.Single(interfaceNode.DescendantNodes().OfType<ParameterSyntax>());
            Assert.Equal("userId", parameter.Identifier.ValueText);

            var longNode = PredefinedType(Token(SyntaxKind.LongKeyword));
            Assert.Equal(longNode.ToString(), parameter.Type.ToString());

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", classMethodNode.Identifier.ValueText);
            Assert.Equal(expectedReturnType.ToString(), classMethodNode.ReturnType.ToString());

            parameter = Assert.Single(classMethodNode.DescendantNodes().OfType<ParameterSyntax>());
            Assert.Equal("userId", parameter.Identifier.ValueText);
            Assert.Equal(longNode.ToString(), parameter.Type.ToString());
        }

        [Fact]
        public void GenerateSourceFile_UsesSourceMetadata_ToAddAttributes()
        {
            var stub = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                Client =
              {
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
                    ReturnType = new TaskOfListType("SomeResponseType"),
                    SourceMetadata = new SourceRouteMetadata
                    {
                      Verb = "Get",
                      Path = "/something"
                    }
                  }
                }
              }
            };

            var result = RoslynGenerator.GenerateSourceNode(stub);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            var interfaceMethodNode = Assert.Single(interfaceNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", interfaceMethodNode.Identifier.ValueText);

            // no attributes defined for interface methods
            var attributes = interfaceMethodNode.DescendantNodes().OfType<AttributeSyntax>();
            Assert.Empty(attributes);

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());
            Assert.Equal("GetAll", classMethodNode.Identifier.ValueText);

            attributes = classMethodNode.DescendantNodes().OfType<AttributeSyntax>();
            var attribute = Assert.Single(attributes);

            Assert.Equal("GeneratedRoute", attribute.Name.ToString());
        }

        [Fact]
        public void GenerateSourceFile_WithModelsDefined_IncludesInSource()
        {
            var stub = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                Models = new List<ApiModelMetadata>
                {
                  new ApiModelMetadata
                  {
                    Name = "SomeObject",
                    Kind = "response",
                    Properties = new List<ApiModelProperty>
                    {
                      new ApiModelProperty
                      {
                        Name = "Id",
                        Type = "number"
                      },
                      new ApiModelProperty
                      {
                        Name = "Name",
                        Type = "string"
                      }
                    }
                  }
                }
            };

            var result = RoslynGenerator.GenerateSourceNode(stub);

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var propertyNodes = classNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();

            Assert.Equal(2, propertyNodes.Count());
        }

        private static TypeSyntax GetListReturnType(string innerType)
        {
            return GenericName(Identifier("Task"))
                          .WithTypeArgumentList(
                              TypeArgumentList(
                                  SingletonSeparatedList<TypeSyntax>(
                                      GenericName(
                                          Identifier("IReadOnlyList"))
                                      .WithTypeArgumentList(
                                          TypeArgumentList(
                                              SingletonSeparatedList<TypeSyntax>(
                                                  IdentifierName(innerType)))))));
        }
    }
}
