using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Domain.Queries
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
