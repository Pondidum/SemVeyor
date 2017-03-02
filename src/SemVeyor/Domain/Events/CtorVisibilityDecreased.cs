namespace SemVeyor.Domain.Events
{
	public class CtorVisibilityDecreased : CtorEvent, IMajor
	{
		public CtorVisibilityDecreased(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
