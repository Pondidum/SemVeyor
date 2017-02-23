using Shouldly;
using SemVeyor.AssemblyScanning;
using System;
using System.Linq;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Tests.Builder;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class AssemblyDetailsTests
	{
		private static void ChangesShouldBe(AssemblyDetails older, AssemblyDetails newer, params Type[] expected)
		{
			older
				.UpdatedTo(newer)
				.Select(c => c.GetType())
				.ShouldBe(expected);
		}

		[Fact]
		public void When_a_type_is_added()
		{
			var older = Build.Assembly("Test").WithTypes(Build.Type("First")).Build();
			var newer = Build.Assembly("Test").WithTypes(Build.Type("First"), Build.Type("Second")).Build();

			ChangesShouldBe(older, newer, typeof(AssemblyTypeAdded));
		}

		[Fact]
		public void When_a_type_is_removed()
		{
			var older = Build.Assembly("Test").WithTypes(Build.Type("First"), Build.Type("Second")).Build();
			var newer = Build.Assembly("Test").WithTypes(Build.Type("First")).Build();

			ChangesShouldBe(older, newer, typeof(AssemblyTypeRemoved));
		}

		[Fact]
		public void When_a_type_is_changed()
		{
			var older = Build.Assembly("Test").WithTypes(Build.Type("First")).Build();
			var newer = Build.Assembly("Test").WithTypes(Build.Type("First").WithVisibility(Visibility.Public)).Build();

			ChangesShouldBe(older, newer, typeof(TypeVisibilityIncreased));
		}
	}
}
