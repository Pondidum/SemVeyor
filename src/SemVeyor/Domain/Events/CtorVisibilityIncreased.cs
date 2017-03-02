namespace SemVeyor.Domain.Events
{
	public class CtorVisibilityIncreased : CtorEvent, IMinor
	{
		public CtorVisibilityIncreased(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
