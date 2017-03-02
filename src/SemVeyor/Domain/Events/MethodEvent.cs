namespace SemVeyor.Domain.Events
{
	public abstract class MethodEvent
	{
		private readonly MethodDetails _older;
		private readonly MethodDetails _newer;

		public MethodEvent(MethodDetails older, MethodDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
