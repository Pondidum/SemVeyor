using System;
using System.Linq;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.AssemblyScanning
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
	}

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

		public TypeDetails Build() => _type;

		public static implicit operator TypeDetails(TypeDetailsBuilder builder) => builder.Build();
	}

	public class FieldDetailsBuilder
	{
		private readonly FieldDetails _field;

		public FieldDetailsBuilder(string name, Type type)
		{
			_field = new FieldDetails
			{
				Type =  type,
				Name = name,
				Visibility = Visibility.Private
			};
		}

		public FieldDetailsBuilder WithVisibility(Visibility visibility)
		{
			_field.Visibility = visibility;
			return this;
		}

		public FieldDetails Build() => _field;

		public static implicit operator FieldDetails(FieldDetailsBuilder builder) => builder.Build();
	}
}

