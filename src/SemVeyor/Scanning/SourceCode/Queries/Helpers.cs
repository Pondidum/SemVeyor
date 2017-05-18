using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public static class Helpers
	{
		public static string GetFullMetadataName(this ISymbol symbol)
		{
			var sb = new StringBuilder(symbol.MetadataName);

			var last = symbol;
			var current = symbol.ContainingSymbol;

			while (!IsRootNamespace(current))
			{
				if (current is ITypeSymbol && last is ITypeSymbol)
					sb.Insert(0, '+');
				else
					sb.Insert(0, '.');

				sb.Insert(0, current.MetadataName);
				current = current.ContainingSymbol;
			}

			return sb.ToString();
		}

		private static bool IsRootNamespace(ISymbol s)
		{
			var symbol = s as INamespaceSymbol;

			return symbol != null && symbol.IsGlobalNamespace;
		}

		public static Visibility VisibilityFrom(SyntaxTokenList modifiers)
		{
			if (modifiers.Any(SyntaxKind.PublicKeyword))
				return Visibility.Public;

			if (modifiers.Any(SyntaxKind.ProtectedKeyword))
				return Visibility.Protected;

			if (modifiers.Any(SyntaxKind.InternalKeyword))
				return Visibility.Internal;

			return Visibility.Private;
		}

		public static Visibility VisibilityFrom(Accessibility accessibility)
		{
			if (accessibility == Accessibility.Public)
				return Visibility.Public;

			if (accessibility == Accessibility.Protected)
				return Visibility.Protected;

			if (accessibility == Accessibility.Internal)
				return Visibility.Internal;

			return Visibility.Private;
		}
	}
}