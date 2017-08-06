namespace SemVeyor.Domain.Events
{
	public class AssemblyTypeRemoved
	{
		private readonly TypeDetails _type;

		public AssemblyTypeRemoved(TypeDetails type)
		{
			_type = type;
		}

		public override string ToString() => $"{GetType().Name}: {_type.FullName}";
	}
}
