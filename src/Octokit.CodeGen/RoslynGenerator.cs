using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Octokit.CodeGen
{
    public class RoslynGenerator
    {
        public static CompilationUnitSyntax GenerateSourceFile(ApiBuilderResult stub)
        {
            return CompilationUnit()
              .WithMembers(
                  SingletonList<MemberDeclarationSyntax>(
                      NamespaceDeclaration(
                          IdentifierName("Octokit"))
                      .WithMembers(
                          List<MemberDeclarationSyntax>(
                              new MemberDeclarationSyntax[]{
                                  InterfaceDeclaration(stub.InterfaceName)
                                  .WithModifiers(
                                      TokenList(
                                          Token(SyntaxKind.PublicKeyword))),
                                  ClassDeclaration(stub.ClassName)
                                  .WithModifiers(
                                      TokenList(
                                          Token(SyntaxKind.PublicKeyword)))
                                  .WithBaseList(
                                      BaseList(
                                          SingletonSeparatedList<BaseTypeSyntax>(
                                              SimpleBaseType(
                                                  IdentifierName(stub.InterfaceName)))))}))))
              .NormalizeWhitespace();
        }
    }
}
