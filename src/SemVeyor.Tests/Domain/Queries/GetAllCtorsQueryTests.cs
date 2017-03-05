﻿using SemVeyor.Domain;
using SemVeyor.Domain.Queries;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SemVeyor.Tests.Domain.Queries
{
	public class GetAllCtorsQueryTests
	{
		private readonly IEnumerable<CtorDetails> _constructors;

		public GetAllCtorsQueryTests()
		{
			_constructors = new GetAllCtorsQuery().Execute(typeof(TestType));
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

	}
}