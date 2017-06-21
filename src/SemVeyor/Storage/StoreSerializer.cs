using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SemVeyor.Storage
{
	public class StoreSerializer
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
		
		public string Serialize(object input)
		{
			return JsonConvert.SerializeObject(input, Settings);
		}

		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, Settings);
		}
	}
}
