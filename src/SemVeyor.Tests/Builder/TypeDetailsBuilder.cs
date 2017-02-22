using System.Linq;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
	public class TypeDetailsBuilder
	{
		private readonly TypeDetails _type;

		public TypeDetailsBuilder(string name)
		{
			_type = new TypeDetails
			{
				Name = name,
				Visibility = Visibility.Internal,
				Fields = Enumerable.Empty<FieldDetails>(),
				Methods = Enumerable.Empty<MethodDetails>(),
				Properties = Enumerable.Empty<PropertyDetails>(),
				Constructors = Enumerable.Empty<CtorDetails>()
			};
		}

		public TypeDetailsBuilder WithVisibility(Visibility visibility)
		{
			_type.Visibility = visibility;
			return this;
		}

		public TypeDetailsBuilder WithField(FieldDetails field)
		{
			_type.Fields = _type.Fields.Concat(new[] { field }).ToArray();
			return this;
		}

		public TypeDetailsBuilder WithGenericArguments(GenericArgumentDetails argument)
		{
			var position = 0;
			var args = _type.GenericArguments.ToList();
			args.Add(argument);
			args.ForEach(x => x.Position = position++);

			_type.GenericArguments = args;
			return this;
		}

		public TypeDetailsBuilder WithMethods(params MethodDetails[] methods)
		{
			_type.Methods = _type.Methods.Concat(methods).ToArray();
			return this;
		}

		public TypeDetailsBuilder WithProperties(params PropertyDetails[] properties)
		{
			_type.Properties = _type.Properties.Concat(properties).ToArray();
			return this;
		}

		public TypeDetailsBuilder WithCtors(params CtorDetails[] ctors)
		{
			_type.Constructors = _type.Constructors.Concat(ctors).ToArray();
			return this;
		}

		public TypeDetails Build() => _type;

		public static implicit operator TypeDetails(TypeDetailsBuilder builder) => builder.Build();
	}
}
