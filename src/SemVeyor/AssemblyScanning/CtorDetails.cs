using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class CtorDetails : MemberDetails
	{
		//public IEnumerable<ArgumentModel> Arguments { get; set; }

		public CtorDetails(ConstructorInfo ctor)
			: base(ctor)
		{
		}
	}
}
