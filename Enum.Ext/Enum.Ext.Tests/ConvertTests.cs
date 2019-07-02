using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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

            var json = JsonConvert.SerializeObject(tempClass);

            Assert.AreEqual("{\"Weekday\":1}", json);
        }

        [Test]
        public void Test_ConvertFromJson()
        {
            var tempClass = JsonConvert.DeserializeObject<ClassToSerialize>("{\"Weekday\":1}");

            Assert.AreEqual(Weekday.Monday, tempClass.Weekday);
        }

        public class ClassToSerialize
        {
            public Weekday Weekday { get; set; }
        }
    }
}