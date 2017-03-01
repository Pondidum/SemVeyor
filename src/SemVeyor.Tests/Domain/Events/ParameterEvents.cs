using Ploeh.AutoFixture;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain.Events
{
	public class ParameterEvents
	{
		private static readonly Fixture Fixture = new Fixture();

		private readonly ParameterDetails _older;
		private readonly ParameterDetails _newer;

		public ParameterEvents()
		{
			_older = Fixture.Create<ParameterDetails>();
			_newer = Fixture.Create<ParameterDetails>();
		}

		[Fact]
		public void ParameterMoved()
		{
			new ParameterMoved(_older, _newer).ToString()
				.ShouldBe($"ParameterMoved: [{_older.Position}] {_older.Type} {_older.Name} => [{_newer.Position}] {_newer.Type} {_newer.Name}");
		}

		[Fact]
		public void ParameterNameChanged()
		{
			new ParameterNameChanged(_older, _newer).ToString().ShouldBe("ParameterNameChanged: " + _older + " => " + _newer);
		}

		[Fact]
		public void ParameterTypeChanged()
		{
			new ParameterTypeChanged(_older, _newer).ToString().ShouldBe("ParameterTypeChanged: " + _older + " => " + _newer);
		}
	}
}
