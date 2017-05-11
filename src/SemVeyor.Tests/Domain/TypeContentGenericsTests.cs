using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class TypeContentGenericsTests
	{
		public TypeDetails Details { get; }

		public TypeContentGenericsTests()
		{
			Details = new GetTypeQuery().Execute(typeof(TestType<,>));
		}

		[Fact]
		public void The_name_is_populated() => Details.Name.ShouldBe("TestType`2");

		[Fact]
		public void The_generic_arguments_are_populated() => Details.GenericArguments.Count().ShouldBe(2);

		public class TestType<TKey, TValue>
		{
		}
	}
}
