using System.Runtime.CompilerServices;
using System;
using System.Reflection;
using System.Linq;

namespace Enum.Ext
{
    public static class Initialize
    {
        /// <summary>
        /// throws <see cref="TypeInitializationException"/> when something unexpected happens
        /// (like multiple same ids on a enum)
        /// </summary>
        public static void InitEnumExt<T>()
        {
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }

        public static void InitEnumExt(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => TypeUtil.IsDerived(t, typeof(TypeSafeEnum<,>)))
                .Distinct();

            foreach (var item in types)
            {
                RuntimeHelpers.RunClassConstructor(item.TypeHandle);
            }
        }
    }
}