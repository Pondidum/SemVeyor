using Ploeh.AutoFixture;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain.Events
{
	public class GenericArgumentEvents
	{
		private static readonly Fixture Fixture = new Fixture();

		private readonly GenericArgumentDetails _older;
		private readonly GenericArgumentDetails _newer;

		public GenericArgumentEvents()
		{
			_older = Fixture.Create<GenericArgumentDetails>();
			_newer = Fixture.Create<GenericArgumentDetails>();
		}

		[Fact]
		public void ConstraintAdded()
		{
			new GenericArgumentConstraintAdded("IExecutable").ToString().ShouldBe("GenericArgumentConstraintAdded: IExecutable");
		}

		[Fact]
		public void ConstraintRemoved()
		{
			new GenericArgumentConstraintRemoved("IExecutable").ToString().ShouldBe("GenericArgumentConstraintRemoved: IExecutable");
		}

		[Fact]
		public void NameChanged()
		{
			new GenericArgumentNameChanged(_older, _newer).ToString().ShouldBe($"GenericArgumentNameChanged: " + _older + " => " + _newer);
		}

		[Fact]
		public void PositionChanged()
		{
			new GenericArgumentPositionChanged(_older, _newer).ToString().ShouldBe($"GenericArgumentPositionChanged: " + _older + " => " + _newer);
		}
	}
}
