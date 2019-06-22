using FluentAssertions;
using NUnit.Framework;
using System;

namespace Enum.Ext.Tests
{
    [TestFixture]
    public class WeekdayTests
    {
        [SetUp]
        public void InitWeekday()
        {
            Initialize.InitEnumExt<Weekday>();
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
        public void Test_AssignByInt()
        {
            Weekday day = (Weekday)2;

            day.Should().Be(Weekday.Tuesday);
        }

        [Test]
        public void Test_PrintOutName()
        {
            var day = Weekday.Monday;

            day.Name.Should().Be("--Monday--");
        }

        [Test]
        public void Test_ThrowsWhenSameId()
        {
            Action initializeWithSameId = () => Initialize.InitEnumExt<WrongEnum>();

            initializeWithSameId.Should().Throw<TypeInitializationException>();
        }
    }
}