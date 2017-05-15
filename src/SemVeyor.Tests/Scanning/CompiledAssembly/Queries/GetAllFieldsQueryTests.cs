using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.Domain;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllFieldsQueryTests : GetAllFieldsQueryTestBase
	{
		protected override IEnumerable<FieldDetails> BuildFields()
		{
			return new GetAllFieldsQuery().Execute(typeof(TestType));
		}
	}
}