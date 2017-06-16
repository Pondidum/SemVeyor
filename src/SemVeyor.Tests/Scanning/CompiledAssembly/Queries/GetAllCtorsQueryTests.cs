using System.Collections.Generic;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllCtorsQueryTests : GetAllCtorsQueryTestBase
	{
		protected override IEnumerable<CtorDetails> BuildDetails()
		{
			return new GetAllCtorsQuery().Execute(typeof(TestType));
		}
	}
}
