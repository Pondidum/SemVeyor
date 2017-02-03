using System;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class ArgumentDetails
	{
		public Type Type { get; set; }
		public string Name { get; set; }

		private ArgumentDetails()
		{
		}

		public static ArgumentDetails From(ParameterInfo parameter)
		{
			return new ArgumentDetails
			{
				Name = parameter.Name,
				Type = parameter.ParameterType
			};
		}
	}
}
