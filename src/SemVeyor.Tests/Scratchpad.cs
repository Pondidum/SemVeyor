using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using SemVeyor.Domain;
using SemVeyor.Infrastructure;
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
		public void When_testing_something()
		{
			var current = AssemblyDetails.From(Assembly.LoadFile(@"D:\dev\projects\SemVeyor\packages\Stronk.2.0.0\lib\net452\stronk.dll"));

			foreach (var e in new AssemblyDetails().UpdatedTo(current))
			{
				_output.WriteLine(e.ToString());
			}
		}
	}
}
