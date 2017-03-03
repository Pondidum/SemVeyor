namespace SemVeyor.Domain.Events
{
	public class TypePropertyAdded : IMinor
	{
		private readonly PropertyDetails _newer;

		public TypePropertyAdded(PropertyDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
