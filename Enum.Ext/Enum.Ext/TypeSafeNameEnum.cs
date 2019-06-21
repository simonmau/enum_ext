namespace Enum.Ext
{
    public abstract class TypeSafeNameEnum<TValue, TKey> : TypeSafeEnum<TypeSafeNameEnum<TValue, TKey>, TKey> where TKey : struct
    {
        protected TypeSafeNameEnum(TKey id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}