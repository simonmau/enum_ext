using System;
using System.Collections.Generic;
using System.Text;

namespace Enum.Ext.EFCore
{
    public static class ConverterExtension
    {
        public static TypeSafeEnumConverter<TValue, TKey> GetEFCoreConverter<TValue, TKey>(this TypeSafeEnum<TValue, TKey> _)
            where TValue : TypeSafeEnum<TValue, TKey>
            where TKey : struct
        {
            return new TypeSafeEnumConverter<TValue, TKey>();
        }
    }
}