using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Classification;
using SemVeyor.CommandLine;
using SemVeyor.Domain;
using SemVeyor.Reporting;
using SemVeyor.Storage;

namespace SemVeyor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var cli = new Cli().Parse(args);
			var options = Options.From(cli);

			var store = new StorageFactory().CreateStore(cli, options);

			var previous = store.Read();
			var current = AssemblyDetails.From(Assembly.LoadFile(options.Paths.First()));

			Console.WriteLine(previous != null
				? "History loaded"
				: "No History found");

			if (previous != null)
			{
				var changes = previous.UpdatedTo(current);
				var classifier = new EventClassification();

				var processed = classifier.ClassifyAll(changes).ToArray();
				var reporter = new SimpleReporter();

				reporter.Write(new ReportArgs
				{
					PreviousAssembly = previous,
					CurrentAssembly = current,
					Changes = processed
				});
			}

			store.Write(current);

			Console.WriteLine("Done.");
		}
	}
}
