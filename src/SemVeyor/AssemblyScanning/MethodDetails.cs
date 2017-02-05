using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class MethodDetails : IMemberDetails
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public Type Type { get; set; }
		public IEnumerable<ArgumentDetails> Arguments { get; set; }
		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }

		public static MethodDetails From(MethodInfo method)
		{
			return new MethodDetails
			{
				Name = method.Name,
				Visibility = method.GetVisibility(),
				Type = method.ReturnType,
				Arguments = method.GetParameters().Select(ArgumentDetails.From).ToArray(),
				GenericArguments = method.GetGenericArguments().Select(GenericArgumentDetails.From).ToArray()
			};
		}
	}
}
