using System;
using System.Collections;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class GenericArgumentDetailsTests
	{
		private readonly GenericArgumentDetails _details;

		public GenericArgumentDetailsTests()
		{
			var generic = typeof(GenericArgumentDetailsTests)
				.GetMethod(nameof(GenericMethod))
				.GetGenericArguments()
				.Last();

			_details = GenericArgumentDetails.From(generic);
		}

		[Fact]
		public void The_name_is_popualted() => _details.Name.ShouldBe("TValue");

		[Fact]
		public void The_position_is_populated() => _details.Positon.ShouldBe(1);

		[Fact]
		public void The_constraints_are_populated() => _details.Constraints.ShouldBe(new[] { nameof(IEnumerable) });

		public TValue GenericMethod<TKey, TValue>(TValue test, Action<TValue> action)
			where TValue : IEnumerable
		{
			throw new NotSupportedException();
		}
	}
}
