namespace SemVeyor.Reporting
{
	public interface IReporter
	{
		string Name { get; }
		void Write(ReportArgs e);
	}
}
