using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mailjet.Client.JsonConverters
{
    public class VariableConverter : JsonConverter
    {
        private static readonly JsonSerializer VariableSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            Converters = new List<JsonConverter>
            {
                new Newtonsoft.Json.Converters.StringEnumConverter()
            }
        });

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException("This converter can only be used during serialization.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            VariableSerializer.Serialize(writer, value);
        }
    }
}
