using System;
using System.Linq;

namespace SemVeyor.Reporting
{
	public class SimpleReporter : IReporter
	{
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
			Console.WriteLine("SemVer change requried: " + e.Changes.Max(x => x.Classification));
		}
	}
}
