using System;
using System.Collections.Generic;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class FieldDetails : IMemberDetails
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set;}
		public Type Type { get; set;}

		public static FieldDetails From(FieldInfo info)
		{
			return new FieldDetails
			{
				Name = info.Name,
				Visibility = info.GetVisibility(),
				Type = info.FieldType
			};
		}

		public IEnumerable<object> UpdatedTo(FieldDetails second)
		{
			if (Type != second.Type)
				yield return new FieldTypeChanged();

			if (Visibility > second.Visibility)
				yield return new FieldVisibilityDecreased();

			if (Visibility < second.Visibility)
				yield return new FieldVisibilityIncreased();
		}
	}

	public class FieldVisibilityIncreased { }
	public class FieldVisibilityDecreased { }
	public class FieldTypeChanged { }
}
