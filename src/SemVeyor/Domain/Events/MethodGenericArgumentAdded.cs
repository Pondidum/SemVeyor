using System;

namespace SemVeyor.Domain.Events
{
	public class MethodGenericArgumentAdded : MethodEvent, IMinor
	{
		public MethodGenericArgumentAdded(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}
	}
}
