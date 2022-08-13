# Enum.Ext

[![Build Status](https://mauracher.visualstudio.com/enum_ext/_apis/build/status/simonmau.enum_ext?branchName=master)](https://mauracher.visualstudio.com/enum_ext/_build/latest?definitionId=20&branchName=master) [![Version](https://img.shields.io/nuget/v/Enum.Ext.svg)](https://www.nuget.org/packages/Enum.Ext)  [![Downloads](https://img.shields.io/nuget/dt/Enum.Ext.svg)](https://www.nuget.org/packages/Enum.Ext)

Enum.Ext provides a `TypeSafeEnum` that has a bunch of advantages compared to the normal .NET `Enum` value type.

You can store additional information directly with the enum and later query an enum based on that information, which you stored with it. We also offer various extension packages, that ensure compatibility with other areas of the .NET environment, as well as other known extensions.

### Installation 
https://www.nuget.org/packages/Enum.Ext/

`Enum.Ext` can be installed using the following command via the NuGet package manager console:

    PM> Install-Package Enum.Ext


If you are planning to use `Enum.Ext` with Entity Framework Core, you should also install `Enum.Ext.EFCore`

https://www.nuget.org/packages/Enum.Ext.EFCore/


    PM> Install-Package Enum.Ext.EFCore
    
List of all packages that we currently offer: 

    PM> Install-Package Enum.Ext
    PM> Install-Package Enum.Ext.EFCore
    PM> Install-Package Enum.Ext.AutoFixture
    PM> Install-Package Enum.Ext.NewtonsoftJson
    PM> Install-Package Enum.Ext.SystemTextJson
    PM> Install-Pakcage Enum.Ext.Swashbuckle.AspNetCore

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

    private Weekday(int id, string name) : base(id, name)
    {
    }
}
```

use it just like the native one 

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

#### Usage with a switch statment

```C#
var day = Weekday.Monday;

switch (day)
{
    case var _ when day == Weekday.Monday:
        // Do stuff
        break;

    case var _ when day == Weekday.Tuesday:
        // Do Stuff
        break;
}
```

#### JSON Conversion

We currently support two JSON frameworks:

* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)
* [System.Text.Json](https://www.nuget.org/packages/System.Text.Json)

To use the build-in converters you can either annotate your enum classes directly

```C#
// with Newtonsoft.Json
[JsonConverter(typeof(JsonTypeSafeEnumConverter))] 
public sealed class Weekday : TypeSafeNameEnum<Weekday, int>
{
    ...
}

// with System.Text.Json
[JsonConverter(typeof(JsonTypeSafeEnumConverter<Weekday, int>))]
public sealed class Weekday : TypeSafeNameEnum<Weekday, int>
{
    ...
}
```

or add them globally (e.g. in ASP.NET Core) 

```C#
// with Newtonsoft.Json
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.Converters.Add(new JsonTypeSafeEnumConverter());
    });
    
    ...
}

// with System.Text.Json
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonTypeSafeEnumConverterFactory());
    });
    
    ...
}
```


#### EF Core configuration

Just add the following line at the end of the `OnModelCreating` method in your `DbContext` class and you are ready to go

```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    ...
    
    modelBuilder.ConfigureEnumExt();
}
```

#### Usage with AutoFixture

```C#
var fixture = new Fixture();
fixture.WithEnumExt();

var weekday = fixture.Create<Weekday>();
```

#### Type mappers for Swashbuckle.AspNetCore

If you are using `int` or `long` as key type and want your enums to be correctly displayed in the swagger documentation, you can use

```C#
builder.Services.AddSwaggerGen(options =>
{
    ...
    
    options.ConfigureEnumExt(typeof(Weekday).Assembly);
}
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

    private YearlyPrice(int id, decimal price, DateTime validFrom, DateTime validTo) : base(id)
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
