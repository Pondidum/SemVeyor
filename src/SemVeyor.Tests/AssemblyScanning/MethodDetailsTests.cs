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
			var first = Build.Method("M1").WithVisibility(Visibility.Protected).Build();
			var second = Build.Method("M1").WithVisibility(Visibility.Public).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodVisibilityIncreased)
			});
		}

		[Fact]
		public void When_a_method_becomes_less_visible()
		{
			var first = Build.Method("M1").WithVisibility(Visibility.Public).Build();
			var second = Build.Method("M1").WithVisibility(Visibility.Protected).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodVisibilityDecreased)
			});
		}

		[Fact]
		public void When_a_methods_return_type_changes()
		{
			var first = Build.Method("M1").Returning<int>().Build();
			var second = Build.Method("M1").Returning<string>().Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodTypeChanged)
			});
		}

		[Fact]
		public void When_a_methods_has_an_argument_added()
		{

			var first = Build.Method("").WithArguments(Build.Argument<int>("one")).Build();
			var second = Build.Method("").WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_two_arguments_added()
		{
			var first = Build.Method("").Build();
			var second = Build.Method("").WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();

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
			var first = Build.Method("").WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();
			var second = Build.Method("").WithArguments(Build.Argument<int>("one")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_method_has_two_arguments_removed()
		{
			var first = Build.Method("").WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();
			var second = Build.Method("").Build();

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
			var first = Build.Method("").WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();
			var second = Build.Method("").WithArguments(Build.Argument<string>("two"), Build.Argument<int>("one")).Build();

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
			var first = Build.Method("").WithGenericArguments(Build.Generic("T")).Build();
			var second = Build.Method("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentAdded)
			});
		}

		[Fact]
		public void When_a_method_has_two_generic_arguments_added()
		{
			var first = Build.Method("").Build();
			var second = Build.Method("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();

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
			var first = Build.Method("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var second = Build.Method("").WithGenericArguments(Build.Generic("T")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodGenericArgumentRemoved)
			});
		}

		[Fact]
		public void When_a_method_has_two_generic_arguments_removed()
		{
			var first = Build.Method("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var second = Build.Method("").Build();

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
			var first = Build.Method("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var second = Build.Method("").WithGenericArguments(Build.Generic("TVal"), Build.Generic("T")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(GenericArgumentPositionChanged),
				typeof(GenericArgumentPositionChanged)
			});
		}
	}
}