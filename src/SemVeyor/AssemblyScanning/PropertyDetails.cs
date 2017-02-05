using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class PropertyDetails : IMemberDetails
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public Type Type { get; set; }
		public Visibility? SetterVisibility { get; set; }
		public IEnumerable<ArgumentDetails> Arguments { get; set; }

		public static PropertyDetails From(PropertyInfo prop)
		{
			return new PropertyDetails
			{
				Name = prop.Name,
				Visibility = prop.GetVisibility(),
				Type = prop.PropertyType,
				Arguments = prop.GetIndexParameters().Select(ArgumentDetails.From),
				SetterVisibility = prop.SetMethod != null
					? prop.SetMethod.GetVisibility()
					: (Visibility?)null
			};
		}
	}
}
