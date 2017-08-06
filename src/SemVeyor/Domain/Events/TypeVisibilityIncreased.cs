namespace SemVeyor.Domain.Events
{
	public class TypeVisibilityIncreased
	{
		private readonly Visibility _older;
		private readonly Visibility _newer;

		public TypeVisibilityIncreased(Visibility older, Visibility newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_older} => {_newer}";
	}
}
