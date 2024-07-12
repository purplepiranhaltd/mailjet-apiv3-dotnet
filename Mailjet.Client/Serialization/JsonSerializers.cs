using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mailjet.Client.Serialization
{
    public class JsonSerializers
    {
        public static readonly JsonSerializer MailjetJsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new Newtonsoft.Json.Converters.StringEnumConverter(),
                new VariableConverter()
            }
        });

        public static readonly JsonSerializer MailjetVariableJsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings 
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            Converters = new List<JsonConverter>
            {
                new Newtonsoft.Json.Converters.StringEnumConverter()
            }
        });
    }
}
