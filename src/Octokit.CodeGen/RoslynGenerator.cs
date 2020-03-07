using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Octokit.CodeGen
{
    public class RoslynGenerator
    {
        private static TypeSyntax ConvertToReturnType<T>(T taskOfSomeType) where T : IResponseType
        {
            var listTask = taskOfSomeType as TaskOfListType;
            if (listTask != null)
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
                                                      IdentifierName(listTask.ListType)))))));
            }

            var objectTask = taskOfSomeType as TaskOfType;
            if (objectTask != null)
            {
                var innerType = ConvertToTypeSyntax(objectTask.Type);
                return GenericName(Identifier("Task"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(innerType)));
            }

            return PredefinedType(Token(SyntaxKind.VoidKeyword));
        }

        private static TypeSyntax ConvertToTypeSyntax(string text)
        {
            if (text == "number")
            {
                return PredefinedType(Token(SyntaxKind.LongKeyword));
            }

            if (text == "integer")
            {
                return PredefinedType(Token(SyntaxKind.IntKeyword));
            }

            if (text == "boolean")
            {
                return PredefinedType(Token(SyntaxKind.BoolKeyword));
            }

            // otherwise we don't know how to handle it
            return IdentifierName(text);
        }

        private static ParameterListSyntax GetParameterList(List<ApiParameterMetadata> parameters)
        {
            if (parameters.Count == 0)
            {
                return ParameterList();
            }

            if (parameters.Count == 1)
            {
                var parameter = parameters.FirstOrDefault();
                var parameterType = ConvertToTypeSyntax(parameter.Type);

                return ParameterList(SingletonSeparatedList<ParameterSyntax>(
                                                Parameter(Identifier(parameter.Name))
                                                .WithType(parameterType)));
            }

            var list = new List<SyntaxNodeOrToken>();

            foreach (var parameter in parameters)
            {
                var parameterType = ConvertToTypeSyntax(parameter.Type);
                list.Add(Parameter(Identifier(parameter.Name)).WithType(parameterType));
                list.Add(Token(SyntaxKind.CommaToken));
            }

            // remove trailing comma token to ensure code compiles
            list.RemoveAt(list.Count - 1);

            return ParameterList(SeparatedList<ParameterSyntax>(list));
        }

        private static SyntaxList<AttributeListSyntax> GetAttributeList(ApiMethodMetadata method)
        {
            if (method.SourceMetadata == null)
            {
                return SingletonList<AttributeListSyntax>(AttributeList());
            }

            var generatedRouteAttribute = Attribute(IdentifierName("GeneratedRoute"))
                                                .WithArgumentList(
                                                    AttributeArgumentList(
                                                        SeparatedList<AttributeArgumentSyntax>(
                                                            new SyntaxNodeOrToken[]{
                                                            AttributeArgument(
                                                                LiteralExpression(
                                                                    SyntaxKind.StringLiteralExpression,
                                                                    Literal(method.SourceMetadata.Verb))),
                                                            Token(SyntaxKind.CommaToken),
                                                            AttributeArgument(
                                                                LiteralExpression(
                                                                    SyntaxKind.StringLiteralExpression,
                                                                    Literal(method.SourceMetadata.Path)))})));

            return SingletonList<AttributeListSyntax>(AttributeList(SingletonSeparatedList<AttributeSyntax>(generatedRouteAttribute)));
        }

        private static InterfaceDeclarationSyntax WithInterface(ApiCodeFileMetadata apiBuilder)
        {
            var members = apiBuilder.Client.Methods.Select(m =>
            {
                var parameters = GetParameterList(m.Parameters);
                var attributes = GetAttributeList(m);
                var returnType = ConvertToReturnType(m.ReturnType);

                return MethodDeclaration(returnType, Identifier(m.Name))
                            .WithParameterList(parameters)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            });

            return InterfaceDeclaration(apiBuilder.Client.InterfaceName)
                                      .WithModifiers(
                                          TokenList(
                                              Token(SyntaxKind.PublicKeyword)))
                                      .WithMembers(List<MemberDeclarationSyntax>(members));
        }

        private static ClassDeclarationSyntax WithImplementation(ApiCodeFileMetadata apiBuilder)
        {
            var members = apiBuilder.Client.Methods.Select(m =>
            {
                var parameters = GetParameterList(m.Parameters);
                var attributes = GetAttributeList(m);
                // TODO: a proper type returned from the API
                var returnType = ConvertToReturnType(m.ReturnType);

                return MethodDeclaration(returnType, Identifier(m.Name))
                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                            .WithParameterList(parameters)
                            .WithAttributeLists(attributes)
                            .WithBody(
                                Block(
                                    SingletonList<StatementSyntax>(
                                        ThrowStatement(
                                            ObjectCreationExpression(
                                                IdentifierName("NotImplementedException"))
                                            .WithArgumentList(
                                                ArgumentList())))));
            });

            return ClassDeclaration(apiBuilder.Client.ClassName)
                                      .WithModifiers(
                                          TokenList(
                                              Token(SyntaxKind.PublicKeyword)))
                                      .WithBaseList(
                                          BaseList(
                                              SingletonSeparatedList<BaseTypeSyntax>(
                                                  SimpleBaseType(
                                                      IdentifierName(apiBuilder.Client.InterfaceName)))))
                                      .WithMembers(List<MemberDeclarationSyntax>(members));
        }

        public static CompilationUnitSyntax GenerateSourceFile(ApiCodeFileMetadata stub)
        {
            return CompilationUnit()
              .WithMembers(
                  SingletonList<MemberDeclarationSyntax>(
                      NamespaceDeclaration(
                          IdentifierName("Octokit"))
                      .WithMembers(
                          List<MemberDeclarationSyntax>(
                              new MemberDeclarationSyntax[]{
                                  WithInterface(stub),
                                  WithImplementation(stub) }))))
              .NormalizeWhitespace();
        }
    }
}
