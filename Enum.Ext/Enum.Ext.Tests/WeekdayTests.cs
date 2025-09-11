using Enum.Ext.Tests.Shared;
using NUnit.Framework;
using Shouldly;

namespace Enum.Ext.Tests
{
    [TestFixture]
    public class WeekdayTests
    {
        [Test]
        public void Test_ConvertToInt()
        {
            int value = Weekday.Monday;

            value.ShouldBe(1);
        }

        [Test]
        public void Test_ConvertFromInt()
        {
            Weekday value = (Weekday)1;

            value.ShouldBe(Weekday.Monday);
        }

        [Test]
        public void Test_AssignByInt()
        {
            Weekday day = (Weekday)2;

            day.ShouldBe(Weekday.Tuesday);
        }

        [Test]
        public void Test_PrintOutName()
        {
            var day = Weekday.Monday;

            day.Name.ShouldBe("--Monday--");
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

            branch.ShouldBe(expectedBranch);
        }
    }
}