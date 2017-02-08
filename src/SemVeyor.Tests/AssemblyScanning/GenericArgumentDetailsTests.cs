using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class GenericArgumentDetailsTests
	{
		[Fact]
		public void When_populating_from_a_method()
		{
			var generic = typeof(GenericArgumentDetailsTests)
				.GetMethod(nameof(GenericMethod))
				.GetGenericArguments()
				.Last();

			var details = GenericArgumentDetails.From(generic);

			details.ShouldSatisfyAllConditions(
				() => details.Name.ShouldBe("TValue"),
				() => details.Positon.ShouldBe(1),
				() => details.Constraints.ShouldBe(new[] { nameof(IEnumerable) })
			);
		}

		[Fact]
		public void When_an_argument_has_not_changed()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>");
			var newer = From(0, "TKey", "IEnumerable<TKey>");

			var changes = older.UpdatedTo(newer);

			changes.ShouldBeEmpty();
		}

		[Fact]
		public void When_an_argument_has_changed_position()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>");
			var newer = From(1, "TKey", "IEnumerable<TKey>");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentPositionChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_changed_name()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>");
			var newer = From(0, "TValue", "IEnumerable<TKey>");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentNameChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_a_constraint_added()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>");
			var newer = From(0, "TKey", "IEnumerable<TKey>", "IEquatable<TKey>");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentConstraintAdded)
			});
		}

		[Fact]
		public void When_an_argument_has_a_constraint_removed()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>");
			var newer = From(0, "TKey");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentConstraintRemoved)
			});
		}

		[Fact]
		public void When_an_arguments_constraints_change_order()
		{
			var older = From(0, "TKey", "IEnumerable<TKey>", "ICollection<TKey>");
			var newer = From(0, "TKey", "ICollection<TKey>", "IEnumerable<TKey>");

			var changes = older.UpdatedTo(newer);

			changes.ShouldBeEmpty();
		}

		private static GenericArgumentDetails From(int position, string name, params string[] constraints)
		{
			return new GenericArgumentDetails
			{
				Name = name,
				Positon = position,
				Constraints = constraints
			};
		}

		public TValue GenericMethod<TKey, TValue>(TValue test, Action<TValue> action)
			where TValue : IEnumerable
		{
			throw new NotSupportedException();
		}
	}
}
