using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public interface IStorage
	{
		void Write(AssemblyDetails details);
		AssemblyDetails Read();
	}

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

		public FileStore(string path)
		{
			_path = path;
		}

		public void Write(AssemblyDetails details)
		{
			var json = JsonConvert.SerializeObject(details, Settings);

			File.AppendAllLines(_path, new [] { json });
		}

		public AssemblyDetails Read()
		{
			var json = File.ReadAllLines(_path).Last();

			return JsonConvert.DeserializeObject<AssemblyDetails>(json);
		}
	}
}