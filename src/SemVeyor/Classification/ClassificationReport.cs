using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Reporting;

namespace SemVeyor.Classification
{
	public class ClassificationReport
	{
		public ReportArgs Execute(AssemblyDetails previous, AssemblyDetails current)
		{
			if (previous == null)
				return null;

			var changes = previous.UpdatedTo(current);
			var classifier = new EventClassification();

			var processed = classifier.ClassifyAll(changes).ToArray();
			var semVerChange = processed
				.DefaultIfEmpty(new ChangeClassification { Classification = SemVer.None })
				.Max(c => c.Classification);

			return new ReportArgs
			{
				PreviousAssembly = previous,
				CurrentAssembly = current,
				Changes = processed,
				SemVerChange = semVerChange
			};
		}
	}
}
