using System;
using Newtonsoft.Json;
using SemVeyor.Domain;

namespace SemVeyor.Storage
{
	public class TypeNameConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(TypeName);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return new TypeName(Convert.ToString(reader.Value));
		}
	}
}
