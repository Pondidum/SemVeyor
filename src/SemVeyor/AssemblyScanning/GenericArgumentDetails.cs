using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.AssemblyScanning
{
	public class GenericArgumentDetails
	{
		public int Positon { get; set; }
		public string Name { get; set; }
		public IEnumerable<string> Constraints { get; set; }

		public static GenericArgumentDetails From(Type type)
		{
			return new GenericArgumentDetails
			{
				Name = type.Name,
				Positon = type.GenericParameterPosition,
				Constraints =  type.GetGenericParameterConstraints().Select(c => c.Name)
			};
		}
	}
}
