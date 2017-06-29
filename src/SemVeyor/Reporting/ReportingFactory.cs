using System;
using SemVeyor.CommandLine;
using SemVeyor.Config;

namespace SemVeyor.Reporting
{
	public class ReportingFactory
	{
		public IReporter CreateReporter(CliParameters cli, Configuration config)
		{
			if (config.StorageTypes.Contains("simple") == false)
				throw new NotSupportedException($"You must specify 'simple' reporting at this time. Actually got {string.Join(",", config.StorageTypes)}");

			return new SimpleReporter(
				config.ReporterOptions<SimpleReporterOptions>("simple"));
		}
	}
}