using System;
using SemVeyor.CommandLine;

namespace SemVeyor.Storage
{
	public class StorageFactory
	{
		public IStorage CreateStore(CliParameters cli, Options options)
		{
			if (options.Storage != "file")
				throw new NotImplementedException(options.Storage);

			return new FileStore(cli.ForPrefix(options.Storage).Build<FileStoreOptions>());
		}
	}
}
