namespace SemVeyor.Domain.Events
{
	public class TypeCtorRemoved : IMajor
	{
		private readonly CtorDetails _newer;

		public TypeCtorRemoved(CtorDetails newer)
		{
			_newer = newer;
		}

		public override string ToString() => $"{GetType().Name}: {_newer}";
	}
}
