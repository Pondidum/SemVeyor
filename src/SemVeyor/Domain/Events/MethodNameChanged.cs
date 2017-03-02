namespace SemVeyor.Domain.Events
{
	public class MethodNameChanged : MethodEvent, IMajor
	{
		public MethodNameChanged(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
