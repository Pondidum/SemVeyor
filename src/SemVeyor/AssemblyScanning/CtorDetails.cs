using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class CtorDetails : MemberDetails
	{
		public IEnumerable<ArgumentDetails> Arguments { get; set; }

		public CtorDetails(ConstructorInfo ctor)
			: base(ctor)
		{
			Arguments = ctor.GetParameters().Select(a => new ArgumentDetails()).ToArray();
		}
	}
}
