namespace SemVeyor.Tests.Builder
{
	public class Build
	{
		public static AssemblyDetailsBuilder Assembly(string name)
		{
			return new AssemblyDetailsBuilder(name);
		}

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

		public static ParameterDetailsBuilder Parameter<T>(string name)
		{
			return new ParameterDetailsBuilder(name, typeof(T));
		}

		public static MethodDetailsBuilder Method(string name)
		{
			return new MethodDetailsBuilder(name);
		}

		public static PropertyDetailsBuilder Property<T>(string name)
		{
			return new PropertyDetailsBuilder(name, typeof(T));
		}

		public static CtorDetailsBuilder Ctor()
		{
			return new CtorDetailsBuilder();
		}
	}
}
