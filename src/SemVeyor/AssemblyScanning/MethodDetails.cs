using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class MethodDetails : MemberDetails
	{
		public Type Type { get; }
		public IEnumerable<ArgumentDetails> Arguments { get; }

		public MethodDetails(MethodInfo method)
			: base(method)
		{
			Type = method.ReturnType;
			Arguments = method.GetParameters().Select(ArgumentDetails.From).ToArray();
		}
	}
}
