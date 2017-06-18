using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllMethodsQuery
	{
		private static readonly HashSet<string> Ignore = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"MemberwiseClone"
		};

		public IEnumerable<MethodDetails> Execute(INamedTypeSymbol type)
		{
			return AllMethods(type)
				.Where(t => t.MethodKind == MethodKind.Ordinary)
				.Where(t => t.IsStatic == false)
				.Where(t => Ignore.Contains(t.Name) == false)
				.Select(ms => new MethodDetails
				{
					Name = ms.Name,
					Type = new TypeName(ms.ReturnType.GetFullMetadataName()),
					Visibility = Helpers.VisibilityFrom(ms.DeclaredAccessibility),
					Parameters = ms.Parameters.Select(p => new ParameterDetails()),
					GenericArguments = ms.TypeParameters.Select(tp => new GenericArgumentDetails())
				})
				.Where(MemberDetails.IsExternal);
		}

		private static IEnumerable<IMethodSymbol> AllMethods(INamedTypeSymbol type)
		{
			var current = type;
			var members = new List<IMethodSymbol>();

			while (current != null)
			{
				members.AddRange(current.GetMembers().OfType<IMethodSymbol>());
				current = current.BaseType;
			}

			return members;
		}
	}
}