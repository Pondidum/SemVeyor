using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class MethodDetails : MemberDetails
	{
		public MethodDetails(MethodBase method)
			: base(method)
		{
		}
	}
}
