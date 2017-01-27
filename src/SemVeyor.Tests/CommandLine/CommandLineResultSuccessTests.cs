using System;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CommandLineResultSuccessTests
	{
		private readonly CommandLineResult<Options> _result;
		private readonly string _path;

		public CommandLineResultSuccessTests()
		{
			_path = Guid.NewGuid().ToString();
			_result = new CommandLineResult<Options>(new Options
			{
				AssemblyPath = _path
			});
		}

		[Fact]
		public void Failure_callback_does_not_get_called()
		{
			var called = false;
			_result.OnFailure(x => called = true);

			called.ShouldBe(false);
		}

		[Fact]
		public void Success_callback_gets_called()
		{
			var called = false;
			_result.OnSuccess(x => called = true);

			called.ShouldBe(true);
		}

		[Fact]
		public void Success_callback_has_all_errors() => _result.OnSuccess(opts => opts.AssemblyPath.ShouldBe(_path));
	}
}