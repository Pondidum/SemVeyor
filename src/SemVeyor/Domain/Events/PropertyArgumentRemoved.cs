namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentRemoved : IMajor
	{
		private readonly PropertyDetails _older;
		private readonly PropertyDetails _newer;

		public PropertyArgumentRemoved(PropertyDetails older, PropertyDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
