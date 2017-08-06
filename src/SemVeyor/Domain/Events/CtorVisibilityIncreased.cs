namespace SemVeyor.Domain.Events
{
	public class CtorVisibilityIncreased : CtorEvent
	{
		public CtorVisibilityIncreased(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
