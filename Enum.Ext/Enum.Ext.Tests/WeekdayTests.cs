using FluentAssertions;
using NUnit.Framework;
using System;

namespace Enum.Ext.Tests
{
    public class WeekdayTests
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

            value.Should().Be(1);
        }

        [Test]
        public void Test_ConvertFromInt()
        {
            Weekday value = (Weekday)1;

            value.Should().Be(Weekday.Monday);
        }

        [Test]
        public void Test_ThrowsWhenSameId()
        {
            Action secondInitialize = () => Initialize.InitStaticFields<WrongEnum>();

            secondInitialize.Should().Throw<TypeInitializationException>();
        }
    }
}