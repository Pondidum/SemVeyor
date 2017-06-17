using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllPropertiesQueryTests : GetAllPropertiesQueryTestBase
	{
		protected override IEnumerable<PropertyDetails> BuildProperties()
		{
			return new GetAllPropertiesQuery().Execute(typeof(TestType));
		}
	}
}
