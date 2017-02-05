using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class FieldDetails : IMemberDetails
	{
		public string Name { get; }
		public Visibility Visibility { get; }
		public object Type { get; }

		public FieldDetails(FieldInfo info)
		{
			Name = info.Name;
			Visibility = info.GetVisibility();
			Type = info.FieldType;
		}
	}
}
