using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using OneOf;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Octokit.CodeGen
{
    public class RoslynGenerator
    {
        private static TypeSyntax ConvertToReturnType(OneOf<TaskOfType, TaskOfListType, UnknownReturnType> taskOfSomeType)
        {
            return taskOfSomeType.Match<TypeSyntax>(objectTask =>
            {
                var innerType = ConvertToTypeSyntax(objectTask.Type);
                return GenericName(Identifier("Task"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(innerType)));
            }, listTask =>
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
            }, unknownType =>
            {
                return PredefinedType(Token(SyntaxKind.VoidKeyword));
            }
            );
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

        private static LocalDeclarationStatementSyntax GenerateUriStatement(ApiMethodMetadata method)
        {
            bool isPlaceHolder(string str)
            {
                return str.StartsWith("{") && str.EndsWith("}");
            }

            // for compatibility with GHE we need to start these URIs without a /
            var path = method.SourceMetadata.Path.TrimStart('/');
            var parameters = method.Parameters;

            var stringInterpolationNodes = new List<InterpolatedStringContentSyntax>();

            ArgumentListSyntax constructorArgument;

            if (parameters.Count == 0)
            {
                constructorArgument = ArgumentList(
                    SingletonSeparatedList<ArgumentSyntax>(
                        Argument(
                            LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                Literal(path)))));
            }
            else
            {
                var tokensForStringInterpolation = new List<StringInterpolationToken>();

                var tokensFromPath = path.Split("/");
                foreach (var token in tokensFromPath)
                {
                    if (isPlaceHolder(token))
                    {
                        var parameterName = token.Replace("{", "").Replace("}", "");
                        var parameterToReplace = parameters.FirstOrDefault(p => p.Replaces == parameterName);
                        if (parameterToReplace == null)
                        {
                            throw new InvalidOperationException($"Unable to find parameter to replace {parameterName}");
                        }

                        tokensForStringInterpolation.Add(new StringInterpolationToken
                        {
                            Text = parameterToReplace.Name,
                            Type = StringInterpolationTokenType.Placeholder
                        });
                    }
                    else
                    {
                        tokensForStringInterpolation.Add(new StringInterpolationToken
                        {
                            Text = token.Trim('/'),
                            Type = StringInterpolationTokenType.Text
                        });
                    }
                }

                for (var i = 0; i < tokensForStringInterpolation.Count;)
                {
                    var token = tokensForStringInterpolation[i];

                    if (token.Type == StringInterpolationTokenType.Text)
                    {
                        // do not add a preceding slash for the first token
                        var prefix = i == 0 ? "" : "/";

                        var otherTokens = tokensForStringInterpolation.Skip(i)
                                                                      .TakeWhile(t => t.Type == StringInterpolationTokenType.Text)
                                                                      .Select(t => t.Text)
                                                                      .ToList();
                        var count = otherTokens.Count;
                        i += count;
                        var mergedTokens = string.Join('/', otherTokens);

                        // only add a trailiing slash if there are other tokens to pull
                        var suffix = i < tokensForStringInterpolation.Count ? "/" : "";

                        var uriSegment = $"{prefix}{mergedTokens}{suffix}";

                        var node = InterpolatedStringText()
                                                  .WithTextToken(
                                                      Token(
                                                          TriviaList(),
                                                          SyntaxKind.InterpolatedStringTextToken,
                                                          uriSegment,
                                                          uriSegment,
                                                          TriviaList()));

                        stringInterpolationNodes.Add(node);
                    }
                    else
                    {
                        var prevToken = i > 0 ? tokensForStringInterpolation[i - 1] : null;
                        var prevTokenWasPlaceholder = prevToken != null ? prevToken.Type == StringInterpolationTokenType.Placeholder : false;

                        if (prevTokenWasPlaceholder)
                        {
                            var node = InterpolatedStringText()
                                                     .WithTextToken(
                                                         Token(
                                                             TriviaList(),
                                                             SyntaxKind.InterpolatedStringTextToken,
                                                             "/",
                                                             "/",
                                                             TriviaList()));
                            stringInterpolationNodes.Add(node);
                        }

                        stringInterpolationNodes.Add(Interpolation(IdentifierName(token.Text)));
                        i += 1;
                    }
                }


                // TODO: how can we build up the "path with substitutes" here, replacing
                //       each parameter in the path with it's C# equivalent?'

                // TODO: and then how can we convert this string into it's Roslyn-based
                //       equivalent?
                constructorArgument = ArgumentList(
                  SingletonSeparatedList<ArgumentSyntax>(
                      Argument(
                          InterpolatedStringExpression(Token(SyntaxKind.InterpolatedStringStartToken))
                          .WithContents(
                              List<InterpolatedStringContentSyntax>(stringInterpolationNodes)))));

            }

            return LocalDeclarationStatement(
                VariableDeclaration(IdentifierName("var"))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(Identifier("uri"))
                            .WithInitializer(
                                EqualsValueClause(ObjectCreationExpression(IdentifierName("Uri")).WithArgumentList(constructorArgument))))));
        }

        private static ReturnStatementSyntax GenerateReturnStatement(ApiMethodMetadata method)
        {
            // TODO: change the method based on the verb

            // TODO: the argument passed in here may need to strip the Task<> or Task<IReadOnlyList>
            //       from the ReturnType

            // TODO: how would this differ for POST/PATCH/DELETE?
            // TODO: how can we handle things like preview APIs? content types?

            return ReturnStatement(
                  InvocationExpression(
                      MemberAccessExpression(
                          SyntaxKind.SimpleMemberAccessExpression,
                          IdentifierName("ApiConnection"),
                          GenericName(Identifier(method.Name))
                          .WithTypeArgumentList(
                              TypeArgumentList(
                                  SingletonSeparatedList<TypeSyntax>(
                                      IdentifierName("MarketplaceListingAccount"))))))
                  .WithArgumentList(
                      ArgumentList(
                          SingletonSeparatedList<ArgumentSyntax>(
                              Argument(IdentifierName("uri"))))));
        }

        private static BlockSyntax GetBodyForClientMethod(ApiMethodMetadata method)
        {
            if (method.SourceMetadata.Verb != "GET")
            {
                return Block(
                   SingletonList<StatementSyntax>(
                       ThrowStatement(
                           ObjectCreationExpression(
                               IdentifierName("NotImplementedException"))
                           .WithArgumentList(
                               ArgumentList()))));
            }

            // for a GET we need two things
            //  - the URL to call - where we merge the required parameters into the path
            var declareUriStatement = GenerateUriStatement(method);
            //  - the call to the underlying ApiConnection
            var returnStatement = GenerateReturnStatement(method);


            return Block(
              declareUriStatement,
              returnStatement);
        }

        private static SyntaxList<UsingDirectiveSyntax> UsingStatements()
        {
            return List<UsingDirectiveSyntax>
            (
                new UsingDirectiveSyntax[]
                {
                  UsingDirective(IdentifierName("System")),
                  UsingDirective(
                    QualifiedName(
                          QualifiedName(IdentifierName("System"),
                          IdentifierName("Collections")),
                          IdentifierName("Generic"))),
                  UsingDirective(
                    QualifiedName(
                          QualifiedName(IdentifierName("System"),
                          IdentifierName("Threading")),
                          IdentifierName("Tasks")))
                }
            );
        }

        private static ClassDeclarationSyntax WithModel(ApiResponseModelMetadata modelMetadata)
        {
            var properties = modelMetadata.Properties.Select(m =>
            {
                // TODO: a proper type returned from the API
                var returnType = ConvertToTypeSyntax(m.Type);

                return PropertyDeclaration(returnType, Identifier(m.Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(
                            AccessorList(
                                List<AccessorDeclarationSyntax>(
                                    new AccessorDeclarationSyntax[]{
                                        AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                        AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))})));
            });

            return ClassDeclaration(modelMetadata.Name)
                  .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                  .WithMembers(List<MemberDeclarationSyntax>(properties));
        }

        private static InterfaceDeclarationSyntax WithInterface(ApiClientFileMetadata apiBuilder)
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

        private static ClassDeclarationSyntax WithImplementation(ApiClientFileMetadata apiBuilder)
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
                            .WithBody(GetBodyForClientMethod(m));
            }).ToList<MemberDeclarationSyntax>();

            MemberDeclarationSyntax constructor = ConstructorDeclaration(
                        Identifier(apiBuilder.Client.ClassName))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList<ParameterSyntax>(
                                Parameter(
                                    Identifier("apiConnection"))
                                .WithType(
                                    IdentifierName("IApiConnection")))))
                    .WithInitializer(
                        ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            ArgumentList(
                                SingletonSeparatedList<ArgumentSyntax>(
                                    Argument(
                                        IdentifierName("apiConnection"))))))
                    .WithBody(
                        Block()); // TODO: create and assign any child clients that correspond to properties

            members.Insert(0, constructor);

            return ClassDeclaration(apiBuilder.Client.ClassName)
                                      .WithModifiers(
                                          TokenList(
                                              Token(SyntaxKind.PublicKeyword)))
                                      .WithBaseList(
                                        BaseList(
                                            SeparatedList<BaseTypeSyntax>(
                                                new SyntaxNodeOrToken[]{
                                                    SimpleBaseType(IdentifierName("ApiClient")),
                                                    Token(SyntaxKind.CommaToken),
                                                    SimpleBaseType(
                                                        IdentifierName(apiBuilder.Client.InterfaceName))})))
                                      .WithMembers(List<MemberDeclarationSyntax>(members));
        }

        public static string GetSourceFileText(ApiClientFileMetadata metadata)
        {
            var sourceFile = GenerateSourceNode(metadata);

            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine, true);
            cw.Options.WithChangedOption(CSharpFormattingOptions.WrappingPreserveSingleLine, true);
            SyntaxNode formattedNode = Formatter.Format(sourceFile, cw);

            return formattedNode.ToFullString();
        }

        public static CompilationUnitSyntax GenerateSourceNode(ApiClientFileMetadata metadata)
        {
            var members = new List<MemberDeclarationSyntax>();

            if (metadata.RequestModels.Any())
            {
                members.AddRange(metadata.RequestModels.Select(WithModel));
            }

            if (metadata.ResponseModels.Any())
            {
                members.AddRange(metadata.ResponseModels.Select(WithModel));
            }

            if (metadata?.Client?.InterfaceName != null)
            {
                members.Add(WithInterface(metadata));
            }

            if (metadata?.Client?.ClassName != null)
            {
                members.Add(WithImplementation(metadata));
            }

            return CompilationUnit()
                      .WithUsings(List<UsingDirectiveSyntax>(UsingStatements()))
                      .WithMembers(
                          SingletonList<MemberDeclarationSyntax>(
                              NamespaceDeclaration(
                                  IdentifierName("Octokit"))
                              .WithMembers(
                                  List<MemberDeclarationSyntax>(members))));
        }
    }

    enum StringInterpolationTokenType
    {
        Placeholder,
        Text
    }

    class StringInterpolationToken
    {
        public string Text { get; set; }
        public StringInterpolationTokenType Type { get; set; }
    }
}
