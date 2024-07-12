using Mailjet.Client.TransactionalEmails.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mailjet.Client.Serialization
{
    internal class VariableConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Variable);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException("This converter can only be used during serialization.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Variable variable)
                JsonSerializers.MailjetVariableJsonSerializer.Serialize(writer, variable.Value);
            else
                throw new InvalidOperationException("This converter only works on variables.");
        }
    }
}
