using System.Runtime.CompilerServices;
using System;

namespace Enum.Ext
{
    public static class Initialize
    {
        /// <summary>
        /// throws <see cref="TypeInitializationException"/> when something unexpected happens
        /// (like multiple same ids on a enum)
        /// </summary>
        public static void InitStaticFields<T>()
        {
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }
    }
}