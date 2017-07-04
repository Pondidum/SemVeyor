using System;
using FileSystem;

namespace SemVeyor.Storage
{
	public class StorageFactory
	{
		public IStore CreateStore(Configuration.Config config)
		{
			if (config.StorageTypes.Contains("file") == false)
				throw new NotSupportedException($"You must specify 'file' storage. Actually got {string.Join(",", config.StorageTypes)}");

			return new FileStore(
				new PhysicalFileSystem(),
				new StoreSerializer(),
				config.GlobalOptions,
				config.StorageOptions<FileStoreOptions>("file"));
		}
	}
}
