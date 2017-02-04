using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentGenericsTests
	{
		public TypeContent Content { get; }

		public TypeContentGenericsTests()
		{
			Content = TypeContent.From(typeof(TestType<,>));
		}

		[Fact]
		public void The_name_is_populated() => Content.Name.ShouldBe("TestType`2");

		[Fact]
		public void The_generic_arguments_are_populated() => Content.GenericArguments.Count().ShouldBe(2);

		public class TestType<TKey, TValue>
		{
		}
	}
}
