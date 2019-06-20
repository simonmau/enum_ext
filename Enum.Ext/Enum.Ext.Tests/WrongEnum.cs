namespace Enum.Ext.Tests
{
    public sealed class WrongEnum : TypeSafeEnum<WrongEnum, int>
    {
        public static readonly WrongEnum True = new WrongEnum(1);
        public static readonly WrongEnum False = new WrongEnum(1);

        public WrongEnum(int id) : base(id)
        {
        }
    }
}