using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentGenericsTests
	{
		public TypeDetails Details { get; }

		public TypeContentGenericsTests()
		{
			Details = TypeDetails.From(typeof(TestType<,>));
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
