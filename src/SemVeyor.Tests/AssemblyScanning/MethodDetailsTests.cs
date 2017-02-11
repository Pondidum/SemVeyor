using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class MethodDetailsTests
	{
		[Fact]
		public void When_a_method_becomes_more_visible()
		{
			var first = From<int>(Visibility.Protected);
			var second = From<int>(Visibility.Public);

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodVisibilityIncreased)
			});
		}

		[Fact]
		public void When_a_method_becomes_less_visible()
		{
			var first = From<int>(Visibility.Public);
			var second = From<int>(Visibility.Protected);

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodVisibilityDecreased)
			});
		}

		[Fact]
		public void When_a_methods_return_type_changes()
		{
			var first = From<int>();
			var second = From<string>();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodTypeChanged)
			});
		}

		[Fact]
		public void When_a_methods_has_an_argument_added()
		{
			var first = From<int>(customise: x => x.Arguments = Args(Arg<int>("one") ));
			var second = From<int>(customise: x => x.Arguments = Args(Arg<int>("one"), Arg<string>("two") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_two_arguments_added()
		{
			var first = From<int>();
			var second = From<int>(customise: x => x.Arguments = Args(Arg<int>("one"), Arg<string>("two") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentAdded),
				typeof(MethodArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_an_argument_removed()
		{
			var first = From<int>(customise: x => x.Arguments = Args(Arg<int>("one"), Arg<string>("two") ));
			var second = From<int>(customise: x => x.Arguments = Args(Arg<int>("one") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_method_has_two_arguments_removed()
		{
			var first = From<int>(customise: x => x.Arguments = Args(Arg<int>("one"), Arg<string>("two") ));
			var second = From<int>();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentRemoved),
				typeof(MethodArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_methods_arguments_change_order()
		{
			var first = From<int>(customise: x => x.Arguments = Args(Arg<int>("one"), Arg<string>("two") ));
			var second = From<int>(customise: x => x.Arguments = Args(Arg<string>("two"), Arg<int>("one") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(ArgumentMoved),
				typeof(ArgumentMoved)
			});
		}

		[Fact]
		public void When_a_method_has_a_generic_argument_added()
		{
			var first = From<int>(customise: x => x.GenericArguments = Generics( Generic("T") ));
			var second = From<int>(customise: x => x.GenericArguments = Generics( Generic("T"), Generic("TVal") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_two_generic_arguments_added()
		{
			var first = From<int>();
			var second = From<int>(customise: x => x.GenericArguments = Generics( Generic("T"), Generic("TVal") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentAdded),
				typeof(MethodGenericArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_a_generic_argument_removed()
		{
			var first = From<int>(customise: x => x.GenericArguments = Generics( Generic("T"), Generic("TVal") ));
			var second = From<int>(customise: x => x.GenericArguments = Generics( Generic("T") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_method_has_two_generic_arguments_removed()
		{
			var first = From<int>(customise: x => x.GenericArguments = Generics( Generic("T"), Generic("TVal") ));
			var second = From<int>();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentRemoved),
				typeof(MethodGenericArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_methods_generic_arguments_change_order()
		{
			var first = From<int>(customise: x => x.GenericArguments = Generics( Generic("T"), Generic("TVal") ));
			var second = From<int>(customise: x => x.GenericArguments = Generics( Generic("TVal"), Generic("T") ));

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(GenericArgumentPositionChanged),
				typeof(GenericArgumentPositionChanged)
			});
		}

		private static MethodDetails From<T>(Visibility visibility = Visibility.Public, Action<MethodDetails> customise = null)
		{
			var md = new MethodDetails
			{
				Name = "SomeMethod",
				Type = typeof(T),
				Visibility = visibility,
				Arguments = Enumerable.Empty<ArgumentDetails>(),
				GenericArguments = Enumerable.Empty<GenericArgumentDetails>()
			};

			customise?.Invoke(md);

			return md;
		}

		private static IEnumerable<ArgumentDetails> Args(params ArgumentDetails[] args)
		{
			var position = 0;
			foreach (var arg in args)
			{
				arg.Position = position++;
				yield return arg;
			}
		}

		private static ArgumentDetails Arg<T>(string name)
		{
			return new ArgumentDetails
			{
				Name = name,
				Type = typeof(T)
			};
		}

		private static IEnumerable<GenericArgumentDetails> Generics(params GenericArgumentDetails[] args)
		{
			var position = 0;
			foreach (var arg in args)
			{
				arg.Position = position++;
				yield return arg;
			}
		}

		private static GenericArgumentDetails Generic(string name, params string[] constriants)
		{
			return new GenericArgumentDetails
			{
				Name = name,
				Constraints = constriants
			};
		}
	}
}