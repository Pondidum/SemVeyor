namespace SemVeyor.Domain.Events
{
	public class CtorArgumentRemoved : CtorEvent
	{
		public CtorArgumentRemoved(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
