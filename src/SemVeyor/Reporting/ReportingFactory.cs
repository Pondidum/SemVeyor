using System;

namespace SemVeyor.Reporting
{
	public class ReportingFactory
	{
		public IReporter CreateReporter(Configuration.Config config)
		{
			if (config.ReporterTypes.Contains("simple") == false)
				throw new NotSupportedException($"You must specify 'simple' reporting at this time. Actually got {string.Join(",", config.ReporterTypes)}");

			return new SimpleReporter(
				config.ReporterOptions<SimpleReporterOptions>("simple"));
		}
	}
}