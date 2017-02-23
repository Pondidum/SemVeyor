using System.Linq;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
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
