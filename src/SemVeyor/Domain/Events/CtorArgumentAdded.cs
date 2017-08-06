namespace SemVeyor.Domain.Events
{
	public class CtorArgumentAdded : CtorEvent
	{
		public CtorArgumentAdded(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
