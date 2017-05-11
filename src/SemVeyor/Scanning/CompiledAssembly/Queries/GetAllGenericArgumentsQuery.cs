using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.CompiledAssembly.Queries
{
	public class GetAllGenericArgumentsQuery
	{
		public IEnumerable<GenericArgumentDetails> Execute(Type type)
		{
			return type
				.GetGenericArguments()
				.Select(GenericArgumentDetails.From)
				.ToArray();
		}
	}
}
