using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Domain;
using SemVeyor.Scanning.SourceCode.Queries;
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
		public void The_name_is_populated() => _type.Name.ShouldBe(nameof(TestType));

		[Fact]
		public void The_namespace_is_populated() => _type.Namespace.ShouldBe(typeof(TestType).Namespace);

		[Fact]
		public void There_are_2_constructors() => _type.Constructors.Count().ShouldBe(2);

		[Fact]
		public void There_are_2_fields() => _type.Fields.Count().ShouldBe(2);

		[Fact]
		public void There_are_7_properties() => _type.Properties.Count().ShouldBe(7);

		[Fact]
		public void There_are_3_methods() => _type.Methods.Count().ShouldBe(MethodsOnObject() + 3);

		[Fact]
		public void The_base_type_is_populated() => _type.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => _type.Interfaces.ShouldBe(new[]
		{
			nameof(ITestInterfaceOne),
			nameof(ITestInterfaceTwo)
		});

		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();
	}
}