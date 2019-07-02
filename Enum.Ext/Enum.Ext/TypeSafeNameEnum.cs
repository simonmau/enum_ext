using System;

namespace Enum.Ext
{
    public abstract class TypeSafeNameEnum<TValue, TKey> : TypeSafeEnum<TValue, TKey>
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
        where TValue : TypeSafeEnum<TValue, TKey>
    {
        protected TypeSafeNameEnum(TKey id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}