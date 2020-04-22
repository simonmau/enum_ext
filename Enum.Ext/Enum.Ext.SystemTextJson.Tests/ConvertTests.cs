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
            var tempClass = new ClassToSerialize<Weekday>
            {
                Item = Weekday.Monday
            };

            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var json = JsonSerializer.Serialize(tempClass, serializerOptions);

            json.Should().Be("{\"Item\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<Weekday>>("{\"Item\":1}", serializerOptions);

            tempClass.Item.Should().Be(Weekday.Monday);
        }

        [Test]
        public void Test_ConvertToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<WeekdayWithAnnotation>
            {
                Item = WeekdayWithAnnotation.Monday
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.Should().Be("{\"Item\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<WeekdayWithAnnotation>>("{\"Item\":1}");

            tempClass.Item.Should().Be(WeekdayWithAnnotation.Monday);
        }

        [Test]
        public void Test_ConvertNullToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<Weekday>
            {
                Item = null
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.Should().Be("{\"Item\":null}");
        }

        [Test]
        public void Test_ConvertToNullFromJsonValueWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<WeekdayWithAnnotation>>("{\"Item\":null}");

            tempClass.Item.Should().Be(null);
        }
    }
}