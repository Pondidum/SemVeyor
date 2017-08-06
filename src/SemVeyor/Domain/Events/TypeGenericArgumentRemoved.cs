namespace SemVeyor.Domain.Events
{
	public class TypeGenericArgumentRemoved
	{
		private readonly GenericArgumentDetails _newer;

		public TypeGenericArgumentRemoved(GenericArgumentDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
