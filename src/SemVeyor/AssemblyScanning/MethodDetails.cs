using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class MethodDetails : IMemberDetails
	{
		public string Name { get; }
		public Visibility Visibility { get; }
		public Type Type { get; }
		public IEnumerable<ArgumentDetails> Arguments { get; }
		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }

		public MethodDetails(MethodInfo method)
		{
			Name = method.Name;
			Visibility = method.GetVisibility();
			Type = method.ReturnType;
			Arguments = method.GetParameters().Select(ArgumentDetails.From).ToArray();
			GenericArguments = method.GetGenericArguments().Select(GenericArgumentDetails.From).ToArray();
		}

	}
}
