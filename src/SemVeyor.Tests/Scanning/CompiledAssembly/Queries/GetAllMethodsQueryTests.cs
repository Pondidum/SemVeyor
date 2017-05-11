using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;
using SemVeyor.Tests.Domain;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning.CompiledAssembly.Queries
{
	public class GetAllMethodsQueryTests
	{
		private readonly IEnumerable<MethodDetails> _methods;

		public GetAllMethodsQueryTests()
		{
			_methods = new GetAllMethodsQuery().Execute(typeof(TestType));
		}
		
		[Fact]
		public void There_are_3_methods() => _methods.Count().ShouldBe(MethodsOnObject() + 3);

		[Fact]
		public void The_public_method_is_listed() => _methods.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_method_is_listed() => _methods.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_method_is_not_listed() => _methods.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_method_is_not_listed() => _methods.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_method_name_is_populated() => PublicMethod.Name.ShouldBe(nameof(TestType.Method));

		[Fact]
		public void The_return_type_is_populated() => PublicMethod.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_method_arguments_are_populated() => PublicMethod.Parameters.Count().ShouldBe(3);

		[Fact]
		public void The_method_generic_arguments_are_populated() => GenericMethod.GenericArguments.Count().ShouldBe(1);

		private MethodDetails PublicMethod => _methods.ByVisibility(Visibility.Public);
		private MethodDetails GenericMethod => _methods.Single(m => m.Name == TestType.GenericMethodName);

		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();

	}
}