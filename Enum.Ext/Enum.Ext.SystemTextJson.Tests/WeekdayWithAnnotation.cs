using Enum.Ext.SystemTextJson;
using Enum.Ext.Tests.Shared;

using System.Text.Json.Serialization;

namespace Enum.Ext.Tests
{
    [JsonConverter(typeof(JsonTypeSafeEnumConverter<WeekdayWithAnnotation, int>))]
    public sealed class WeekdayWithAnnotation : TypeSafeNameEnum<WeekdayWithAnnotation, int>
    {
        public static readonly WeekdayWithAnnotation Monday = new WeekdayWithAnnotation(1, "--Monday--");
        public static readonly WeekdayWithAnnotation Tuesday = new WeekdayWithAnnotation(2, "--Tuesday--");
        public static readonly WeekdayWithAnnotation Wednesday = new WeekdayWithAnnotation(3, "--Wednesday--");
        public static readonly WeekdayWithAnnotation Thursday = new WeekdayWithAnnotation(4, "--Thursday--");
        public static readonly WeekdayWithAnnotation Friday = new WeekdayWithAnnotation(5, "--Friday--");
        public static readonly WeekdayWithAnnotation Saturday = new WeekdayWithAnnotation(6, "--Saturday--");
        public static readonly WeekdayWithAnnotation Sunday = new WeekdayWithAnnotation(7, "--Sunday--");

        private WeekdayWithAnnotation(int id, string name) : base(id, name)
        {
        }
    }
}