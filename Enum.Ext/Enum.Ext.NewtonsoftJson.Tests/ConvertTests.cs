using Enum.Ext.NewtonsoftJson.Converter;
using Enum.Ext.Tests;
using Enum.Ext.Tests.Shared;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Enum.Ext.NewtonsoftJson.Tests
{
    public class ConvertTests
    {
        [Test]
        public void Test_ConvertToJsonWithSerializerOptions()
        {
            var tempClass = new ClassToSerialize
            {
                Weekday = Weekday.Monday
            };

            var serializerOptions = new JsonSerializerSettings
            {
                Converters = { new JsonTypeSafeEnumConverter() }
            };

            var json = JsonConvert.SerializeObject(tempClass, serializerOptions);

            json.Should().Be("{\"Weekday\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithSerializerOptions()
        {
            var serializerOptions = new JsonSerializerSettings
            {
                Converters = { new JsonTypeSafeEnumConverter() }
            };

            var tempClass = JsonConvert.DeserializeObject<ClassToSerialize>("{\"Weekday\":1}", serializerOptions);

            tempClass.Weekday.Should().Be(Weekday.Monday);
        }

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

        [Test]
        public void Test_ConvertNullToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerializeWithAnnotation
            {
                Weekday = null
            };

            var json = JsonConvert.SerializeObject(tempClass);

            json.Should().Be("{\"Weekday\":null}");
        }

        [Test]
        public void Test_ConvertToNullFromJsonValueWithAnnotation()
        {
            var tempClass = JsonConvert.DeserializeObject<ClassToSerializeWithAnnotation>("{\"Weekday\":null}");

            tempClass.Weekday.Should().Be(null);
        }

        public class ClassToSerialize
        {
            public Weekday Weekday { get; set; }
        }

        public class ClassToSerializeWithAnnotation
        {
            public WeekdayWithAnnotation? Weekday { get; set; }
        }
    }
}