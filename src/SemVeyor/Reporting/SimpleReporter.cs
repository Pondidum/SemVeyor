using System;
using System.Linq;
using SemVeyor.Classification;

namespace SemVeyor.Reporting
{
	public class SimpleReporter
	{
		public void Write(ChangeClassification[] changes)
		{
			if (changes.Any() == false)
			{
				Console.WriteLine("No changes since previous run.");
				return;
			}

			Console.WriteLine("Changes since previous run:");

			foreach (var change in changes)
				Console.WriteLine(change.Change + " : " + change.Classification);

			Console.WriteLine();
			Console.WriteLine("SemVer change requried: " + changes.Max(x => x.Classification));
		}
	}
}
