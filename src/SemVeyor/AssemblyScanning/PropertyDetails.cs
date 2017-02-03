using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class PropertyDetails : MemberDetails
	{
		public Type Type { get; }
		public Visibility? SetterVisibility { get; }
		public IEnumerable<ArgumentDetails> Arguments { get; }

		public PropertyDetails(PropertyInfo prop)
			: base(prop)
		{
			Type = prop.PropertyType;
			Arguments = prop.GetIndexParameters().Select(ArgumentDetails.From);
			SetterVisibility = prop.SetMethod != null
				? VisibilityFromType(prop?.SetMethod)
				: (Visibility?)null;
		}

		protected override Visibility VisibilityFromType(MemberInfo info)
		{
			var prop = info as PropertyInfo;

			return prop != null
				? base.VisibilityFromType(prop?.GetMethod ?? prop?.SetMethod)
				: base.VisibilityFromType(info);
		}
	}
}
