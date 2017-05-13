using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetTypeQuery
	{
		public GetTypeQuery()
		{
		}

		public TypeDetails Execute(ClassDeclarationSyntax classDeclaration)
		{
			return new TypeDetails
			{
				Name = classDeclaration.Identifier.ValueText
			};
		}
	}
}
