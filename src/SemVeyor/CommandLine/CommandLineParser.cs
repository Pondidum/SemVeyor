using System.Linq;

namespace SemVeyor.CommandLine
{
	public class CommandLineParser
	{
		public CommandLineResult<Options> Parse(string[] args)
		{
			return new CommandLineResult<Options>(Enumerable.Empty<string>());
		}
	}
}