using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FabrikamWidgets.UI
{
    internal class KeyValuePairArrayConverter<TKey, TValue> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return ((objectType.IsValueType && objectType.IsGenericType) && (objectType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var typedvalue = (KeyValuePair<TKey, TValue>)value;
            writer.WriteStartObject();
            serializer.Serialize(writer, typedvalue.Key);
            serializer.Serialize(writer, typedvalue.Value);
            writer.WriteEndObject();
        }
    }
}
