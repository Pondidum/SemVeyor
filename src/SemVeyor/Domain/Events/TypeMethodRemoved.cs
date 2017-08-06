namespace SemVeyor.Domain.Events
{
	public class TypeMethodRemoved
	{
		private readonly MethodDetails _newer;

		public TypeMethodRemoved(MethodDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
