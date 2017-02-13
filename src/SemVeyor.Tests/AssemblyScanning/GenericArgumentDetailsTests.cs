using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Tests.Builder;
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
				() => details.Position.ShouldBe(1),
				() => details.Constraints.ShouldBe(new[] { nameof(IEnumerable) })
			);
		}

		[Fact]
		public void When_an_argument_has_not_changed()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();
			var newer = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();

			var changes = older.UpdatedTo(newer);

			changes.ShouldBeEmpty();
		}

		[Fact]
		public void When_an_argument_has_changed_position()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();
			var newer = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").WithPosition(1).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentPositionChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_changed_name()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();
			var newer = Build.Generic("TValue").WithConstraints("IEnumerable<TKey>").Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentNameChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_a_constraint_added()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();
			var newer = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>", "IEquatable<TKey>").Build();;

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentConstraintAdded)
			});
		}

		[Fact]
		public void When_an_argument_has_a_constraint_removed()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>").Build();
			var newer = Build.Generic("TKey").Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(GenericArgumentConstraintRemoved)
			});
		}

		[Fact]
		public void When_an_arguments_constraints_change_order()
		{
			var older = Build.Generic("TKey").WithConstraints("IEnumerable<TKey>", "ICollection<TKey>").Build();
			var newer = Build.Generic("TKey").WithConstraints("ICollection<TKey>", "IEnumerable<TKey>").Build();

			var changes = older.UpdatedTo(newer);

			changes.ShouldBeEmpty();
		}

		public TValue GenericMethod<TKey, TValue>(TValue test, Action<TValue> action)
			where TValue : IEnumerable
		{
			throw new NotSupportedException();
		}
	}
}
