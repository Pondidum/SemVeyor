using System.Collections.Generic;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllGenericArgumentsQueryTests: GetAllGenericArgumentsQueryTestBase
	{
		protected override IEnumerable<GenericArgumentDetails> BuildGenerics()
		{
			return new GetAllGenericArgumentsQuery().Execute(typeof(GenericType<,>));
		}
	}
}
