using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enum.Ext.SystemTextJson
{
    public class JsonTypeSafeEnumConverter<TValue, TKey> : JsonConverter<TypeSafeEnum<TValue, TKey>>
        where TValue : TypeSafeEnum<TValue, TKey>
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return TypeUtil.IsDerived(typeToConvert, typeof(TypeSafeEnum<,>));
        }

        private Type GetKeyType(Type objectType)
        {
            return TypeUtil.GetKeyType(objectType, typeof(TypeSafeEnum<,>));
        }

        private MethodInfo GetBaseMethod(Type objectType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return null;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(TypeSafeEnum<,>))
                    return currentType.GetMethod("GetById");

                currentType = currentType.BaseType;
            }

            return null;
        }

        public override TypeSafeEnum<TValue, TKey> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!CanConvert(typeToConvert))
            {
                throw new NotImplementedException();
            }

            var keyType = GetKeyType(typeToConvert);
            var method = GetBaseMethod(typeToConvert);

            var id = JsonSerializer.Deserialize(ref reader, keyType, options);

            return (TypeSafeEnum<TValue, TKey>)method.Invoke(null, new[] { Convert.ChangeType(id, keyType) });
        }

        public override void Write(Utf8JsonWriter writer, TypeSafeEnum<TValue, TKey> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Id, options);
        }

        public override void WriteAsPropertyName(Utf8JsonWriter writer, TypeSafeEnum<TValue, TKey> value, JsonSerializerOptions options)
        {
            writer.WritePropertyName(value.Id.ToString());
        }

        public override TypeSafeEnum<TValue, TKey> ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var item = reader.GetString();

            if (int.TryParse(item, out var id))
            {
                var keyType = GetKeyType(typeToConvert);
                var method = GetBaseMethod(typeToConvert);

                return (TypeSafeEnum<TValue, TKey>)method.Invoke(null, new[] { Convert.ChangeType(id, keyType) });
            }

            throw new NotImplementedException();
        }

        public bool CanRead => true;
    }
}