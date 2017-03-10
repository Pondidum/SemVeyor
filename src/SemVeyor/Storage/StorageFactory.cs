using SemVeyor.CommandLine;

namespace SemVeyor.Storage
{
	public class StorageFactory
	{
		public IStorage CreateStore(Options options)
		{
			return new FileStore("history.lsj");
		}
	}
}
