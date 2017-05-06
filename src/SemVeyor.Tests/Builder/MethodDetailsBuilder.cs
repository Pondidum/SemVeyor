using System;
using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Tests.Builder
{
	public class MethodDetailsBuilder
	{
		private readonly MethodDetails _method;

		public MethodDetailsBuilder(string name, Type type)
			: this(name)
		{
			_method.Type = type;
		}

		public MethodDetailsBuilder(string name)
		{
			_method = new MethodDetails
			{
				Name = name,
				Visibility = Visibility.Internal,
				Parameters = Enumerable.Empty<ParameterDetails>(),
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

		public MethodDetailsBuilder WithParameters(params ParameterDetails[] parameter)
		{
			var position = 0;
			var args = _method.Parameters.ToList();
			args.AddRange(parameter);
			args.ForEach(x => x.Position = position++);

			_method.Parameters = args;
			return this;
		}

		public MethodDetailsBuilder WithGenericArguments(params GenericArgumentDetails[] arguments)
		{
			var replacement = _method.GenericArguments.Concat(arguments).ToArray();

			var position = 0;
			foreach (var a in replacement)
				a.Position = position++;

			_method.GenericArguments = replacement;
			return this;
		}

		public MethodDetails Build() => _method;

		public static implicit operator MethodDetails(MethodDetailsBuilder builder) => builder.Build();
	}
}
