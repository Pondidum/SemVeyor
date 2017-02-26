namespace SemVeyor.Domain.Events
{
	public class FieldRemoved : IMajor
	{
		private readonly FieldDetails _field;

		public FieldRemoved(FieldDetails field)
		{
			_field = field;
		}

		public override string ToString() => $"{nameof(FieldRemoved)}: {_field}";
	}
}
