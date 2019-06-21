using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enum.Ext.Tests
{
    public sealed class YearlyPrice : TypeSafeEnum<YearlyPrice, int>
    {
        public decimal Price { get; private set; }

        public DateTime ValidFrom { get; private set; }

        public DateTime ValidTo { get; private set; }

        public static readonly YearlyPrice Price_2018 =
            new YearlyPrice(1, 15.99m, new DateTime(2018, 1, 1), new DateTime(2018, 12, 31));

        public static readonly YearlyPrice Price_2019 =
            new YearlyPrice(2, 16.99m, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

        public YearlyPrice(int id, decimal price, DateTime validFrom, DateTime validTo) : base(id)
        {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Price = price;
        }

        public static YearlyPrice GetPriceByDate(DateTime date)
        {
            return List.FirstOrDefault(x => x.ValidFrom <= date && date <= x.ValidTo);
        }
    }
}