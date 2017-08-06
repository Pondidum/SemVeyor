namespace SemVeyor.Domain.Events
{
	public class MethodArgumentAdded : MethodEvent
	{
		public MethodArgumentAdded(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
