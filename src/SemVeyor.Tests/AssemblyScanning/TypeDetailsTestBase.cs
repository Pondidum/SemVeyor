using System;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public abstract class TypeDetailsTestBase<T>
	{
		protected Type InputType { get; }
		protected TypeDetails Details { get; }

		public TypeDetailsTestBase()
		{
			InputType = typeof(T);
			Details = TypeDetails.From(typeof(T));
		}

		[Fact]
		public void The_name_is_populated() => Details.Name.ShouldBe(InputType.Name);

		[Fact]
		public void The_namespace_is_populated() => Details.Namespace.ShouldBe(InputType.Namespace);
	}
}
