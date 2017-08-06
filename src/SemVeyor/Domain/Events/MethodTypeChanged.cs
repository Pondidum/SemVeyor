namespace SemVeyor.Domain.Events
{
	public class MethodTypeChanged : MethodEvent
	{
		public MethodTypeChanged(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
