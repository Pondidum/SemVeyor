using System.Net.Mail;
using SemVeyor.Domain;
using SemVeyor.Storage;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class TypeNameTests
	{
		[Fact]
		public void When_comparing_the_same_instance()
		{
			var name = new TypeName("wat");

			ShouldMatch(name, name);
		}

		[Fact]
		public void When_comparing_different_matching_instances()
		{
			var a = new TypeName("wat is this");
			var b = new TypeName("wat is this");

			ShouldMatch(a, b);
		}

		[Fact]
		public void When_comparing_different_non_matching_instances()
		{
			var a = new TypeName("wat");
			var b = new TypeName("is this?");

			ShouldNotMatch(a, b);
		}

		private void ShouldMatch(TypeName a, TypeName b)
		{
			typeof(TypeName).ShouldSatisfyAllConditions(
				() => a.Equals(b).ShouldBeTrue(),
				() => b.Equals(a).ShouldBeTrue(),

				() => (a == b).ShouldBeTrue(),
				() => (b == a).ShouldBeTrue(),

				() => (a != b).ShouldBeFalse(),
				() => (b != a).ShouldBeFalse(),

				() => a.GetHashCode().ShouldBe(b.GetHashCode()),
				() => b.GetHashCode().ShouldBe(a.GetHashCode()),

				() => object.Equals(a, b).ShouldBeTrue(),
				() => object.Equals(b, a).ShouldBeTrue()
			);
		}

		private void ShouldNotMatch(TypeName a, TypeName b)
		{
			typeof(TypeName).ShouldSatisfyAllConditions(
				() => a.Equals(b).ShouldBeFalse(),
				() => b.Equals(a).ShouldBeFalse(),

				() => (a == b).ShouldBeFalse(),
				() => (b == a).ShouldBeFalse(),

				() => (a != b).ShouldBeTrue(),
				() => (b != a).ShouldBeTrue(),

				() => a.GetHashCode().ShouldNotBe(b.GetHashCode()),
				() => b.GetHashCode().ShouldNotBe(a.GetHashCode()),

				() => object.Equals(a, b).ShouldBeFalse(),
				() => object.Equals(b, a).ShouldBeFalse()
			);
		}
	}
}