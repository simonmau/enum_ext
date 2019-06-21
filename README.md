# Enum.Ext

[![Build Status](https://mauracher.visualstudio.com/enum_ext/_apis/build/status/simonmau.enum_ext?branchName=master)](https://mauracher.visualstudio.com/enum_ext/_build/latest?definitionId=20&branchName=master) [![Version](https://img.shields.io/nuget/v/Enum.Ext.svg)](https://www.nuget.org/packages/Enum.Ext) 

Enum.Ext provides a `TypeSafeEnum` that has many advantages compared to the normal .NET `Enum` value type.

### Installation 
https://www.nuget.org/packages/Enum.Ext/

Enum.Ext can be installed using the following command via the NuGet package manager console:

    PM> Install-Package Enum.Ext


### How to use

Simply inherit your class from `TypeSafeEnum` or `TypeSafeNameEnum` and adjust everything to your needs.





#### Want a weeksday enum with an according string representation for each day?
```C#
public sealed class Weekday : TypeSafeNameEnum<Weekday, int>
{
    public static readonly Weekday Monday = new Weekday(1, "Monday");
    public static readonly Weekday Tuesday = new Weekday(2, "Tuesday");
    public static readonly Weekday Wednesday = new Weekday(3, "Wednesday");
    ....

    public Weekday(int id, string name) : base(id, name)
    {
    }
}
```

#### A fixed price that is valid for a certain time period
```C#
public sealed class YearlyPrice : TypeSafeEnum<YearlyPrice, int>
{
    public decimal Price { get; }

    public DateTime ValidFrom { get; }

    public DateTime ValidTo { get; }

    public static readonly YearlyPrice Price_2018 = 
        new DatedPrice(1, 15.99m, new DateTime(2018, 1, 1), new DateTime(31, 12, 2018));

    public static readonly YearlyPrice Price_2019 =
        new DatedPrice(1, 16.99m, new DateTime(2019, 1, 1), new DateTime(31, 12, 2019));

    public YearlyPrice(int id, decimal price, DateTime validFrom, DateTime validTo) : base(id)
    {
        ValidFrom = validFrom;
        ValidTo = validTo;
        Price = price;
    }

    public YearlyPrice GetPriceByDate(DateTime date)
    {
        // The List property contains all elements declared above
        return List.FirstOrDefault(x => x.ValidFrom <= date && date <= ValidTo);
    }
}
```

