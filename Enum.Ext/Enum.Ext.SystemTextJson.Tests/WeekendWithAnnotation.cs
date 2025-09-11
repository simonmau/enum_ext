using Enum.Ext.SystemTextJson;

using System.Text.Json.Serialization;

namespace Enum.Ext.Tests
{
    [JsonConverter(typeof(JsonTypeSafeEnumConverter<WeekendWithAnnotation, string>))]
    public sealed class WeekendWithAnnotation : TypeSafeEnum<WeekendWithAnnotation, string>
    {
        public static readonly WeekendWithAnnotation Saturday = new WeekendWithAnnotation("--Saturday--");
        public static readonly WeekendWithAnnotation Sunday = new WeekendWithAnnotation("--Sunday--");

        private WeekendWithAnnotation(string id) : base(id)
        {
        }
    }
}