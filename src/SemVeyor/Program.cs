using System;
using System.Linq;
using FileSystem;
using SemVeyor.Configuration;
using SemVeyor.Reporting;
using SemVeyor.Scanning;
using SemVeyor.Storage;

namespace SemVeyor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var reader = new ConfigFileReader(new PhysicalFileSystem());
			var configuration = reader.Read();

			var store = new StorageFactory().CreateStore(configuration);
			var reporter = new ReportingFactory().CreateReporter(configuration);
			var scanner = new ScannerFactory().CreateScanner(configuration.GlobalOptions);

			var previous = store.Read();
			var current = scanner.Execute(new AssemblyScannerArgs { Path = configuration.GlobalOptions.Paths.First() }).Result;
			//var current = new GetAssemblyQuery().Execute(Assembly.LoadFile(options.Paths.First()));

			var app = new App(store, reporter);

			Console.WriteLine(previous != null
				? "History loaded"
				: "No History found");

			app.Execute(previous, current);

			Console.WriteLine("Done.");
		}
	}
}
