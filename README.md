# Enum.Ext

[![Build Status](https://mauracher.visualstudio.com/enum_ext/_apis/build/status/simonmau.enum_ext?branchName=master)](https://mauracher.visualstudio.com/enum_ext/_build/latest?definitionId=20&branchName=master) [![Version](https://img.shields.io/nuget/v/Enum.Ext.svg)](https://www.nuget.org/packages/Enum.Ext) 

Enum.Ext provides a TypeSafeEnum that has many advantages compared to the normal .NET Enum value type.

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
    public static readonly Weekday Thursday = new Weekday(4, "Thursday");
    public static readonly Weekday Friday = new Weekday(5, "Friday");
    public static readonly Weekday Saturday = new Weekday(6, "Saturday");
    public static readonly Weekday Sunday = new Weekday(7, "Sunday");

    public Weekday(int id, string name) : base(id, name)
    {
    }
}
```
