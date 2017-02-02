using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public abstract class TypeContentTest<T>
	{
		protected Type InputType { get; }
		protected TypeContent Content { get; }

		public TypeContentTest()
		{
			InputType = typeof(T);
			Content = TypeContent.From(typeof(T));
		}

		[Fact]
		public void The_name_is_populated() => Content.Name.ShouldBe(InputType.Name);

		[Fact]
		public void The_namespace_is_populated() => Content.Namespace.ShouldBe(InputType.Namespace);
	}

	public static class MemberDetailsExtensions
	{
		public static T ByVisibility<T>(this IEnumerable<T> self, Visibility visibility)
			where T : MemberDetails
		{
			return self.FirstOrDefault(m => m.Visibility == visibility);
		}
	}
}
