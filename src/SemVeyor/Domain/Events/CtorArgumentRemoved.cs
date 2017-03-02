namespace SemVeyor.Domain.Events
{
	public class CtorArgumentRemoved : CtorEvent, IMajor
	{
		public CtorArgumentRemoved(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
