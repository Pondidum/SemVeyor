namespace SemVeyor.Domain.Events
{
	public class CtorArgumentRemoved : IMajor
	{
		private readonly CtorDetails _older;
		private readonly CtorDetails _newer;

		public CtorArgumentRemoved(CtorDetails older, CtorDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
