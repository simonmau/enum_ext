namespace Enum.Ext.Tests.Shared
{
    public class ClassToSerialize<T> where T : TypeSafeEnum<T, int>
    {
        public T Item { get; set; }
    }
}