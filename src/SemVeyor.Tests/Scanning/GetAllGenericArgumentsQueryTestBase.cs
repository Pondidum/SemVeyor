using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning
{
	public abstract class GetAllGenericArgumentsQueryTestBase
	{
		private readonly IEnumerable<GenericArgumentDetails> _generics;

		public GetAllGenericArgumentsQueryTestBase()
		{
			_generics = BuildGenerics();
		}

		protected abstract IEnumerable<GenericArgumentDetails> BuildGenerics();

		[Fact]
		public void The_parameter_count_is_2() => _generics.Count().ShouldBe(2);

		[Fact]
		public void The_first_parameter_has_a_constraint() => _generics.First().Constraints.ShouldHaveSingleItem(nameof(Exception));

		[Fact]
		public void The_second_parameter_has_no_constraint() => _generics.Last().Constraints.ShouldBeEmpty();

		[Fact]
		public void The_names_are_populated() => _generics.Select(g => g.Name).ShouldBe(new[]
		{
			"TKey",
			"TValue"
		});

		[Fact]
		public void The_positions_are_populated() => _generics.Select(g => g.Position).ShouldBe(new[] { 0, 1 });
	}
}
