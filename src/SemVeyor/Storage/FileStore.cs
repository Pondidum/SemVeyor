using System.Linq;
using FileSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
		private readonly string _path;

		public FileStore(IFileSystem fs, FileStoreOptions options)
		{
			_fs = fs;
			_path = options.Path;
		}

		public void Write(AssemblyDetails details)
		{
			var json = JsonConvert.SerializeObject(details, Settings);

			_fs.AppendFileLines(_path, json).Wait();
		}

		public AssemblyDetails Read()
		{
			if (_fs.FileExists(_path).Result == false)
				return null;

			var json = _fs.ReadFileLines(_path).Result.LastOrDefault();

			return json != null
				? JsonConvert.DeserializeObject<AssemblyDetails>(json)
				: null;
		}
	}
}
