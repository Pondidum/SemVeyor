namespace SemVeyor.Tests.Builder
{
	public class Build
	{
		public static TypeDetailsBuilder Type(string name)
		{
			return new TypeDetailsBuilder(name);
		}

		public static FieldDetailsBuilder Field<T>(string name)
		{
			return new FieldDetailsBuilder(name, typeof(T));
		}

		public static GenericArgumentDetailsBuilder Generic(string name)
		{
			return new GenericArgumentDetailsBuilder(name);
		}

		public static ArgumentDetailsBuilder Argument<T>(string name)
		{
			return new ArgumentDetailsBuilder(name, typeof(T));
		}

		public static MethodDetailsBuilder Method(string name)
		{
			return new MethodDetailsBuilder(name);
		}
	}
}
