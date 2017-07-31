using System;
using System.Linq;
using FileSystem;
using Oakton;
using SemVeyor.Classification;
using SemVeyor.Configuration;
using SemVeyor.Reporting;
using SemVeyor.Scanning;
using SemVeyor.Storage;

namespace SemVeyor.Commands.Scan
{
	public class ScanCommand : OaktonCommand<ScanInput>
	{
		public ScanCommand()
		{
			Usage("Scans an assembly for changes which affect SemVer.")
				.Arguments(x => x.ConfigFlag, x => x.Path);
		}

		public override bool Execute(ScanInput input)
		{
			var configuration = new ConfigurationBuilder(new PhysicalFileSystem()).Build(input);

			var storage = new StorageFactory().CreateStore(configuration);
			var reporter = new ReportingFactory().CreateReporter(configuration);
			var scanner = new ScannerFactory().CreateScanner(configuration.GlobalOptions);

			var previous = storage.Read();
			var current = scanner.Execute(new AssemblyScannerArgs { Path = configuration.GlobalOptions.Paths.First() }).Result;

			var classification = new ClassificationReport();

			Console.WriteLine(previous != null
				? "History loaded"
				: "No History found");

			var report = classification.Execute(previous, current);

			reporter.Write(report);
			storage.Write(current);

			Console.WriteLine("Done.");
			return true;
		}
	}
}
