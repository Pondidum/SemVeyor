using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class CtorDetails : IMemberDetails
	{
		public string Name { get; }
		public Visibility Visibility { get; }
		public IEnumerable<ArgumentDetails> Arguments { get; set; }

		public CtorDetails(ConstructorInfo ctor)
		{
			Name = ctor.Name;
			Visibility = ctor.GetVisibility();
			Arguments = ctor.GetParameters().Select(ArgumentDetails.From).ToArray();
		}
	}
}
