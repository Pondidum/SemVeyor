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

		public static ArgumentDetailsBuilder Argument<T>(string name)
		{
			return new ArgumentDetailsBuilder(name, typeof(T));
		}

		public static MethodDetailsBuilder Method(string name)
		{
			return new MethodDetailsBuilder(name);
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

	public class ArgumentDetailsBuilder
	{
		private readonly ArgumentDetails _argument;

		public ArgumentDetailsBuilder(string name, Type type)
		{
			_argument = new ArgumentDetails
			{
				Name = name,
				Type =  type
			};
		}

		public ArgumentDetailsBuilder WithPosition(int position)
		{
			_argument.Position = position;
			return this;
		}

		public ArgumentDetails Build() => _argument;

		public static implicit operator ArgumentDetails(ArgumentDetailsBuilder builder) => builder.Build();
	}

	public class MethodDetailsBuilder
	{
		private readonly MethodDetails _method;

		public MethodDetailsBuilder(string name)
		{
			_method = new MethodDetails
			{
				Name = name,
				Visibility = Visibility.Internal,
				Arguments = Enumerable.Empty<ArgumentDetails>(),
				GenericArguments = Enumerable.Empty<GenericArgumentDetails>()
			};
		}

		public MethodDetailsBuilder Returning<T>()
		{
			_method.Type = typeof(T);
			return this;
		}

		public MethodDetailsBuilder WithVisibility(Visibility visibility)
		{
			_method.Visibility = visibility;
			return this;
		}

		public MethodDetailsBuilder WithArguments(params ArgumentDetails[] argument)
		{
			var position = 0;
			var args = _method.Arguments.ToList();
			args.AddRange(argument);
			args.ForEach(x => x.Position = position++);

			_method.Arguments = args;
			return this;
		}

		public MethodDetailsBuilder WithGenericArguments(params GenericArgumentDetails[] argument)
		{
			var position = 0;
			var args = _method.GenericArguments.ToList();
			args.AddRange(argument);
			args.ForEach(x => x.Position = position++);

			_method.GenericArguments = args;
			return this;
		}

		public MethodDetails Build() => _method;

		public static implicit operator MethodDetails(MethodDetailsBuilder builder) => builder.Build();
	}
}

