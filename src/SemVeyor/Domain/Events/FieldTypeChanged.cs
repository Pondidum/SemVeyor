namespace SemVeyor.Domain.Events
{
	public class FieldTypeChanged : IMajor
	{
		private readonly FieldDetails _older;
		private readonly FieldDetails _newer;

		public FieldTypeChanged(FieldDetails older, FieldDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(FieldTypeChanged)}: {_older} => {_newer}";
	}
}
