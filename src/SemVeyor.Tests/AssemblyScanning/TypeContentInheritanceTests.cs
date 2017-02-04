using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentInheritanceTests : TypeContentTestBase<TypeContentInheritanceTests.TestType>
	{
		[Fact]
		public void The_base_type_is_populated() => Content.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => Content.Interfaces.ShouldBe(new[]
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
