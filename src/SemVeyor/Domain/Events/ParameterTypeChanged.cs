namespace SemVeyor.Domain.Events
{
	public class ParameterTypeChanged : IMajor
	{
		private readonly ParameterDetails _older;
		private readonly ParameterDetails _newer;

		public ParameterTypeChanged(ParameterDetails older, ParameterDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(ParameterTypeChanged)}: {_older} => {_newer}";
	}
}
