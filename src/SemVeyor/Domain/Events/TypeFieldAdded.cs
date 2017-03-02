namespace SemVeyor.Domain.Events
{
	public class TypeFieldAdded : IMinor
	{
		private readonly FieldDetails _field;

		public TypeFieldAdded(FieldDetails field)
		{
			_field = field;
		}

		public override string ToString() => $"{nameof(TypeFieldAdded)}: {_field}";
	}
}
