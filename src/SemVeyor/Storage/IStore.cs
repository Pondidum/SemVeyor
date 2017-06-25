using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public interface IStore
	{
		void Write(AssemblyDetails details);
		AssemblyDetails Read();
	}
}
