using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Acceptance
{
	public class Scenarios
	{
		[Fact]
		public void When_an_overloaded_method_has_a_generic_argument_added()
		{
			var older = Build
				.Type("PublicEmptyClass")
				.WithMethods(
					Build
						.Method("Execute")
						.WithVisibility(Visibility.Public),
					Build
						.Method("Execute")
						.WithVisibility(Visibility.Public)
						.WithParameters(Build.Parameter<string>("name")))
				.Build();

			var newer = Build
				.Type("PublicEmptyClass")
				.WithMethods(
					Build
						.Method("Execute")
						.WithVisibility(Visibility.Public),
					Build
						.Method("Execute")
						.WithVisibility(Visibility.Public)
						.WithGenericArguments(Build.Generic("T"))
						.WithParameters(Build.Parameter<string>("name")))
				.Build();

			var changes = older
				.UpdatedTo(newer)
				.Select(e => e.GetType())
				.ToArray();

			changes.ShouldBe(new []
			{
				typeof(MethodGenericArgumentAdded)
			});
		}
	}
}