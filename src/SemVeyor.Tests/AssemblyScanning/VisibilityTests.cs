using System;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class VisibilityTests
	{
		[Theory]
		[InlineData(typeof(NestedPublicClass), Visibility.Public)]
		[InlineData(typeof(NestedInternalClass), Visibility.Internal)]
		[InlineData(typeof(NestedProtectedClass), Visibility.Protected)]
		[InlineData(typeof(NestedPrivateClass), Visibility.Private)]
		[InlineData(typeof(StandalonePublicClass), Visibility.Public)]
		[InlineData(typeof(StandaloneInternalClass), Visibility.Internal)]
		public void When_checking_a_class(Type type, Visibility expected) => type.GetVisibility().ShouldBe(expected);

		public class NestedPublicClass { }
		internal class NestedInternalClass { }
		protected class NestedProtectedClass { }
		private class NestedPrivateClass { }
	}

	public class StandalonePublicClass { }
	internal class StandaloneInternalClass { }
}