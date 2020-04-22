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
            var tempClass = new ClassToSerialize<Weekday>
            {
                Item = Weekday.Monday
            };

            var serializerOptions = new JsonSerializerSettings
            {
                Converters = { new JsonTypeSafeEnumConverter() }
            };

            var json = JsonConvert.SerializeObject(tempClass, serializerOptions);

            json.Should().Be("{\"Item\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithSerializerOptions()
        {
            var serializerOptions = new JsonSerializerSettings
            {
                Converters = { new JsonTypeSafeEnumConverter() }
            };

            var tempClass = JsonConvert.DeserializeObject<ClassToSerialize<Weekday>>("{\"Item\":1}", serializerOptions);

            tempClass.Item.Should().Be(Weekday.Monday);
        }

        [Test]
        public void Test_ConvertToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<WeekdayWithAnnotation>
            {
                Item = WeekdayWithAnnotation.Monday
            };

            var json = JsonConvert.SerializeObject(tempClass);

            json.Should().Be("{\"Item\":1}");
        }

        [Test]
        public void Test_ConvertFromJsonWithAnnotation()
        {
            var tempClass = JsonConvert.DeserializeObject<ClassToSerialize<WeekdayWithAnnotation>>("{\"Item\":1}");

            tempClass.Item.Should().Be(WeekdayWithAnnotation.Monday);
        }

        [Test]
        public void Test_ConvertNullToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<WeekdayWithAnnotation>
            {
                Item = null
            };

            var json = JsonConvert.SerializeObject(tempClass);

            json.Should().Be("{\"Item\":null}");
        }

        [Test]
        public void Test_ConvertToNullFromJsonValueWithAnnotation()
        {
            var tempClass = JsonConvert.DeserializeObject<ClassToSerialize<WeekdayWithAnnotation>>("{\"Item\":null}");

            tempClass.Item.Should().Be(null);
        }
    }
}