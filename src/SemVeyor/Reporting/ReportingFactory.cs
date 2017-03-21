using System;
using SemVeyor.CommandLine;

namespace SemVeyor.Reporting
{
	public class ReportingFactory
	{
		public IReporter CreateReporter(CliParameters cli, Options options)
		{
			if (options.Reporter != "simple")
				throw new NotSupportedException(options.Reporter);

			return new SimpleReporter(cli.ForPrefix(options.Reporter).Build<SimpleReporterOptions>());
		}
	}
}