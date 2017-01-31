using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class PropertyDetails : MemberDetails
	{
		public PropertyDetails(PropertyInfo prop)
			: base(prop)
		{
		}

		protected override Visibility VisibilityFromType(MemberInfo info)
		{
			var prop = info as PropertyInfo;

			return base.VisibilityFromType(prop?.GetMethod ?? prop?.SetMethod);
		}
	}
}
