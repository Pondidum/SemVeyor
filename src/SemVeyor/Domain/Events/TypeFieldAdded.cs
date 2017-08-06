namespace SemVeyor.Domain.Events
{
	public class TypeFieldAdded
	{
		private readonly FieldDetails _field;

		public TypeFieldAdded(FieldDetails field)
		{
			_field = field;
		}

		public override string ToString() => $"{nameof(TypeFieldAdded)}: {_field}";
	}
}
