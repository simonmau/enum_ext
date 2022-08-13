using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enum.Ext.Swashbuckle.AspNetCore
{
    /// <summary>
    /// Extension methods provided by Enum.Ext.Swashbuckle.AspNetCore
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds mappings for <see cref="TypeSafeEnum{TValue, TKey}"/> when int or long is used as key
        /// </summary>
        /// <param name="options">The <see cref="SwaggerGenOptions"/> to which the mappings should be added</param>
        /// <param name="assemblies">The assemblies in which should be searched for implementations of <see cref="TypeSafeEnum{TValue, TKey}"/>.
        /// If no assemblies are specified the calling <see cref="Assembly"/> is used.</param>
        public static void ConfigureEnumExt(this SwaggerGenOptions options, params Assembly[] assemblies)
        {
            IEnumerable<Type> types;

            if (assemblies.Any())
            {
                types = assemblies.SelectMany(x => x.GetTypes()
                    .Where(y => TypeUtil.IsDerived(y, typeof(TypeSafeEnum<,>))));
            }
            else
            {
                types = Assembly.GetCallingAssembly().GetTypes()
                    .Where(y => TypeUtil.IsDerived(y, typeof(TypeSafeEnum<,>)));
            }

            foreach (var item in types)
            {
                var keyType = TypeUtil.GetKeyType(item, typeof(TypeSafeEnum<,>));

                if (keyType == typeof(int) || keyType == typeof(long))
                {
                    options.MapType(item, () => new OpenApiSchema
                    {
                        Type = "integer",
                    });
                }
            }
        }
    }
}