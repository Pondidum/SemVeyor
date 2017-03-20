using System.Collections.Generic;
using SemVeyor.Classification;

namespace SemVeyor.Reporting
{
	public class ReportArgs
	{
		public IEnumerable<ChangeClassification> Changes { get; set; }
	}
}
