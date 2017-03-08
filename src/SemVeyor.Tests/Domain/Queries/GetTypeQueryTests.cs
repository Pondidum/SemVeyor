using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Domain.Queries;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class GetTypeQueryTests
	{
		protected Type InputType { get; }
		protected TypeDetails Details { get; }

		public GetTypeQueryTests()
		{
			InputType = typeof(TestType);
			Details = new GetTypeQuery().Execute(typeof(TestType));
		}

		[Fact]
		public void The_name_is_populated() => Details.Name.ShouldBe(InputType.Name);

		[Fact]
		public void The_namespace_is_populated() => Details.Namespace.ShouldBe(InputType.Namespace);

		[Fact]
		public void There_are_2_constructors() => Details.Constructors.Count().ShouldBe(2);

		[Fact]
		public void There_are_2_fields() => Details.Fields.Count().ShouldBe(2);

		[Fact]
		public void There_are_7_properties() => Details.Properties.Count().ShouldBe(7);

		[Fact]
		public void There_are_3_methods() => Details.Methods.Count().ShouldBe(MethodsOnObject() + 3);

		[Fact]
		public void The_base_type_is_populated() => Details.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => Details.Interfaces.ShouldBe(new[]
		{
			nameof(ITestInterfaceOne),
			nameof(ITestInterfaceTwo)
		});



		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();
	}
}
