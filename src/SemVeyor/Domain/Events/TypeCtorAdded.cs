namespace SemVeyor.Domain.Events
{
	public class TypeCtorAdded
	{
		private readonly CtorDetails _newer;

		public TypeCtorAdded(CtorDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
