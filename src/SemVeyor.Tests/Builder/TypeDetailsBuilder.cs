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
				Fields = Enumerable.Empty<FieldDetails>()
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

		public TypeDetails Build() => _type;

		public static implicit operator TypeDetails(TypeDetailsBuilder builder) => builder.Build();
	}
}
