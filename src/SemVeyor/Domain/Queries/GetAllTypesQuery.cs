using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.Domain.Queries
{
	public class GetAllTypesQuery
	{
		private readonly GetTypeQuery _getTypeQuery;

		public GetAllTypesQuery(GetTypeQuery getTypeQuery)
		{
			_getTypeQuery = getTypeQuery;
		}

		public IEnumerable<TypeDetails> Execute(Assembly assembly)
		{
			return assembly
				.GetExportedTypes()
				.Select(_getTypeQuery.Execute)
				.Where(MemberDetails.IsExternal)
				.ToArray();
		}
	}
}
