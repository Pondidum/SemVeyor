namespace SemVeyor.Domain.Events
{
	public class TypePropertyRemoved : IMajor
	{
		private readonly PropertyDetails _newer;

		public TypePropertyRemoved(PropertyDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
