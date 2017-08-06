namespace SemVeyor.Domain.Events
{
	public class ParameterNameChanged
	{
		private readonly ParameterDetails _older;
		private readonly ParameterDetails _newer;

		public ParameterNameChanged(ParameterDetails older, ParameterDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(ParameterNameChanged)}: {_older} => {_newer}";
	}
}
