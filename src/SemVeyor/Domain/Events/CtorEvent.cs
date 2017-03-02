namespace SemVeyor.Domain.Events
{
	public abstract class CtorEvent
	{
		private readonly CtorDetails _older;
		private readonly CtorDetails _newer;

		protected CtorEvent(CtorDetails older, CtorDetails newer)
		{
			_older = older;
			_newer = newer;
		}
		
		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
