namespace SemVeyor.Domain.Events
{
	public class MethodVisibilityDecreased : MethodEvent, IMajor
	{
		public MethodVisibilityDecreased(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
