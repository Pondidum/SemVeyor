namespace SemVeyor.Domain.Events
{
	public class TypeFieldRemoved : IMajor
	{
		private readonly FieldDetails _field;

		public TypeFieldRemoved(FieldDetails field)
		{
			_field = field;
		}

		public override string ToString() => $"{nameof(TypeFieldRemoved)}: {_field}";
	}
}
