namespace SemVeyor.Domain.Events
{
	public class MethodArgumentRemoved : MethodEvent
	{
		public MethodArgumentRemoved(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
