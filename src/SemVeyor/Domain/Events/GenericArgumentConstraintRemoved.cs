namespace SemVeyor.Domain.Events
{
	public class GenericArgumentConstraintRemoved
	{
		private readonly string _constraint;

		public GenericArgumentConstraintRemoved(string constraint)
		{
			_constraint = constraint;
		}

		public override string ToString() => $"{nameof(GenericArgumentConstraintRemoved)}: {_constraint}";
	}
}
