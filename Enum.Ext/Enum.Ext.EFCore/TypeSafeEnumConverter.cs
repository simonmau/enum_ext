using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Reflection;

namespace Enum.Ext.EFCore
{
    public class TypeSafeEnumConverter<TValue, TKey> : ValueConverter<TValue, TKey>
        where TValue : TypeSafeEnum<TValue, TKey>
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
    {
        private static bool CanConvert(Type objectType)
        {
            return TypeUtil.IsDerived(objectType, typeof(TypeSafeEnum<,>));
        }

        private static MethodInfo GetBaseMethod(Type objectType)
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

        public static TValue GetFromKey(TKey key)
        {
            if (!CanConvert(typeof(TValue)))
            {
                throw new NotImplementedException();
            }

            var method = GetBaseMethod(typeof(TValue));

            return method.Invoke(null, new[] { (object)key }) as TValue;
        }

        public TypeSafeEnumConverter() : base(value => value.Id, key => GetFromKey(key), null)
        {
        }
    }
}