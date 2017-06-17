using System.Collections.Generic;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllMethodsQueryTests : GetAllMethodsQueryTestBase
	{
		protected override IEnumerable<MethodDetails> BuildMethods()
		{
			return new GetAllMethodsQuery().Execute(typeof(TestType));
		}
	}
}