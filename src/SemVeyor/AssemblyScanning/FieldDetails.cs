using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class FieldDetails : MemberDetails
	{
		public object Type { get; }

		public FieldDetails(FieldInfo info)
			: base(info)
		{
			Type = info.FieldType;
		}
	}
}
