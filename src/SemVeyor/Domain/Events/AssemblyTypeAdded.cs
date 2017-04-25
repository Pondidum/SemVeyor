namespace SemVeyor.Domain.Events
{
	public class AssemblyTypeAdded : IMinor
	{
		private readonly TypeDetails _type;

		public AssemblyTypeAdded(TypeDetails type)
		{
			_type = type;
		}

		public override string ToString() => $"{GetType().Name}: {_type.FullName}";
	}
}
