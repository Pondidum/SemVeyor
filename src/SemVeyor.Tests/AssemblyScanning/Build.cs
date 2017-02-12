using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public static GenericArgumentDetailsBuilder Generic(string name)
		{
			return new GenericArgumentDetailsBuilder(name);
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

	public class GenericArgumentDetailsBuilder
	{
		private readonly GenericArgumentDetails _arg;

		public GenericArgumentDetailsBuilder(string name)
		{
			_arg = new GenericArgumentDetails
			{
				Name = name,
				Position = 0,
				Constraints = Enumerable.Empty<string>()
			};
		}

		public GenericArgumentDetailsBuilder WithPosition(int position)
		{
			_arg.Position = position;
			return this;
		}

		public GenericArgumentDetailsBuilder WithConstraints(params string[] constraints)
		{
			_arg.Constraints = _arg.Constraints.Concat(constraints).ToArray();
			return this;
		}

		public GenericArgumentDetails Build() => _arg;

		public static implicit operator GenericArgumentDetails(GenericArgumentDetailsBuilder builder) => builder.Build();
	}
}

