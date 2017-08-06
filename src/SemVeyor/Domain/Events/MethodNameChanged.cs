namespace SemVeyor.Domain.Events
{
	public class MethodNameChanged : MethodEvent
	{
		public MethodNameChanged(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
