namespace SemVeyor.Domain.Events
{
	public class TypeGenericArgumentAdded
	{
		private readonly GenericArgumentDetails _newer;

		public TypeGenericArgumentAdded(GenericArgumentDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
