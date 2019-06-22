﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace Enum.Ext.EFCore
{
    public static class ConverterExtension
    {
        /// <summary>
        /// Adds a converter for all properties derived from <see cref="TypeSafeEnum{TValue, TKey}"/> so that entity framework core
        /// can work with it.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ConfigureEnumExt(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => TypeUtil.IsDerived(p.PropertyType, typeof(TypeSafeEnum<,>)));

                foreach (var property in properties)
                {
                    var keyType = TypeUtil.GetKeyType(property.PropertyType, typeof(TypeSafeEnum<,>));

                    var converterType = typeof(TypeSafeEnumConverter<,>).MakeGenericType(property.PropertyType, keyType);

                    var converter = (ValueConverter)Activator.CreateInstance(converterType);

                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                }
            }
        }
    }
}