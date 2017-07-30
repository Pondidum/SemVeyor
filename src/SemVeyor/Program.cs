using Oakton;
using SemVeyor.Commands.Scan;

namespace SemVeyor
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			var executor = CommandExecutor.For(_ =>
			{
				_.RegisterCommand<ScanCommand>();
				_.DefaultCommand = typeof(ScanCommand);
			});

			return executor.Execute(args);
		}
	}
}
