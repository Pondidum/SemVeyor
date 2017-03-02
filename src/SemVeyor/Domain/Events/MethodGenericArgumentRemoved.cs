namespace SemVeyor.Domain.Events
{
	public class MethodGenericArgumentRemoved : MethodEvent, IMajor
	{
		public MethodGenericArgumentRemoved(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
