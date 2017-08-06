namespace SemVeyor.Domain.Events
{
	public class TypeMethodAdded
	{
		private readonly MethodDetails _newer;

		public TypeMethodAdded(MethodDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
