using System.Collections.Generic;
using SemVeyor.Classification;
using SemVeyor.Domain;

namespace SemVeyor.Reporting
{
	public class ReportArgs
	{
		public AssemblyDetails PreviousAssembly { get; set; }
		public AssemblyDetails CurrentAssembly { get; set; }

		public IEnumerable<ChangeClassification> Changes { get; set; }
	}
}
