using System.Linq;

namespace SemVeyor.CommandLine
{
	public class CommandLineParser
	{
		public CommandLineResult<Options> Parse(string[] args)
		{
			if (args.Length == 0)
				return new CommandLineResult<Options>(new[]
				{
					"No assembly specified to survey"
				});

			var options = new Options
			{
				AssemblyPath = args.Last()
			};

			return new CommandLineResult<Options>(options);
		}
	}
}
