using SemVeyor.Classification;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Acceptance.Application
{
	public class Scenarios
	{
		private readonly ClassificationReport _classificationReport;

		public Scenarios()
		{
			_classificationReport = new ClassificationReport(new EventClassification(EventClassification.DefaultClassificationMap));
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

			var report = _classificationReport.Execute(previous, current);

			report.SemVerChange.ShouldBe(SemVer.None);
		}
	}
}
