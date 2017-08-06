namespace SemVeyor.Domain.Events
{
	public class MethodVisibilityIncreased : MethodEvent
	{
		public MethodVisibilityIncreased(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
