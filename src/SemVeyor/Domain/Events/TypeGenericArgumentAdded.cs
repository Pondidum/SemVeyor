namespace SemVeyor.Domain.Events
{
	public class TypeGenericArgumentAdded : IMinor
	{
		private readonly GenericArgumentDetails _newer;

		public TypeGenericArgumentAdded(GenericArgumentDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
