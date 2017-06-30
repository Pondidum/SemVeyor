using System;
using System.Collections.Generic;
using SemVeyor.CommandLine;
using SemVeyor.Config;
using SemVeyor.Scanning;
using SemVeyor.Scanning.CompiledAssembly;
using SemVeyor.Scanning.SourceCode;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning
{
	public class ScannerFactoryTests
	{
		private readonly ScannerFactory _factory;

		public ScannerFactoryTests()
		{
			_factory = new ScannerFactory();
		}

		public IAssemblyScanner CreateFor(string path = null)
		{
			var options = new Options();
			if (path != null)
				options.Paths = new[] { path };
			
			return new ScannerFactory().CreateScanner(options);
		}

		[Fact]
		public void When_no_path_is_supplied()
		{
			Should.Throw<NotSupportedException>(() => CreateFor());
		}

		[Theory]
		[InlineData("SomeTest.dll")]
		[InlineData("Something.exe")]
		public void When_the_path_is_a_valid_assembly_type(string path)
		{
			CreateFor(path).ShouldBeOfType<CompiledAssemblyScanner>();
		}

		[Fact]
		public void When_the_path_is_a_csproj()
		{
			CreateFor("Wat.csproj").ShouldBeOfType<SourceCodeScanner>();
		}

		[Fact]
		public void When_the_path_is_something_unsupported()
		{
			Should.Throw<NotSupportedException>(() => CreateFor("readme.md"));
		}
	}
}
