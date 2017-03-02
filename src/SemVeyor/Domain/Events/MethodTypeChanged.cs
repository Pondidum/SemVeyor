namespace SemVeyor.Domain.Events
{
	public class MethodTypeChanged : MethodEvent, IMajor
	{
		public MethodTypeChanged(MethodDetails older, MethodDetails newer) : base(older, newer)
		{
		}
	}
}
