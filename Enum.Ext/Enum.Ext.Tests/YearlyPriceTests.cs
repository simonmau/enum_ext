using NUnit.Framework;
using Shouldly;
using System;

namespace Enum.Ext.Tests
{
    [TestFixture]
    public class YearlyPriceTests
    {
        [Test]
        public void QueryByDate_ShouldReturnCorrectEnum()
        {
            DateTime year_2018 = new DateTime(2018, 5, 3);

            YearlyPrice.GetPriceByDate(year_2018).ShouldBe(YearlyPrice.Price_2018);
        }

        [Test]
        public void Enum_ShouldHoldCorrectAdditionalInformation()
        {
            var priceEnum = YearlyPrice.Price_2018;

            priceEnum.Price.ShouldBe(15.99m);
        }
    }
}