using System;
using System.Linq;
using System.Reflection;
using SemVeyor.CommandLine;
using SemVeyor.Config;
using SemVeyor.Domain;
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
			var configuration = new CliConfigurationBuilder().Build(cli);

			var store = new StorageFactory().CreateStore(cli, configuration.GlobalOptions);
			var reporter = new ReportingFactory().CreateReporter(cli, configuration.GlobalOptions);
			var scanner = new ScannerFactory().CreateScanner(cli, configuration.GlobalOptions);

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
