using System.Linq;
using FileSystem;
using SemVeyor.Configuration;
using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public class FileStore : IStore
	{
		private readonly IFileSystem _fs;
		private readonly StoreSerializer _serializer;
		private readonly Options _options;
		private readonly FileStoreOptions _storeOptions;

		public FileStore(IFileSystem fs, StoreSerializer serializer, Options options,  FileStoreOptions storeOptions)
		{
			_fs = fs;
			_serializer = serializer;
			_options = options;
			_storeOptions = storeOptions;
		}

		public void Write(AssemblyDetails details)
		{
			if (_options.ReadOnly)
				return;

			var json = _serializer.Serialize(details);

			_fs.AppendFileLines(_storeOptions.Path, json).Wait();
		}

		public AssemblyDetails Read()
		{
			if (_fs.FileExists(_storeOptions.Path).Result == false)
				return null;

			var json = _fs.ReadFileLines(_storeOptions.Path).Result.LastOrDefault();

			return json != null
				? _serializer.Deserialize<AssemblyDetails>(json)
				: null;
		}
	}
}
