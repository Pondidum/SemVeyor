namespace SemVeyor.AssemblyScanning
{
	public interface IMemberDetails
	{
		Visibility Visibility { get; }
		string Name { get; }
	}
}
