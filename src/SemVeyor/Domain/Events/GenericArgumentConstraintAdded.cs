namespace SemVeyor.Domain.Events
{
	public class GenericArgumentConstraintAdded
	{
		private readonly string _constraint;

		public GenericArgumentConstraintAdded(string constraint)
		{
			_constraint = constraint;
		}

		public override string ToString() => $"{nameof(GenericArgumentConstraintAdded)}: {_constraint}";
	}
}
