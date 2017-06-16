using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Tests.Domain;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning
{
	public abstract class GetAllCtorsQueryTestBase
	{
		private readonly IEnumerable<CtorDetails> _constructors;

		protected abstract IEnumerable<CtorDetails> BuildDetails();

		public GetAllCtorsQueryTestBase()
		{
			_constructors = BuildDetails();
		}

		[Fact]
		public void There_are_2_constructors() => _constructors.Count().ShouldBe(2);

		[Fact]
		public void The_public_ctor_is_listed() => _constructors.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_ctor_is_listed() => _constructors.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_ctor_is_not_listed() => _constructors.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_ctor_is_not_listed() => _constructors.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_ctor_name_is_populated() => _constructors.ByVisibility(Visibility.Protected).Name.ShouldBe(".ctor");

		[Fact]
		public void The_ctor_arguments_are_populated() => _constructors.ByVisibility(Visibility.Protected).Parameters.Count().ShouldBe(1);

		[Fact]
		public void The_ctor_argument_names_are_populated() => GetParameter().Name.ShouldBe("arg");

		[Fact]
		public void The_ctor_argument_types_are_populated() => GetParameter().Type.ShouldBe(new TypeName("System.String"));

		[Fact]
		public void The_ctor_argument_positions_are_populated() => GetParameter().Position.ShouldBe(0);

		private ParameterDetails GetParameter() => _constructors.ByVisibility(Visibility.Protected).Parameters.Single();
	}
}