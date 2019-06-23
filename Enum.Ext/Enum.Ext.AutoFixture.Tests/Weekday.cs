namespace Enum.Ext.AutoFixture.Tests
{
    public sealed class Weekday : TypeSafeNameEnum<Weekday, int>
    {
        public static readonly Weekday Monday = new Weekday(1, "--Monday--");
        public static readonly Weekday Tuesday = new Weekday(2, "--Tuesday--");
        public static readonly Weekday Wednesday = new Weekday(3, "--Wednesday--");
        public static readonly Weekday Thursday = new Weekday(4, "--Thursday--");
        public static readonly Weekday Friday = new Weekday(5, "--Friday--");
        public static readonly Weekday Saturday = new Weekday(6, "--Saturday--");
        public static readonly Weekday Sunday = new Weekday(7, "--Sunday--");

        private Weekday(int id, string name) : base(id, name)
        {
        }
    }
}