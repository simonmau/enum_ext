# Enum.Ext

[![Build Status](https://mauracher.visualstudio.com/enum_ext/_apis/build/status/simonmau.enum_ext?branchName=master)](https://mauracher.visualstudio.com/enum_ext/_build/latest?definitionId=20&branchName=master) [![Version](https://img.shields.io/nuget/v/Enum.Ext.svg)](https://www.nuget.org/packages/Enum.Ext) 

Enum.Ext provides a `TypeSafeEnum` that has a bunch of advantages compared to the normal .NET `Enum` value type.

For example is it possible to store additional information directly with the enum. You are later on able to 
query an enum based on the information stored with it.

There is also a Json-Serializer implemented, so you dont have to cast from DTOs manually.

### Installation 
https://www.nuget.org/packages/Enum.Ext/

Enum.Ext can be installed using the following command via the NuGet package manager console:

    PM> Install-Package Enum.Ext


### How to use

Simply inherit your class from `TypeSafeEnum` or `TypeSafeNameEnum` and adjust everything to your needs.





#### Want a weekday enum with a special string representation for each day?
```C#
public sealed class Weekday : TypeSafeNameEnum<Weekday, int>
{
    public static readonly Weekday Monday = new Weekday(1, "--Monday--");
    public static readonly Weekday Tuesday = new Weekday(2, "--Tuesday--");
    public static readonly Weekday Wednesday = new Weekday(3, "--Wednesday--");
    ....

    public Weekday(int id, string name) : base(id, name)
    {
    }
}
```

Use the enum just like the native one 

```C#
var day = Weekday.Monday;

// Assigns Tuesday
day = (Weekday)2;
```

and access the additional information easily

```C#
var day = Weekday.Monday;

// Prints out '--Monday--'
Console.WriteLine(day.Name);
```

### Enum.Ext in action

Here you find some examples how you could use the extension.

#### A fixed price that is valid for a certain time period
```C#
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
        // The List property holds all elements declared above
        return List.FirstOrDefault(x => x.ValidFrom <= date && date <= x.ValidTo);
    }
}
```

Get the according enum for a given date
```C#
DateTime date = new DateTime(2018, 5, 3);

// Returns YearlyPrice.Price_2018
var price = YearlyPrice.GetPriceByDate(date);
```
