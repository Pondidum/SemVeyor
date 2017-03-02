namespace SemVeyor.Domain.Events
{
	public abstract class PropertyEvent
	{
		private readonly PropertyDetails _older;
		private readonly PropertyDetails _newer;

		public PropertyEvent(PropertyDetails older, PropertyDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
