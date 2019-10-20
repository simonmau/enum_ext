using Enum.Ext.Tests.Shared;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Enum.Ext.Tests
{
    [TestFixture]
    public class WeekdayTests
    {
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

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void Test_SwitchStatement(int dayInt, int expectedBranch)
        {
            var day = (Weekday)dayInt;

            int branch = 0;

            switch (day)
            {
                case var _ when day == Weekday.Monday:
                    branch = 1;
                    break;

                case var _ when day == Weekday.Tuesday:
                    branch = 2;
                    break;
            }

            branch.Should().Be(expectedBranch);
        }
    }
}