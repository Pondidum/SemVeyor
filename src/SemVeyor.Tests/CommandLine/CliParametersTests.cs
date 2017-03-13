using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliParametersTests
	{
		[Fact]
		public void When_a_prefix_which_doesnt_exist_is_used()
		{
			var parameters = new CliParameters();
			var set = parameters.ForPrefix("wat");

			set.ShouldSatisfyAllConditions(
				() => set.Arguments.ShouldBeEmpty(),
				() => set.Flags.ShouldBeEmpty(),
				() => set.Paths.ShouldBeEmpty()
			);
		}
	}
}
