using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class PropertyDetails : IMemberDetails
	{
		public string Name { get; }
		public Visibility Visibility { get; }
		public Type Type { get; }
		public Visibility? SetterVisibility { get; }
		public IEnumerable<ArgumentDetails> Arguments { get; }

		public PropertyDetails(PropertyInfo prop)
		{
			Name = prop.Name;
			Visibility = prop.GetVisibility();
			Type = prop.PropertyType;
			Arguments = prop.GetIndexParameters().Select(ArgumentDetails.From);
			SetterVisibility = prop.SetMethod != null
				? prop.SetMethod.GetVisibility()
				: (Visibility?)null;
		}
	}
}
