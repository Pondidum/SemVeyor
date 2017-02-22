using SemVeyor.AssemblyScanning;
using Shouldly;
using System;
using System.Linq;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Tests.Builder;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class CtorDetailsTests
	{
		private static void ChangesShouldBe(CtorDetails older, CtorDetails newer, params Type[] expected)
		{
			older
				.UpdatedTo(newer)
				.Select(c => c.GetType())
				.ShouldBe(expected);
		}

		[Fact]
		public void When_a_ctor_becomes_less_visible()
		{
			var older = Build.Ctor().WithVisibility(Visibility.Public).Build();
			var newer = Build.Ctor().WithVisibility(Visibility.Private).Build();

			ChangesShouldBe(older, newer, typeof(CtorVisibilityDecreased));
		}

		[Fact]
		public void When_a_ctor_becomes_more_visible()
		{
			var older = Build.Ctor().WithVisibility(Visibility.Private).Build();
			var newer = Build.Ctor().WithVisibility(Visibility.Public).Build();

			ChangesShouldBe(older, newer, typeof(CtorVisibilityIncreased));
		}

		[Fact]
		public void When_a_ctor_has_an_argument_added()
		{
			var older = Build.Ctor().WithArguments(Build.Argument<int>("one")).Build();
			var newer = Build.Ctor().WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();

			ChangesShouldBe(older, newer, typeof(CtorArgumentAdded));
		}

		[Fact]
		public void When_a_ctor_has_an_argument_removed()
		{
			var older = Build.Ctor().WithArguments(Build.Argument<int>("one"), Build.Argument<string>("two")).Build();
			var newer = Build.Ctor().WithArguments(Build.Argument<int>("one")).Build();

			ChangesShouldBe(older, newer, typeof(CtorArgumentRemoved));
		}
	}
}
