using System.Runtime.CompilerServices;
using System;
using System.Reflection;
using System.Linq;

namespace Enum.Ext
{
    public static class Initialize
    {
        /// <summary>
        /// Initializes the given class derived from <see cref="TypeSafeEnum{TValue, TKey}"/>.
        /// Throws <see cref="TypeInitializationException"/> when something unexpected happens
        /// (like multiple same ids on a enum)
        /// </summary>
        public static void InitEnumExt<T>()
        {
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }

        /// <summary>
        /// Initializes all classes derived from <see cref="TypeSafeEnum{TValue, TKey}"/> in the given assembly.
        /// Throws <see cref="TypeInitializationException"/> when something unexpected happens
        /// (like multiple same ids on a enum)
        /// </summary>
        /// <param name="assembly"></param>
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