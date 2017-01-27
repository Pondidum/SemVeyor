using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CommandLineResultFailureTests
	{
		private readonly CommandLineResult<Options> _result;

		public CommandLineResultFailureTests()
		{
			_result = new CommandLineResult<Options>(new[]
			{
				"first",
				"second"
			});
		}

		[Fact]
		public void Success_callback_does_not_get_called()
		{
			var called = false;
			_result.OnSuccess(x => called = true);

			called.ShouldBe(false);
		}

		[Fact]
		public void Failure_callback_gets_called()
		{
			var called = false;
			_result.OnFailure(x => called = true);

			called.ShouldBe(true);
		}

		[Fact]
		public void Failure_callback_has_all_errors() => _result.OnFailure(errors => errors.ShouldBe(new[] { "first", "second" }));
	}
}