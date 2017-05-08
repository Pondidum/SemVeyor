using System.Linq;
using System.Reflection;

namespace SemVeyor.Domain.Queries
{
	public class GetAssemblyQuery
	{
		private readonly GetAllTypesQuery _getAllTypesQuery;

		public GetAssemblyQuery()
		{
			_getAllTypesQuery = new GetAllTypesQuery(new GetTypeQuery());
		}

		public AssemblyDetails Execute(Assembly assembly)
		{
			return new AssemblyDetails
			{
				Name = assembly.FullName,
				Types = _getAllTypesQuery.Execute(assembly).ToArray()
			};
		}
	}
}
