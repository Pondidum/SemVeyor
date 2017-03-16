using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Classification;
using SemVeyor.CommandLine;
using SemVeyor.Domain;
using SemVeyor.Storage;

namespace SemVeyor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var cli = new Cli().Parse(args);
			var options = Options.From(cli);

			var store = new StorageFactory().CreateStore(options);

			var previous = store.Read();
			var current = AssemblyDetails.From(Assembly.LoadFile(options.Paths.First()));

			Console.WriteLine(previous != null
				? "History loaded"
				: "No History found");

			if (previous != null)
			{
				var changes = previous.UpdatedTo(current);
				var classifier = new EventClassification();

				var processed = changes.Select(change => new
				{
					Change = change,
					Classification = classifier.ClassifyEvent(change)
				}).ToArray();

				if (processed.Any())
				{
					Console.WriteLine("Changes since previous run:");

					foreach (var change in processed)
						Console.WriteLine(change.Change + " : " + change.Classification);

					Console.WriteLine();
					Console.WriteLine("SemVer change requried: " + processed.Max(x => x.Classification));
				}
			}

			store.Write(current);

			Console.WriteLine("Done.");
		}
	}
}
