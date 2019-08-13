using NUnit.Framework;
using System.Text.Json;

namespace Enum.Ext.Tests
{
    public class ConvertTests
    {
        [Test]
        public void Test_ConvertToJson()
        {
            var tempClass = new ClassToSerialize
            {
                Weekday = Weekday.Monday
            };

            var json = JsonSerializer.Serialize<ClassToSerialize>(tempClass);

            Assert.AreEqual("{\"Weekday\":1}", json);
        }

        [Test]
        public void Test_ConvertFromJson()
        {
            var tempClass = JsonSerializer.Deserialize<ClassToSerialize>("{\"Weekday\":1}");

            Assert.AreEqual(Weekday.Monday, tempClass.Weekday);
        }

        public class ClassToSerialize
        {
            public Weekday Weekday { get; set; }
        }
    }
}