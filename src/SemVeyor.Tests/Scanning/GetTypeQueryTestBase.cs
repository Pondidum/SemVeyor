using System.Linq;
using Shouldly;
using Xunit;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning
{
	public abstract class GetTypeQueryTestBase
	{
		private readonly TypeDetails _type;

		protected abstract TypeDetails BuildDetails();

		public GetTypeQueryTestBase()
		{
			_type = BuildDetails();
		}

		[Fact]
		public void The_name_is_populated() => _type.Name.ShouldBe(typeof(GenericType<,>).Name);

		[Fact]
		public void The_namespace_is_populated() => _type.Namespace.ShouldBe(typeof(GenericType<,>).Namespace);

		[Fact]
		public void The_base_type_is_populated() => _type.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => _type.Interfaces.ShouldBe(new[]
		{
			nameof(ITestInterfaceOne),
			nameof(ITestInterfaceTwo)
		});

		[Fact]
		public void The_generic_arguments_are_populated() => _type.GenericArguments.Count().ShouldBe(2);

		[Fact]
		public void The_generic_type_constraint_is_populated() => _type.GenericArguments.First().Constraints.ShouldHaveSingleItem("Exception");
	}
}