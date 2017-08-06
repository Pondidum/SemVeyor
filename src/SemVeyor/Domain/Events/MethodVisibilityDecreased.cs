namespace SemVeyor.Domain.Events
{
	public class MethodVisibilityDecreased : MethodEvent
	{
		public MethodVisibilityDecreased(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
