using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class FieldDetails : IMemberDetails
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set;}
		public object Type { get; set;}

		public static FieldDetails From(FieldInfo info)
		{
			return new FieldDetails
			{
				Name = info.Name,
				Visibility = info.GetVisibility(),
				Type = info.FieldType
			};
		}
	}
}
