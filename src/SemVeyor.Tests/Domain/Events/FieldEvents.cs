using Ploeh.AutoFixture;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain.Events
{
	public class FieldEvents
	{
		private static readonly Fixture Fixture = new Fixture();

		private readonly FieldDetails _older;
		private readonly FieldDetails _newer;

		public FieldEvents()
		{
			_older = Fixture.Create<FieldDetails>();
			_newer = Fixture.Create<FieldDetails>();
		}

		[Fact]
		public void FieldAdded()
		{
			new FieldAdded(_newer).ToString().ShouldBe("FieldAdded: " + _newer);
		}

		[Fact]
		public void FieldRemoved()
		{
			new FieldRemoved(_newer).ToString().ShouldBe("FieldRemoved: " + _newer);
		}

		[Fact]
		public void FieldTypeChanged()
		{
			new FieldTypeChanged(_older, _newer).ToString().ShouldBe("FieldTypeChanged: " + _older + " => " + _newer);
		}
	}
}
