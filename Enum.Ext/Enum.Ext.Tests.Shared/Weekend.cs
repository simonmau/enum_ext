namespace Enum.Ext.Tests.Shared
{
    public sealed class Weekend : TypeSafeEnum<Weekend, string>
    {
        public static readonly Weekend Saturday = new Weekend("--Saturday--");
        public static readonly Weekend Sunday = new Weekend("--Sunday--");

        private Weekend(string id) : base(id)
        {
        }
    }
}