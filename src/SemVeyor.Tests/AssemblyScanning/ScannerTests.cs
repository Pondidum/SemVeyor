using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using StandardLibrary;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class ScannerTests
	{
		[Fact]
		public void FactMethodName()
		{
			var asm = typeof(PublicEmptyClass).Assembly;

			var scanner = new Scanner();
			var results = scanner.Scan(asm);

			results.Select(tc => tc.FullName).ShouldBe(new []
			{
				typeof(PublicEmptyClass).FullName,
				typeof(PublicStaticClass).FullName
			});
		}
	}
}
