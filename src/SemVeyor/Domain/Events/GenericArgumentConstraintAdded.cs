namespace SemVeyor.Domain.Events
{
	public class GenericArgumentConstraintAdded : IMinor
	{
		private readonly string _constraint;

		public GenericArgumentConstraintAdded(string constraint)
		{
			_constraint = constraint;
		}

		public override string ToString() => $"{nameof(GenericArgumentConstraintAdded)}: {_constraint}";
	}
}
