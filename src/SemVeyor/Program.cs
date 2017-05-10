using System;
using System.Linq;
using System.Reflection;
using SemVeyor.CommandLine;
using SemVeyor.Domain;
using SemVeyor.Domain.Queries;
using SemVeyor.Reporting;
using SemVeyor.Scanning;
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
			var reporter = new ReportingFactory().CreateReporter(cli, options);
			var scanner = new ScannerFactory().CreateScanner(cli, options);

			var previous = store.Read();
			var current = scanner.Execute(new AssemblyScannerArgs { Path = options.Paths.First() }).Result;
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
