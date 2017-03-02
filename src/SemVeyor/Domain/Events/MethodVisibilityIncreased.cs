namespace SemVeyor.Domain.Events
{
	public class MethodVisibilityIncreased : MethodEvent, IMinor
	{
		public MethodVisibilityIncreased(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
