using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetTypeQueryTests : GetTypeQueryTestBase
	{
		protected override TypeDetails BuildDetails()
		{
			return new GetTypeQuery().Execute(typeof(GenericType<,>));
		}
	}
}
