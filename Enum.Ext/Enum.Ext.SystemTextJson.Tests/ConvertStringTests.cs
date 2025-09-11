using Enum.Ext.Tests;
using Enum.Ext.Tests.Shared;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Enum.Ext.SystemTextJson.Tests
{
    [TestFixture]
    public class ConvertStringTests
    {
        [Test]
        public void Test_ConvertToJsonWithSerializerOptions()
        {
            var tempClass = new ClassToSerialize<Weekend>
            {
                Item = Weekend.Saturday
            };

            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var json = JsonSerializer.Serialize(tempClass, serializerOptions);

            json.ShouldBe("{\"Item\":\"--Saturday--\"}");
        }

        [Test]
        public void Test_ConvertFromJsonWithSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonTypeSafeEnumConverterFactory() }
            };

            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<Weekend>>("{\"Item\":\"--Saturday--\"}", serializerOptions);

            tempClass.Item.ShouldBe(Weekend.Saturday);
        }

        [Test]
        public void Test_ConvertToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<WeekendWithAnnotation>
            {
                Item = WeekendWithAnnotation.Saturday
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.ShouldBe("{\"Item\":\"--Saturday--\"}");
        }

        [Test]
        public void Test_ConvertFromJsonWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<WeekendWithAnnotation>>("{\"Item\":\"--Saturday--\"}");

            tempClass.Item.ShouldBe(WeekendWithAnnotation.Saturday);
        }

        [Test]
        public void Test_ConvertNullToJsonWithAnnotation()
        {
            var tempClass = new ClassToSerialize<Weekend>
            {
                Item = null
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.ShouldBe("{\"Item\":null}");
        }

        [Test]
        public void Test_ConvertToNullFromJsonValueWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerialize<WeekendWithAnnotation>>("{\"Item\":null}");

            tempClass.Item.ShouldBe(null);
        }

        [Test]
        public void Test_ConvertDictonaryKeyToJsonWithAnnotation()
        {
            var tempClass = new Dictionary<WeekendWithAnnotation, string>
            {
                { WeekendWithAnnotation.Saturday, "value"  }
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.ShouldBe("{\"--Saturday--\":\"value\"}");
        }

        [Test]
        public void Test_ConvertDictonaryKeyFormJsonWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<Dictionary<WeekendWithAnnotation, string>>("{\"--Saturday--\":\"value\"}");

            tempClass.Keys.First().ShouldBe(WeekendWithAnnotation.Saturday);
            tempClass[WeekendWithAnnotation.Saturday].ShouldBe("value");
        }

        [Test]
        public void Test_ConvertDictonaryValueToJsonWithAnnotation()
        {
            var tempClass = new Dictionary<string, WeekendWithAnnotation>
            {
                { "value", WeekendWithAnnotation.Saturday }
            };

            var json = JsonSerializer.Serialize(tempClass);

            json.ShouldBe("{\"value\":\"--Saturday--\"}");
        }

        [Test]
        public void Test_ConvertDictonaryValueFormJsonWithAnnotation()
        {
            var tempClass = JsonSerializer.Deserialize<Dictionary<string, WeekendWithAnnotation>>("{\"value\":\"--Saturday--\"}");

            tempClass.Keys.First().ShouldBe("value");
            tempClass["value"].ShouldBe(WeekendWithAnnotation.Saturday);
        }
    }
}