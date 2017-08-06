namespace SemVeyor.Domain.Events
{
	public class MethodGenericArgumentRemoved : MethodEvent
	{
		public MethodGenericArgumentRemoved(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
