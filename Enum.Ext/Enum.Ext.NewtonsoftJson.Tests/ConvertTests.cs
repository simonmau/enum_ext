using Enum.Ext.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Enum.Ext.NewtonsoftJson.Tests
{
    public class ConvertTests
    {
        [Test]
        public void Test_ConvertToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerializeWithAnnotation
            {
                Weekday = WeekdayWithAnnotation.Monday
            };

            var json = JsonConvert.SerializeObject(tempClass);

            json.Should().Be("{\"Weekday\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithAnnotation()
        {
            var tempClass = JsonConvert.DeserializeObject<ClassToSerializeWithAnnotation>("{\"Weekday\":1}");

            tempClass.Weekday.Should().Be(WeekdayWithAnnotation.Monday);
        }

        public class ClassToSerializeWithAnnotation
        {
            public WeekdayWithAnnotation Weekday { get; set; }
        }
    }
}