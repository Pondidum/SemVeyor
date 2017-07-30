using NSubstitute;
using SemVeyor.Classification;
using SemVeyor.Reporting;
using SemVeyor.Tests.Builder;
using Xunit;

namespace SemVeyor.Tests.Acceptance.Application
{
	public class Scenarios
	{
		private readonly IReporter _reporter;
		private readonly ClassificationReport _classificationReport;

		public Scenarios()
		{
			_reporter = Substitute.For<IReporter>();
			_classificationReport = new ClassificationReport(_reporter);
		}

		[Fact]
		public void An_assembly_with_no_changes_causes_no_semver_change()
		{
			var previous = Build.Assembly("Test")
				.WithTypes(
					Build.Type("Test.Namespace", "First")
						.WithMethods(Build.Method<int>("One")),
					Build.Type("Test", "Second")
						.WithMethods(Build.Method("Go"))
						.WithProperties(Build.Property<int>("Rate"))
				);

			var current = Build.Assembly("Test")
				.WithTypes(
					Build.Type("Test.Namespace", "First")
						.WithMethods(Build.Method<int>("One")),
					Build.Type("Test", "Second")
						.WithMethods(Build.Method("Go"))
						.WithProperties(Build.Property<int>("Rate"))
				);

			_classificationReport.Execute(previous, current);

			_reporter.Received().Write(Arg.Is<ReportArgs>(ra => ra.SemVerChange == SemVer.None));
		}
	}
}
