namespace SemVeyor.Domain.Events
{
	public class MethodArgumentAdded : MethodEvent, IMinor
	{
		public MethodArgumentAdded(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
