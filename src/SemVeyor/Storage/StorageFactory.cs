using System;
using FileSystem;
using SemVeyor.CommandLine;
using SemVeyor.Config;

namespace SemVeyor.Storage
{
	public class StorageFactory
	{
		public IStore CreateStore(CliParameters cli, Options options)
		{
			if (options.Storage.Equals("file", StringComparison.OrdinalIgnoreCase) == false)
				throw new NotSupportedException(options.Storage);

			return new FileStore(
				new PhysicalFileSystem(),
				new StoreSerializer(),
				options,
				cli.ForPrefix(options.Storage).Build<FileStoreOptions>());
		}
	}
}
