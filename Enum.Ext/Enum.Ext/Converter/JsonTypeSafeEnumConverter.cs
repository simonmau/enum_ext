using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Enum.Ext.Converter
{
    public class JsonTypeSafeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Type currentType = objectType.BaseType;
            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(TypeSafeEnum<,>))
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }

        private Type GetKeyType(Type objectType)
        {
            Type currentType = objectType.BaseType;
            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(TypeSafeEnum<,>))
                    return currentType.GenericTypeArguments[1];

                currentType = currentType.BaseType;
            }

            return null;
        }

        private MethodInfo GetBaseMethod(Type objectType)
        {
            Type currentType = objectType.BaseType;
            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(TypeSafeEnum<,>))
                    return currentType.GetMethod("GetById");

                currentType = currentType.BaseType;
            }

            return null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!CanConvert(objectType))
            {
                throw new NotImplementedException();
            }

            var keyType = GetKeyType(objectType);
            var method = GetBaseMethod(objectType);

            return method.Invoke(null, new[] { Convert.ChangeType(reader.Value, keyType) });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var idProperty = value.GetType().GetProperty("Id");

            var ser = new JsonSerializer();
            ser.Serialize(writer, idProperty.GetValue(value));
        }

        public override bool CanRead => true;
    }
}