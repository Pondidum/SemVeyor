using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Tests.Scanning.SourceCode.Queries;
using SemVeyor.Tests.TestUtils;
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
			await Task.Yield();
		}
	}
}
