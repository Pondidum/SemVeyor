namespace SemVeyor.Domain.Events
{
	public class ParameterMoved : IMajor
	{
		private readonly ParameterDetails _older;
		private readonly ParameterDetails _newer;

		public ParameterMoved(ParameterDetails older, ParameterDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(ParameterMoved)}: {_older} => {_newer}";
	}
}
