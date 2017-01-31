using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class FieldDetails : MemberDetails
	{
		public FieldDetails(FieldInfo info)
			: base(info)
		{
		}
	}
}
