namespace SemVeyor.Domain.Events
{
	public class MethodArgumentRemoved : MethodEvent, IMajor
	{
		public MethodArgumentRemoved(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
