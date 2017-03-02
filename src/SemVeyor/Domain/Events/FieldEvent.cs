namespace SemVeyor.Domain.Events
{
	public abstract class FieldEvent
	{
		private readonly FieldDetails _older;
		private readonly FieldDetails _newer;

		public FieldEvent(FieldDetails older, FieldDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
