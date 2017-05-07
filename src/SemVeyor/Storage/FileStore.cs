using System.Linq;
using FileSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SemVeyor.CommandLine;
using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public class FileStore : IStorage
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			Converters =
			{
				new StringEnumConverter()

			},
			Formatting = Formatting.None
		};

		private readonly IFileSystem _fs;
		private readonly Options _options;
		private readonly FileStoreOptions _storeOptions;

		public FileStore(IFileSystem fs, Options options,  FileStoreOptions storeOptions)
		{
			_fs = fs;
			_options = options;
			_storeOptions = storeOptions;
		}

		public void Write(AssemblyDetails details)
		{
			if (_options.ReadOnly)
				return;

			var json = JsonConvert.SerializeObject(details, Settings);

			_fs.AppendFileLines(_storeOptions.Path, json).Wait();
		}

		public AssemblyDetails Read()
		{
			if (_fs.FileExists(_storeOptions.Path).Result == false)
				return null;

			var json = _fs.ReadFileLines(_storeOptions.Path).Result.LastOrDefault();

			return json != null
				? JsonConvert.DeserializeObject<AssemblyDetails>(json)
				: null;
		}
	}
}
