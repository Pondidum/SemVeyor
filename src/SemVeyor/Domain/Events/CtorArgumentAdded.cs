namespace SemVeyor.Domain.Events
{
	public class CtorArgumentAdded : CtorEvent, IMinor
	{
		public CtorArgumentAdded(CtorDetails older, CtorDetails newer) : base(older, newer)
		{
		}
	}
}
