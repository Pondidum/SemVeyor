namespace SemVeyor.Domain.Events
{
	public class CtorVisibilityDecreased : CtorEvent
	{
		public CtorVisibilityDecreased(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
