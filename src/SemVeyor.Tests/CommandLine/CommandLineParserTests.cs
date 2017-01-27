using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;

namespace SemVeyor.Tests.CommandLine
{
	public class CommandLineParserTests
	{
		private Options _options;
		private List<string> _errors;

		private CommandLineResult<Options> Parse(params string[] args)
		{
			var parsed = new CommandLineParser().Parse(args);

			parsed.OnSuccess(result => _options = result);
			parsed.OnFailure(errors => _errors = errors.ToList());

			return parsed;
		}

//		[Fact]
//		public void When_there_are_no_arguments()
//		{
//			var parsed = Parse();
//
//			parsed.ShouldSatisfyAllConditions(
//				() => parsed.WasSuccessful.ShouldBe(false),
//				() => parsed.Options.ShouldBeNull(),
//				() => _options.ShouldBeNull(),
//				() => _errors.ShouldHaveSingleItem()
//			);
//		}
//
//		[Fact]
//		public void When_only_an_assembly_is_specified()
//		{
//			var parsed = Parse("some/assembly.dll");
//
//			parsed.ShouldSatisfyAllConditions(
//				() => parsed.WasSuccessful.ShouldBe(true),
//				() => _options.ShouldBe(parsed.Options),
//				() => _options.AssemblyPath.ShouldBe("some/assembly.dll"),
//				() => _errors.ShouldBeNull()
//			);
//		}
	}
}
