using NSubstitute;
using SemVeyor.Classification;
using SemVeyor.Reporting;
using SemVeyor.Storage;
using SemVeyor.Tests.Builder;
using Xunit;

namespace SemVeyor.Tests.Acceptance.Application
{
	public class Scenarios
	{
		private readonly IStorage _storage;
		private readonly IReporter _reporter;
		private readonly App _app;

		public Scenarios()
		{
			_storage = Substitute.For<IStorage>();
			_reporter = Substitute.For<IReporter>();
			_app = new App(_storage, _reporter);
		}

		[Fact]
		public void An_assembly_with_no_changes_causes_no_semver_change()
		{
			var previous = Build.Assembly("Test")
				.WithTypes(Build.Type("Test.Namespace", "First"));

			var current = Build.Assembly("Test")
				.WithTypes(Build.Type("Test.Namespace", "First"));

			_app.Execute(previous, current);

			_reporter.Received().Write(Arg.Is<ReportArgs>(ra => ra.SemVerChange == SemVer.None));
		}
	}
}
