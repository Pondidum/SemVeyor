using System.Collections.Generic;
using SemVeyor.Infrastructure;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Infrastructure
{
	public class ExtensionsTests
	{
		[Theory]
		[InlineData("good key", 123)]
		[InlineData("bad key", 456)]
		public void GetOrDefault_returns_correct_value(string key, int expected)
		{
			var dict = new Dictionary<string, int> { { "good key", 123 } };

			dict.GetOrDefault(key, 456).ShouldBe(expected);
		}
	}
}
