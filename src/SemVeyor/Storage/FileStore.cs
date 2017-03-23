using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SemVeyor.Domain;
using SemVeyor.Infrastructure;

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

			_fs.AppendLine(_path, json);
		}

		public AssemblyDetails Read()
		{
			var json = _fs.ReadAllLines(_path).LastOrDefault();

			return json != null
				? JsonConvert.DeserializeObject<AssemblyDetails>(json)
				: null;
		}
	}
}
