using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enum.Ext.NewtonsoftJson
{
    internal static class TypeUtil
    {
        public static bool IsDerived(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return false;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }

        public static Type GetKeyType(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return null;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return currentType.GenericTypeArguments[1];

                currentType = currentType.BaseType;
            }

            return null;
        }

        public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => type.IsAssignableFrom(p.FieldType))
                .Select(pi => (TFieldType)pi.GetValue(null))
                .ToList();
        }
    }
}