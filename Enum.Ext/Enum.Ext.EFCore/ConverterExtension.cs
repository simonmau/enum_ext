using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace Enum.Ext.EFCore
{
    public static class ConverterExtension
    {
        public static void ConfigureEnumExt(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => IsDerived(p.PropertyType));

                foreach (var property in properties)
                {
                    var keyType = GetKeyType(property.PropertyType);

                    var converterType = typeof(TypeSafeEnumConverter<,>).MakeGenericType(property.PropertyType, keyType);

                    var converter = (ValueConverter)Activator.CreateInstance(converterType);

                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                }
            }
        }

        private static bool IsDerived(Type objectType)
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

        private static Type GetKeyType(Type objectType)
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
    }
}