using System.Collections.Generic;
using SemVeyor.CommandLine;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliParametersTests
	{
		private readonly CliParameters _parameters;

		public CliParametersTests()
		{
			_parameters = new CliParameters
			{
				Arguments =
				{
					{ "", new Dictionary<string, string> {
						{ "storage", "aws-s3" }
					}},
					{ "s3" ,new Dictionary<string, string>
					{
						{ "accesskey", "123456" },
						{ "secretkey", "987654" }
					}}
				},
				Flags =
				{
					{ "", new HashSet<string> { "readonly" }},
					{ "s3", new HashSet<string> { "no-mfa" }}
				},
				Paths =
				{
					"/first/assembly.dll",
					"/second/executable.exe"
				}
			};
		}

		[Fact]
		public void When_creating_for_a_prefix_which_doesnt_exist()
		{

		}
	}
}