namespace SemVeyor.Domain.Events
{
	public class FieldAdded : IMinor
	{
		private readonly FieldDetails _field;

		public FieldAdded(FieldDetails field)
		{
			_field = field;
		}

		public override string ToString() => $"{nameof(FieldAdded)}: {_field}";
	}
}
