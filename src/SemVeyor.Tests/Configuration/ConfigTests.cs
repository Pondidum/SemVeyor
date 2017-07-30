using System.Collections.Generic;
using Ploeh.AutoFixture;
using SemVeyor.Configuration;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Configuration
{
	public class ConfigTests
	{
		[Fact]
		public void When_overriding_with_new_options()
		{
			var fx = new Fixture();

			var initial = new Config(
				fx.Create<Options>(),
				fx.Create<IDictionary<string, IDictionary<string, string>>>(),
				fx.Create<IDictionary<string, IDictionary<string, string>>>());

			var combined = initial.OverrideWith(fx.Create<Options>());

			combined.ShouldSatisfyAllConditions(
				() => combined.GlobalOptions.ShouldNotBe(initial.GlobalOptions),
				() => combined.StorageTypes.ShouldBe(initial.StorageTypes),
				() => combined.ReporterTypes.ShouldBe(initial.ReporterTypes));
		}
	}
}
