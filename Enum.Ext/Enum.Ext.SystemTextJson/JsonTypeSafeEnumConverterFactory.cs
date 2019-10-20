using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enum.Ext.SystemTextJson
{
    public class JsonTypeSafeEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return TypeUtil.IsDerived(typeToConvert, typeof(TypeSafeEnum<,>));
        }

        public override JsonConverter CreateConverter(Type typeToConvert,
                                                 JsonSerializerOptions options)
        {
            var keyType = TypeUtil.GetKeyType(typeToConvert, typeof(TypeSafeEnum<,>));

            var converterType = typeof(JsonTypeSafeEnumConverter<,>).MakeGenericType(typeToConvert, keyType);

            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
}