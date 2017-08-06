namespace SemVeyor.Domain.Events
{
	public class MethodGenericArgumentAdded : MethodEvent
	{
		public MethodGenericArgumentAdded(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
