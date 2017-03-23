using System;
using SemVeyor.CommandLine;
using SemVeyor.Infrastructure;

namespace SemVeyor.Storage
{
	public class StorageFactory
	{
		public IStorage CreateStore(CliParameters cli, Options options)
		{
			if (options.Storage.Equals("file", StringComparison.OrdinalIgnoreCase) == false)
				throw new NotSupportedException(options.Storage);

			return new FileStore(
				new PhysicalFileSystem(),
				cli.ForPrefix(options.Storage).Build<FileStoreOptions>());
		}
	}
}
