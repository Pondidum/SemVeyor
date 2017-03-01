namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentAdded : IMinor
	{
		private readonly PropertyDetails _older;
		private readonly PropertyDetails _newer;

		public PropertyArgumentAdded(PropertyDetails older, PropertyDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
