using SemVeyor.Configuration;

namespace SemVeyor.Commands.Scan
{
	public class ScanInput
	{
		public bool ReadOnlyFlag { get; set; }
		public string ConfigFlag { get; set; }
		public string Path { get; set; }

		public Options AsOptions() => new Options
		{
			ReadOnly = ReadOnlyFlag,
			Paths = new[] { Path }
		};
	}
}
