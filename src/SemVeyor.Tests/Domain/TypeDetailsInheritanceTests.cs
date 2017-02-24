using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class TypeDetailsInheritanceTests : TypeDetailsTestBase<TypeDetailsInheritanceTests.TestType>
	{
		[Fact]
		public void The_base_type_is_populated() => Details.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => Details.Interfaces.ShouldBe(new[]
		{
			nameof(ITestInterfaceOne),
			nameof(ITestInterfaceTwo)
		});

		public class TestType : ParentType, ITestInterfaceOne, ITestInterfaceTwo
		{

		}

		public class ParentType {}
		public interface ITestInterfaceOne {}
		public interface ITestInterfaceTwo {}
	}
}
