using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace Enum.Ext.EFCore
{
    public static class Extensions
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

                    if (entityType.IsOwned())
                    {
#pragma warning disable EF1001 // Internal EF Core API usage, might change without notice -> have not found a better way till now
                        var navigationBuilder = new OwnedNavigationBuilder(entityType.FindOwnership());
#pragma warning restore EF1001

                        navigationBuilder.Property(property.Name).HasConversion(converter);
                    }
                    else
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                    }
                }
            }
        }
    }
}