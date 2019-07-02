using System.Runtime.CompilerServices;
using System;
using System.Reflection;
using System.Linq;

namespace Enum.Ext
{
    public static class Initialize
    {
        /// <summary>
        /// Obsolete, does nothing
        /// </summary>
        [Obsolete]
        public static void InitEnumExt<T>()
        {
            //RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }

        /// <summary>
        /// Obsolete, does nothing
        /// </summary>
        /// <param name="assembly"></param>
        [Obsolete]
        public static void InitEnumExt(Assembly assembly)
        {
            //var types = assembly.GetTypes()
            //    .Where(t => TypeUtil.IsDerived(t, typeof(TypeSafeEnum<,>)))
            //    .Distinct();

            //foreach (var item in types)
            //{
            //    RuntimeHelpers.RunClassConstructor(item.TypeHandle);
            //}
        }
    }
}