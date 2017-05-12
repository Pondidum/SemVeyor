using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SemVeyor.Domain;
using SemVeyor.Infrastructure;
using SemVeyor.Scanning;
using SemVeyor.Storage;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace SemVeyor.Tests
{
	public class Scratchpad
	{
		private readonly ITestOutputHelper _output;

		public Scratchpad(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public async Task When_testing_something()
		{
			var scanner = new SourceCodeScanner();
			var assemblyDetails = await scanner.Execute(new AssemblyScannerArgs
			{
				Path = @"D:\dev\projects\SemVeyor\src\TestAssemblies\StandardLibrary\StandardLibrary.csproj"
			});

			_output.WriteLine(assemblyDetails.Name);

			foreach (var type in assemblyDetails.Types)
			{
				_output.WriteLine(type.Name);
			}

		}
	}
}
