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
        public void GenerateSourceNode_WithStubDefinition_DefinesEmptyInterfaceAndClass()
        {
            var metadata = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                Client = new ApiClientMetadata
                {
                    InterfaceName = "ISomeSortOfClient",
                    ClassName = "SomeSortOfClient",
                }
            };

            var result = RoslynGenerator.GenerateSourceNode(metadata);

            var interfaceNode = Assert.Single(result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            Assert.Equal("ISomeSortOfClient", interfaceNode.Identifier.ValueText);
            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            Assert.Equal("SomeSortOfClient", classNode.Identifier.ValueText);

            var baseClasses = classNode.DescendantNodes().OfType<BaseTypeSyntax>();
            Assert.Single(baseClasses.Where(b => b.DescendantNodes().OfType<IdentifierNameSyntax>().Any(b => b.Identifier.ValueText == "ISomeSortOfClient")));
        }

        [Fact]
        public void GenerateSourceNode_AddsMethod_ToInterfaceAndImplementation()
        {
            var metadata = new ApiClientFileMetadata
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

            var result = RoslynGenerator.GenerateSourceNode(metadata);

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
        public void GenerateSourceNode_UsesSourceMetadata_ToAddAttributes()
        {
            var metadata = new ApiClientFileMetadata
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

            var result = RoslynGenerator.GenerateSourceNode(metadata);

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
        public void GenerateSourceNode_UsesSourceMetadata_AssignsStringLiteralInUrlConstructorInMethod()
        {
            var metadata = new ApiClientFileMetadata
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
                    ReturnType = new TaskOfListType("SomeResponseType"),
                    SourceMetadata = new SourceRouteMetadata
                    {
                      Verb = "GET",
                      Path = "/something"
                    }
                  }
                }
              }
            };

            var result = RoslynGenerator.GenerateSourceNode(metadata);

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());

            var objectCreation = Assert.Single(classMethodNode.DescendantNodes().OfType<ObjectCreationExpressionSyntax>());
            var argumentList = Assert.Single(objectCreation.DescendantNodes().OfType<ArgumentListSyntax>());

            // because no parameters are found, we should not find any string interpolation
            Assert.Empty(argumentList.DescendantNodes().OfType<InterpolatedStringExpressionSyntax>());

            // but we should find a plain string inside the Uri constructor
            var literal = Assert.Single(argumentList.DescendantNodes().OfType<LiteralExpressionSyntax>());
            Assert.Equal(SyntaxKind.StringLiteralExpression, literal.Kind());
            Assert.Equal(SyntaxKind.StringLiteralToken, literal.Token.Kind());
            Assert.Equal("something", literal.Token.ValueText);
        }

        [Fact]
        public void GenerateSourceNode_UsesSourceMetadata_AssignsInterpolatedStringWithPlaceholdersForParameters()
        {
            var metadata = new ApiClientFileMetadata
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
                      new ApiParameterMetadata
                      {
                        Name = "org",
                        Replaces = "org",
                        Type = "string"
                      },
                      new ApiParameterMetadata
                      {
                        Name = "migrationId",
                        Replaces = "migration_id",
                        Type = "number"
                      }
                    },
                    ReturnType = new TaskOfListType("SomeResponseType"),
                    SourceMetadata = new SourceRouteMetadata
                    {
                      Verb = "GET",
                      Path = "/orgs/{org}/migrations/{migration_id}/repositories"
                    }
                  }
                }
              }
            };

            var result = RoslynGenerator.GenerateSourceNode(metadata);

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());

            var objectCreation = Assert.Single(classMethodNode.DescendantNodes().OfType<ObjectCreationExpressionSyntax>());
            var argumentList = Assert.Single(objectCreation.DescendantNodes().OfType<ArgumentListSyntax>());

            // because we need to handle the parameters in the path, we should not use string literals here
            Assert.Empty(argumentList.DescendantNodes().OfType<LiteralExpressionSyntax>());

            // instead we need to find the string interpolation and walk it's descendants
            var interpolatedString = Assert.Single(argumentList.DescendantNodes().OfType<InterpolatedStringExpressionSyntax>());

            var textTokens = interpolatedString.DescendantNodes().OfType<InterpolatedStringTextSyntax>();
            Assert.Equal(3, textTokens.Count());
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "orgs/"));
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "/migrations/"));
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "/repositories"));

            var identifierNames = interpolatedString.DescendantNodes().OfType<IdentifierNameSyntax>();
            Assert.Equal(2, identifierNames.Count());
            Assert.Single(identifierNames.Where(i => i.Identifier.Text == "org"));
            Assert.Single(identifierNames.Where(i => i.Identifier.Text == "migrationId"));
        }

        [Fact]
        public void GenerateSourceNode_UsesSourceMetadata_HandlesConsecutivePlaceholdersInInterpolatedString()
        {
            var metadata = new ApiClientFileMetadata
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
                      new ApiParameterMetadata
                      {
                        Name = "owner",
                        Replaces = "owner",
                        Type = "string"
                      },
                      new ApiParameterMetadata
                      {
                        Name = "name",
                        Replaces = "name",
                        Type = "string"
                      }
                    },
                    ReturnType = new TaskOfListType("SomeResponseType"),
                    SourceMetadata = new SourceRouteMetadata
                    {
                      Verb = "GET",
                      Path = "/repos/{owner}/{name}/topics"
                    }
                  }
                }
              }
            };

            var result = RoslynGenerator.GenerateSourceNode(metadata);

            var classNode = Assert.Single(result.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var classMethodNode = Assert.Single(classNode.DescendantNodes().OfType<MethodDeclarationSyntax>());

            var objectCreation = Assert.Single(classMethodNode.DescendantNodes().OfType<ObjectCreationExpressionSyntax>());
            var argumentList = Assert.Single(objectCreation.DescendantNodes().OfType<ArgumentListSyntax>());

            // because we need to handle the parameters in the path, we should not use string literals here
            Assert.Empty(argumentList.DescendantNodes().OfType<LiteralExpressionSyntax>());

            // instead we need to find the string interpolation and walk it's descendants
            var interpolatedString = Assert.Single(argumentList.DescendantNodes().OfType<InterpolatedStringExpressionSyntax>());

            var textTokens = interpolatedString.DescendantNodes().OfType<InterpolatedStringTextSyntax>();
            Assert.Equal(3, textTokens.Count());
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "repos/"));
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "/"));
            Assert.Single(textTokens.Where(t => t.TextToken.ValueText == "/topics"));

            var identifierNames = interpolatedString.DescendantNodes().OfType<IdentifierNameSyntax>();
            Assert.Equal(2, identifierNames.Count());
            Assert.Single(identifierNames.Where(i => i.Identifier.Text == "owner"));
            Assert.Single(identifierNames.Where(i => i.Identifier.Text == "name"));
        }

        [Fact]
        public void GenerateSourceFile_WithModelsDefined_IncludesInSource()
        {
            var metadata = new ApiClientFileMetadata
            {
                FileName = Path.Join("Octokit", "Clients", "SomeSortOfClient.cs"),
                ResponseModels = new List<ApiResponseModelMetadata>
                {
                  new ApiResponseModelMetadata
                  {
                    Name = "SomeObject",
                    Kind = "response",
                    Properties = new List<ApiResponseModelProperty>
                    {
                      new ApiResponseModelProperty
                      {
                        Name = "Id",
                        Type = "number"
                      },
                      new ApiResponseModelProperty
                      {
                        Name = "Name",
                        Type = "string"
                      }
                    }
                  }
                }
            };

            var result = RoslynGenerator.GenerateSourceNode(metadata);

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
