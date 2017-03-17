using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public interface IStorage
	{
		void Write(AssemblyDetails details);
		AssemblyDetails Read();
	}
}
