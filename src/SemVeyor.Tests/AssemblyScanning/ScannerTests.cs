using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using StandardLibrary;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class ScannerTests
	{
		private readonly IEnumerable<TypeDetails> _results;

		public ScannerTests()
		{
			var asm = typeof(PublicEmptyClass).Assembly;

			var scanner = new Scanner();
			_results = scanner.Scan(asm);
		}

		[Fact]
		public void Returns_all_public_types()
		{
			_results.Select(tc => tc.FullName).ShouldBe(new []
			{
				typeof(PublicEmptyClass).FullName,
				typeof(PublicStaticClass).FullName
			});
		}
	}
}
