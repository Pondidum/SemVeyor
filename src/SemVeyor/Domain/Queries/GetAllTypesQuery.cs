using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.Domain.Queries
{
	public class GetAllTypesQuery
	{
		public IEnumerable<TypeDetails> Execute(Assembly assembly)
		{
			return assembly
				.GetExportedTypes()
				.Select(TypeDetails.From)
				.Where(MemberDetails.IsExternal)
				.ToArray();
		}
	}
}