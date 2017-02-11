using System;
using SemVeyor.Infrastructure;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Infrastructure
{
	public class LambdaComparerTests
	{
		[Theory]
		[InlineData(0, 0, true)]
		[InlineData(0, 1, false)]
		[InlineData(1, 0, false)]
		public void When_comparing_integers(int first, int last, bool expected)
		{
			var comparer = new LambdaComparer<TestDto>(x => x.Position);

			comparer
				.Equals(new TestDto { Position =  first}, new TestDto { Position = last })
				.ShouldBe(expected);
		}

		[Theory]
		[InlineData("", "", true)]
		[InlineData("one", "", false)]
		[InlineData("", "one", false)]
		[InlineData("two", "two", true)]
		[InlineData("ONE", "one", false)]
		public void When_comparing_strings(string first, string last, bool expected)
		{
			var comparer = new LambdaComparer<TestDto>(x => x.Name);

			comparer
				.Equals(new TestDto { Name =  first}, new TestDto { Name = last })
				.ShouldBe(expected);
		}

		[Theory]
		[InlineData(typeof(int), typeof(int), true)]
		[InlineData(typeof(int), typeof(string), false)]
		[InlineData(typeof(string), typeof(int), false)]
		public void When_comparing_objects(Type first, Type last, bool expected)
		{
			var comparer = new LambdaComparer<TestDto>(x => x.Type);

			comparer
				.Equals(new TestDto { Type =  first}, new TestDto { Type = last })
				.ShouldBe(expected);
		}

		private class TestDto
		{
			public int Position { get; set; }
			public string Name { get; set; }
			public Type Type { get; set; }
		}
	}
}
