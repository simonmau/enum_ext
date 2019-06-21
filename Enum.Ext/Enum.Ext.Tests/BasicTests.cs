using NUnit.Framework;
using System;

namespace Enum.Ext.Tests
{
    public class BasicTests
    {
        [SetUp]
        public void InitWeekday()
        {
            Initialize.InitStaticFields<Weekday>();
        }

        [Test]
        public void Test_ConvertToInt()
        {
            int value = Weekday.Monday;

            Assert.AreEqual(1, value);
        }

        [Test]
        public void Test_ConvertFromInt()
        {
            Weekday value = (Weekday)1;

            Assert.AreEqual(Weekday.Monday, value);
        }

        [Test]
        public void Test_ThrowsWhenSameId()
        {
            Assert.Throws<TypeInitializationException>(() => Initialize.InitStaticFields<WrongEnum>());
        }
    }
}