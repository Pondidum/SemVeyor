using System;
using System.Linq;

namespace SemVeyor.Reporting
{
	public class SimpleReporter : IReporter
	{
		private readonly SimpleReporterOptions _options;

		public SimpleReporter(SimpleReporterOptions options)
		{
			_options = options;
		}

		public string Name => nameof(SimpleReporter);

		public void Write(ReportArgs e)
		{
			if (e.Changes.Any() == false)
			{
				Console.WriteLine("No changes since previous run.");
				return;
			}

			Console.WriteLine("Changes since previous run:");

			foreach (var change in e.Changes)
				Console.WriteLine(change.Change + " : " + change.Classification);

			Console.WriteLine();
			Console.WriteLine("SemVer change requried: " + e.SemVerChange);
		}
	}
}
