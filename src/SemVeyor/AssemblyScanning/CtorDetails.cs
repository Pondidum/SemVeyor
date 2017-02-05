using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class CtorDetails : IMemberDetails
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public IEnumerable<ArgumentDetails> Arguments { get; set; }

		public static CtorDetails From(ConstructorInfo ctor)
		{
			return new CtorDetails
			{
				Name = ctor.Name,
				Visibility = ctor.GetVisibility(),
				Arguments = ctor.GetParameters().Select(ArgumentDetails.From).ToArray()
			};

		}
	}
}
