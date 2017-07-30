using System;
using System.Linq;
using FileSystem;
using Oakton;
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
			var reader = new ConfigFileReader(new PhysicalFileSystem());
			var configuration = reader.Read().OverrideWith(input.AsOptions());

			var store = new StorageFactory().CreateStore(configuration);
			var reporter = new ReportingFactory().CreateReporter(configuration);
			var scanner = new ScannerFactory().CreateScanner(configuration.GlobalOptions);

			var previous = store.Read();
			var current = scanner.Execute(new AssemblyScannerArgs { Path = configuration.GlobalOptions.Paths.First() }).Result;

			var app = new App(store, reporter);

			Console.WriteLine(previous != null
				? "History loaded"
				: "No History found");

			app.Execute(previous, current);

			Console.WriteLine("Done.");
			return true;
		}
	}
}
