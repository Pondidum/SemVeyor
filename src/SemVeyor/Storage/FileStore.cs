using System.IO;
using System.Linq;
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

		private readonly string _path;

		public FileStore(FileStoreOptions options)
		{
			_path = options.Path;
		}

		public void Write(AssemblyDetails details)
		{
			var json = JsonConvert.SerializeObject(details, Settings);

			File.AppendAllLines(_path, new [] { json });
		}

		public AssemblyDetails Read()
		{
			if (File.Exists(_path) == false)
				return null;

			var json = File.ReadAllLines(_path).Last();

			return JsonConvert.DeserializeObject<AssemblyDetails>(json);
		}
	}
}
