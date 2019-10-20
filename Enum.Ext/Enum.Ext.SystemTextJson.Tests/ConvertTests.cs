using Enum.Ext.Tests;
using Enum.Ext.Tests.Shared;
using FluentAssertions;
using NUnit.Framework;
using System.Text.Json;

namespace Enum.Ext.SystemTextJson.Tests
{
    [TestFixture]
    public class ConvertTests
    {
        [Test]
        public void Test_ConvertToJsonWithSerializerOptions()
        {
            var tempClass = new ClassToSerialize
            {
                Weekday = Weekday.Monday
            };

            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var json = JsonSerializer.Serialize(tempClass, serializerOptions);

            json.Should().Be("{\"Weekday\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var tempClass = JsonSerializer.Deserialize<ClassToSerialize>("{\"Weekday\":1}", serializerOptions);

            tempClass.Weekday.Should().Be(Weekday.Monday);
        }

        [Test]
        public void Test_ConvertToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerializeWithAnnotation
            {
                Weekday = WeekdayWithAnnotation.Monday
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.Should().Be("{\"Weekday\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerializeWithAnnotation>("{\"Weekday\":1}");

            tempClass.Weekday.Should().Be(WeekdayWithAnnotation.Monday);
        }

        [Test]
        public void Test_ConvertNullToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerializeWithAnnotation
            {
                Weekday = null
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.Should().Be("{\"Weekday\":null}");
        }

        [Test]
        public void Test_ConvertToNullFromJsonValueWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerializeWithAnnotation>("{\"Weekday\":null}");

            tempClass.Weekday.Should().Be(null);
        }

        public class ClassToSerialize
        {
            public Weekday Weekday { get; set; }
        }

        public class ClassToSerializeWithAnnotation
        {
            public WeekdayWithAnnotation Weekday { get; set; }
        }
    }
}